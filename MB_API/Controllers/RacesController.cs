using AutoMapper;
using FladeUp_Api.Data;
using MB_API.Data.Entities.Identity;
using MB_API.Data.Entities;
using MB_API.Interfaces;
using MB_API.Requests.Track;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MB_API.Requests;
using MB_API.Requests.Event;
using Microsoft.EntityFrameworkCore;
using MB_API.Requests.CheckPoint;
using MB_API.Requests.RaceCheckPoint;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MB_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RacesController : ControllerBase
    {
        // GET: api/<EventController>
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;
        private readonly ICloudStorageService _cloudStorage;

        public RacesController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, IMapper mapper, AppEFContext appEFContext, ICloudStorageService cloudStorage)
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
                var raceEntity = _appEFContext.Races
                    .Where(u => u.Id == id)
                    .Include(e => e.RaceType)
                    .Include(e => e.Track)
                    .Include(e => e.Track.Country)
                    .SingleOrDefault();

                if (raceEntity == null)
                    return NotFound();


                return Ok(raceEntity);

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
                var races = _appEFContext.Races
                    .Include(e => e.RaceType)
                    .Include(e => e.Track)
                    .Include(e => e.Track.Country)
                    .ToList();

                if (races == null)
                    return NotFound();


                return Ok(races);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] RaceCreateUpdateRequest model)
        {
            try
            {
                RaceEntity raceEntity = new RaceEntity()
                {
                    RaceName = model.RaceName,
                    RaceTypeId = model.RaceTypeId,
                    TeamRace = model.TeamRace,
                    TrackId = model.TrackId,
                    RaceDate = model.RaceDate,
                    EventId = model.EventId,
                };

                _appEFContext.Add(raceEntity);
               
                await _appEFContext.SaveChangesAsync();


                return Ok(raceEntity);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createCheckPoint")]
        public async Task<IActionResult> CreateCheckPoint([FromBody] RaceCheckPointCreateRequest model)
        {
            try
            {
                RaceCheckPointEntity raceCheckPoint = new RaceCheckPointEntity()
                {
                    RaceId = model.RaceId,
                    CheckPointId = model.CheckPointId,
                    Lap = model.Lap,
                    Number = model.Number,
                    Name = model.Name,
                };

                _appEFContext.Add(raceCheckPoint);
                await _appEFContext.SaveChangesAsync();


                return Ok(raceCheckPoint);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<TracksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] RaceCreateUpdateRequest value)
        {
            try
            {
                var raceEntity = await _appEFContext.Races
                    .Where(r => r.Id == id)
                    .Include(e => e.RaceType)
                    .Include(e => e.Track)
                    .Include(e => e.Track.Country)
                    .SingleOrDefaultAsync();

                if (raceEntity == null)
                    return NotFound();

                raceEntity.RaceName = value.RaceName;
                raceEntity.RaceTypeId = value.RaceTypeId;
                raceEntity.TeamRace = value.TeamRace;
                raceEntity.TrackId = value.TrackId;
                raceEntity.RaceDate = value.RaceDate;
                raceEntity.EventId = value.EventId;

                _appEFContext.Update(raceEntity);
                await _appEFContext.SaveChangesAsync();


                return Ok(raceEntity);
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
                var raceEntity = await _appEFContext.Races
                    .Where(r => r.Id == id)
                    .SingleOrDefaultAsync();

                if (raceEntity == null)
                    return NotFound();

                _appEFContext.Remove(raceEntity);
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
