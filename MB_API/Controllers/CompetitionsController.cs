using AutoMapper;
using FladeUp_Api.Data;
using MB_API.Data.Entities.Identity;
using MB_API.Data.Entities;
using MB_API.Interfaces;
using MB_API.Requests.Track;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MB_API.Requests.Competition;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MB_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompetitionsController : ControllerBase
    {
        // GET: api/<CompetitionsController>
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;
        private readonly ICloudStorageService _cloudStorage;

        public CompetitionsController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, IMapper mapper, AppEFContext appEFContext, ICloudStorageService cloudStorage)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _appEFContext = appEFContext;
            _cloudStorage = cloudStorage;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var competition = _appEFContext.Competitions
                    .Where(u => u.Id == id)
                    .SingleOrDefault();

                if (competition == null)
                    return NotFound();


                return Ok(competition);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int? season)
        {
            try
            {
                var competitions = _appEFContext.Competitions
                    .ToList();

                if(season !=  null)
                    competitions = competitions.Where(u => u.Season == season).ToList();

                if (competitions == null)
                    return NotFound();


                return Ok(competitions);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CompetitionCreateUpdateRequest model)
        {
            try
            {
                CompetitionEntity competition = new CompetitionEntity()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Season = model.Season,
                    Type = model.Type,
                };

                _appEFContext.Add(competition);
                await _appEFContext.SaveChangesAsync();


                return Ok(competition);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompetitionCreateUpdateRequest model)
        {
            try
            {
                var competition = await _appEFContext.Competitions
                    .Where(r => r.Id == id)
                    .SingleOrDefaultAsync();

                if (competition == null)
                    return NotFound();

                competition.Name = model.Name;
                competition.Description = model.Description;
                competition.Season = model.Season;
                competition.Type = model.Type;

                _appEFContext.Update(competition);
                await _appEFContext.SaveChangesAsync();


                return Ok(competition);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var competition = await _appEFContext.Competitions
                    .Where(r => r.Id == id)
                    .SingleOrDefaultAsync();

                if (competition == null)
                    return NotFound();

                _appEFContext.Remove(competition);
                await _appEFContext.SaveChangesAsync();


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
