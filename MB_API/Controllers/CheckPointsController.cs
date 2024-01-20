using AutoMapper;
using FladeUp_Api.Data;
using MB_API.Data.Entities.Identity;
using MB_API.Data.Entities;
using MB_API.Interfaces;
using MB_API.Requests.Track;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MB_API.Requests.CheckPoint;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MB_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CheckPointsController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;
        private readonly ICloudStorageService _cloudStorage;

        public CheckPointsController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, IMapper mapper, AppEFContext appEFContext, ICloudStorageService cloudStorage)
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
                var checkPoint = _appEFContext.CheckPoints
                    .Where(u => u.Id == id)
                    .Include(c => c.CheckPointType)
                    .SingleOrDefault();

                if (checkPoint == null)
                    return NotFound();


                return Ok(checkPoint);

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
                var checkPoints = _appEFContext.CheckPoints
                    .Include(c => c.CheckPointType)
                    .ToList();

                if (checkPoints == null)
                    return NotFound();


                return Ok(checkPoints);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CheckPointCreateUpdateRequest model)
        {
            try
            {
                CheckPointEntity checkPoint = new CheckPointEntity()
                {
                    Name = model.Name,
                    CheckPointTypeId = model.CheckPointTypeId,
                    TrackId = model.TrackId,
                    X1 = model.X1,
                    Y1 = model.Y1,
                    Z1 = model.Z1,
                    X2 = model.X2,
                    Y2 = model.Y2,
                    Z2 = model.Z2,
                };

                _appEFContext.Add(checkPoint);
                await _appEFContext.SaveChangesAsync();


                return Ok(checkPoint);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // PUT api/<TracksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CheckPointCreateUpdateRequest model)
        {
            try
            {
                var checkPoint = await _appEFContext.CheckPoints
                    .Where(r => r.Id == id)
                    .Include(c => c.CheckPointType)
                    .SingleOrDefaultAsync();

                if (checkPoint == null)
                    return NotFound();

                checkPoint.Name = model.Name;
                checkPoint.CheckPointTypeId = model.CheckPointTypeId;
                checkPoint.TrackId = model.TrackId;
                checkPoint.X1 = model.X1;
                checkPoint.Y1 = model.Y1;
                checkPoint.Z1 = model.Z1;
                checkPoint.X2 = model.X2;
                checkPoint.Y2 = model.Y2;
                checkPoint.Z2 = model.Z2;

                _appEFContext.Update(checkPoint);
                await _appEFContext.SaveChangesAsync();


                return Ok(checkPoint);
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
                var checkPoint = await _appEFContext.CheckPoints
                    .Where(r => r.Id == id)
                    .SingleOrDefaultAsync();

                if (checkPoint == null)
                    return NotFound();

                _appEFContext.Remove(checkPoint);
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
