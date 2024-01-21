using AutoMapper;
using FladeUp_Api.Data;
using MB_API.Data.Entities.Identity;
using MB_API.Data.Entities;
using MB_API.Interfaces;
using MB_API.Requests.Competition;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using MB_API.Models.Event;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MB_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;
        private readonly ICloudStorageService _cloudStorage;

        public EventsController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, IMapper mapper, AppEFContext appEFContext, ICloudStorageService cloudStorage)
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
                var eventEntity = _appEFContext.Events
                    .Where(u => u.Id == id)
                    .Include(u => u.Competition)
                    .Include(u => u.Track)
                    .Include(u => u.Country)
                    .SingleOrDefault();

                if (eventEntity == null)
                    return NotFound();


                return Ok(eventEntity);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int? competitionId, int? trackId, int? season)
        {
            try
            {
                var eventEntities = await _appEFContext.Events
                    .Include(u => u.Competition)
                    .Include(u => u.Track)
                    .Include(u => u.Country)
                    .Select(u => _mapper.Map<EventModel>(u))
                    .ToListAsync();


   


                if (competitionId != null)
                    eventEntities = eventEntities.Where(u => u.Competition.Id == competitionId).ToList();

                if (trackId != null)
                    eventEntities = eventEntities.Where(u => u.Track.Id == trackId).ToList();

                if (season != null)
                    eventEntities = eventEntities.Where(u => u.Competition.Season == season).ToList();

                foreach (var eventEntity in eventEntities)
                {   
                    eventEntity.Races = await _appEFContext.Races
                        .Where(r => r.EventId == eventEntity.Id)
                        .OrderBy(r => r.RaceDate)
                        .ToListAsync();

                    if (!eventEntity.Races.IsNullOrEmpty())
                    {
                        eventEntity.StartDate = eventEntity.Races.Min(r => r.RaceDate);
                        eventEntity.EndDate = eventEntity.Races.Max(r => r.RaceDate);
                    }
                   
                }
                
                var events = eventEntities.Where(e => !e.Races.IsNullOrEmpty()).ToList();

                var eventsNoRaces = eventEntities.Where(e => e.Races.IsNullOrEmpty()).ToList();


                events = events.OrderBy(e => e.Races.Min(r => r.RaceDate)).ToList();

                events = events.Union(eventsNoRaces).ToList();
            

                if (events == null)
                    return NotFound();


                return Ok(events);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] EventCreateUpdateRequest model)
        {
            try
            {
                EventEntity eventEntity = new EventEntity()
                {
                    Name = model.Name,
                    CompetitionId = model.CompetitionId,
                    TrackId = model.TrackId,
                    CountryId = model.CountryId,
                };

                _appEFContext.Add(eventEntity);
                await _appEFContext.SaveChangesAsync();


                return Ok(eventEntity);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EventCreateUpdateRequest model)
        {
            try
            {
                var eventEntity = await _appEFContext.Events
                    .Where(r => r.Id == id)
                    .Include(r => r.Competition)
                    .Include(r => r.Track)
                    .SingleOrDefaultAsync();

                if (eventEntity == null)
                    return NotFound();

                eventEntity.Name = model.Name;
                eventEntity.CompetitionId = model.CompetitionId;
                eventEntity.TrackId = model.TrackId;
                eventEntity.CountryId = model.CountryId;
                _appEFContext.Update(eventEntity);
                await _appEFContext.SaveChangesAsync();


                return Ok(eventEntity);
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
                var eventEntity = await _appEFContext.Events
                    .Where(r => r.Id == id)
                    .SingleOrDefaultAsync();

                if (eventEntity == null)
                    return NotFound();

                _appEFContext.Remove(eventEntity);
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
