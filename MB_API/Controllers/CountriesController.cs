using AutoMapper;
using MB_API.Data.Entities.Identity;
using MB_API.Data.Entities;
using FladeUp_Api.Data;
using MB_API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MB_API.Requests.Country;
using MB_API.Requests.Event;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FladeUp_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;
        private readonly ICloudStorageService _cloudStorage;

        public CountriesController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, IMapper mapper, AppEFContext appEFContext, ICloudStorageService cloudStorage)
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
        public async Task<IActionResult> Create([FromForm] CountryCreateUpdateRequest model)
        {
            try
            {
                CountryEntity country = new CountryEntity()
                {
                    Name = model.Name,
                    Character = model.Character,
                    Code = model.Code,
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] CountryCreateUpdateRequest value)
        {
            try
            {
                var country = await _appEFContext.Countries
                    .Where(r => r.Id == id)
                    .SingleOrDefaultAsync();

                if (country == null)
                    return NotFound();

                country.Name = value.Name;
                country.Character = value.Character;
                country.Code = value.Code;


                _appEFContext.Update(country);
                await _appEFContext.SaveChangesAsync();


                return Ok(country);
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
                var country = await _appEFContext.Countries
                    .Where(r => r.Id == id)
                    .SingleOrDefaultAsync();

                if (country == null)
                    return NotFound();

                _appEFContext.Remove(country);
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
