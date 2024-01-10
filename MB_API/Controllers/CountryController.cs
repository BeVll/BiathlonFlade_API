using AutoMapper;
using MB_API.Data.Entities.Identity;
using MB_API.Data.Entities;
using FladeUp_Api.Data;
using MB_API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MB_API.Requests.Country;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FladeUp_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;
        private readonly ICloudStorageService _cloudStorage;

        public CountryController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, IMapper mapper, AppEFContext appEFContext, ICloudStorageService cloudStorage)
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
                var country = _appEFContext.Countries
                    .Where(u => u.Id == id)
                    .SingleOrDefault();

                if (country == null)
                    return NotFound();

                
                return Ok(country);

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
                var countries = _appEFContext.Countries
                    .ToList();

                if (countries == null)
                    return NotFound();


                return Ok(countries);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CountryCreateRequest model)
        {
            try
            {
                CountryEntity country = new CountryEntity()
                {
                    Name = model.Name,
                    Character = model.Character,
                };

                _appEFContext.Add(country);
                await _appEFContext.SaveChangesAsync();


                return Ok(country);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpPut("update")]
        //public async Task<IActionResult> Update([FromForm] RoomUpdateRequest model)
        //{
        //    try
        //    {
        //        var room = await _appEFContext.Rooms
        //            .Where(r => r.Id == model.Id)
        //            .SingleOrDefaultAsync();

        //        if(room == null)
        //            return NotFound();

        //        room.Name = model.Name;
        //        room.Description = model.Description;

        //        _appEFContext.Update(room);
        //        await _appEFContext.SaveChangesAsync();


        //        return Ok(room);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("delete/{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var room = await _appEFContext.Rooms
        //            .Where(r => r.Id == id)
        //            .SingleOrDefaultAsync();

        //        if (room == null)
        //            return NotFound();

        //        _appEFContext.Remove(room);
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
