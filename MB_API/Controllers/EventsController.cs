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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        // GET: api/<EventController>
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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var events = _appEFContext.Events
                    .ToList();

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
        public async Task<IActionResult> Create([FromForm] EventCreateUpdateRequest model)
        {
            try
            {
                EventEntity eventEntity = new EventEntity()
                {
                    EventName = model.EventName,
                    EventTypeId = model.EventTypeId,
                    TeamEvent = model.TeamEvent,
                    TrackId = model.TrackId,
                    EventDate = model.EventDate,
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

        // PUT api/<TracksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] EventCreateUpdateRequest value)
        {
            try
            {
                var eventEntity = await _appEFContext.Events
                    .Where(r => r.Id == id)
                    .SingleOrDefaultAsync();

                if (eventEntity == null)
                    return NotFound();

                eventEntity.EventName = value.EventName;
                eventEntity.EventTypeId = value.EventTypeId;
                eventEntity.TeamEvent = value.TeamEvent;
                eventEntity.TrackId = value.TrackId;
                eventEntity.EventDate = value.EventDate;


                _appEFContext.Update(eventEntity);
                await _appEFContext.SaveChangesAsync();


                return Ok(eventEntity);
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
