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
using MB_API.Hubs;
using Microsoft.AspNetCore.SignalR;
using MB_API.Models.CheckPoint;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MB_API.Controllers
{
    [Route("[controller]")]
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
                    .Where(r => r.Id == id)
                    .Include(r => r.Player)
                    .Include(r => r.Race)
                    .Include(r => r.RaceCheckpoint)
                    .Include(r => r.RaceCheckpoint.CheckPoint)
                    .Include(r => r.RaceCheckpoint.CheckPoint.CheckPointType)
                    .Include(r => r.Race)
                    .Include(r => r.Race.RaceType)
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
        public async Task<IActionResult> GetAll([FromQuery] int? id, int? raceId, int? checkpointId, int? playerId, int? checkpointTypeId)
        {
            try
            {
                var results = await _appEFContext.Results
                    .Include(r => r.Player)
                    .Include(r => r.Player.Country)
                    .Include(r => r.RaceCheckpoint)
                    .Include(r => r.RaceCheckpoint.CheckPoint)
                    .Include(r => r.RaceCheckpoint.CheckPoint.CheckPointType)
                    .Include(r => r.Race)
                    .Include(r => r.Race.RaceType)
                    .Include(r => r.Race.Track)
                    .Include(r => r.Race.Track.Country)
                    .ToListAsync();

                if (id != null)
                    results = results.Where(r => r.Id == id).ToList();

                if (raceId != null)
                {
                    results = results.Where(r => r.RaceId == raceId).ToList();
                    results = results.OrderBy(r => r.ResultValue).ToList();
                }
                    

                if (checkpointId != null)
                    results = results.Where(r => r.RaceCheckpointId == checkpointId).ToList();

                if (playerId != null)
                    results = results.Where(r => r.PlayerId == playerId).ToList();

                if (checkpointTypeId != null)
                    results = results.Where(r => r.RaceCheckpoint.CheckPoint.CheckPointTypeId == checkpointTypeId).ToList();


                if (results == null)
                    return NotFound();

                


                return Ok(results);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("allCheckPoints/{raceId}")]
        public async Task<IActionResult> GetAllCheckpoints(int raceId)
        {
            try
            {
                var raceCheckPoints = await _appEFContext.RaceCheckPoints
                    .Where(r => r.RaceId == raceId)
                    .Include(r => r.Race)
                    .Include(r => r.CheckPoint)
                    .OrderBy(r => r.Number)
                    .ToListAsync();

                if (raceCheckPoints == null)
                    return NotFound();




                return Ok(raceCheckPoints);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ResultCreateUpdateRequest model, [FromServices] IHubContext<ResultsHub> hubContext)
        {
            try
            {
                var player = _userManager.Users.Where(u => u.UserName == model.UserName).FirstOrDefault();  

                if (player == null)
                    return NotFound();

                var raceCp = await _appEFContext.RaceCheckPoints.Where(r => r.CheckPointId == model.CheckpointId && r.Lap == model.Lap && r.RaceId == model.RaceId).FirstOrDefaultAsync();

                if (raceCp != null)
                {
                    ResultEntity resultEntity = new ResultEntity()
                    {
                        RaceId = model.RaceId,
                        PlayerId = player.Id,
                        RaceCheckpointId = raceCp.Id,
                        ResultValue = model.ResultValue,
                        IsDNF = model.IsDNF,
                        StartNumber = model.StartNumber,
                        IsTeamResult = model.IsTeamResult,
                        IsFinish = model.IsFinish,
                        Lap = model.Lap,
                        TeamId = model.TeamId,
                        StageNumber = model.StageNumber,
                    };


                    _appEFContext.Add(resultEntity);
                    await _appEFContext.SaveChangesAsync();


                    var results = await _appEFContext.Results
                        .Where(r => r.RaceCheckpointId == resultEntity.RaceCheckpointId)
                        .Include(r => r.Player)
                        .Include(r => r.Player.Country)
                        .Include(r => r.RaceCheckpoint)
                        .Include(r => r.RaceCheckpoint.CheckPoint)
                        .Include(r => r.RaceCheckpoint.CheckPoint.CheckPointType)
                        .Include(r => r.Race)
                        .Include(r => r.Race.RaceType)
                        .Include(r => r.Race.Track)
                        .Include(r => r.Race.Track.Country)
                        .ToListAsync();
                    results = results.OrderBy(r => r.ResultValue).ToList();

                    await hubContext.Clients.All.SendAsync("ReceiveNewResult", results);

                    return Ok(resultEntity);
                }


                return NotFound();

               

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
