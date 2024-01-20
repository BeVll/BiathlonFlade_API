using AutoMapper;
using FladeUp_Api.Data;
using MB_API.Data.Entities.Identity;
using MB_API.Data.Entities;
using MB_API.Interfaces;
using MB_API.Requests.Country;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MB_API.Requests.Track;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MB_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;
        private readonly ICloudStorageService _cloudStorage;

        public TracksController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, IMapper mapper, AppEFContext appEFContext, ICloudStorageService cloudStorage)
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
                var track = _appEFContext.Tracks
                    .Where(u => u.Id == id)
                    .SingleOrDefault();

                if (track == null)
                    return NotFound();


                return Ok(track);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tracks = _appEFContext.Tracks
                    .ToList();

                if (tracks == null)
                    return NotFound();


                return Ok(tracks);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] TrackCreateUpdateRequest model)
        {
            try
            {
                TrackEntity track = new TrackEntity()
                {
                    TrackName = model.TrackName,
                    TrackDetails = model.TrackDetails,
                    CountryId = model.CountryId,
                };

                _appEFContext.Add(track);
                await _appEFContext.SaveChangesAsync();


                return Ok(track);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<TracksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] TrackCreateUpdateRequest value)
        {
            try
            {
                var track = await _appEFContext.Tracks
                    .Where(r => r.Id == id)
                    .SingleOrDefaultAsync();

                if (track == null)
                    return NotFound();

                track.TrackName = value.TrackName;
                track.TrackDetails = value.TrackDetails;
                track.CountryId = value.CountryId;

                _appEFContext.Update(track);
                await _appEFContext.SaveChangesAsync();


                return Ok(track);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TracksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var track = await _appEFContext.Tracks
                    .Where(r => r.Id == id)
                    .SingleOrDefaultAsync();

                if (track == null)
                    return NotFound();

                _appEFContext.Remove(track);
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
