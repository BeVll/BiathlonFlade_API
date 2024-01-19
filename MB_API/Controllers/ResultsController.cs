using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System;
using AutoMapper;
using FladeUp_Api.Data;
using MB_API.Data.Entities.Identity;
using MB_API.Data.Entities;
using MB_API.Interfaces;
using MB_API.Requests.Track;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MB_API.Requests.Result;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {

        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;
        private readonly ICloudStorageService _cloudStorage;

        public ResultsController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, IMapper mapper, AppEFContext appEFContext, ICloudStorageService cloudStorage)
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
                var result = await _appEFContext.Results
                    .Where(u => u.Id == id)
                    .Include(r => r.Player)
                    .Include(r => r.Checkpoint)
                    .Include(r => r.Checkpoint.CheckPointType)
                    .Include(r => r.Race)
                    .Include(r => r.Race.EventType)
                    .Include(r => r.Race.Track)
                    .Include(r => r.Race.Track.Country)
                    .SingleOrDefaultAsync();

                if (result == null)
                    return NotFound();


                return Ok(result);

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
                var results = await _appEFContext.Results
                    .Include(r => r.Player)
                    .Include(r => r.Checkpoint)
                    .Include(r => r.Checkpoint.CheckPointType)
                    .Include(r => r.Race)
                    .Include(r => r.Race.EventType)
                    .Include(r => r.Race.Track)
                    .Include(r => r.Race.Track.Country)
                    .ToListAsync();

                if (results == null)
                    return NotFound();


                return Ok(results);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ResultCreateUpdateRequest model)
        {
            try
            {
                var player = _userManager.Users.Where(u => u.UserName == model.UserName).FirstOrDefault();  

                if (player == null)
                    return NotFound();

                ResultEntity result = new ResultEntity()
                {
                    RaceId = model.RaceId,
                    PlayerId = player.Id,
                    CheckpointId = model.CheckpointId,
                    ResultValue = model.ResultValue,
                    IsDNF = model.IsDNF,
                    StartNumber = model.StartNumber,
                    IsTeamResult = model.IsTeamResult,
                    IsFinish = model.IsFinish,
                    Lap = model.Lap,
                    TeamId = model.TeamId,
                    StageNumber = model.StageNumber,
                };

                _appEFContext.Add(result);
                await _appEFContext.SaveChangesAsync();


                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

      
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromForm] TrackCreateUpdateRequest value)
        //{
        //    try
        //    {
        //        var track = await _appEFContext.Tracks
        //            .Where(r => r.Id == id)
        //            .SingleOrDefaultAsync();

        //        if (track == null)
        //            return NotFound();

        //        track.TrackName = value.TrackName;
        //        track.TrackDetails = value.TrackDetails;
        //        track.CountryId = value.CountryId;

        //        _appEFContext.Update(track);
        //        await _appEFContext.SaveChangesAsync();


        //        return Ok(track);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

 
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var track = await _appEFContext.Tracks
        //            .Where(r => r.Id == id)
        //            .SingleOrDefaultAsync();

        //        if (track == null)
        //            return NotFound();

        //        _appEFContext.Remove(track);
        //        await _appEFContext.SaveChangesAsync();


        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
