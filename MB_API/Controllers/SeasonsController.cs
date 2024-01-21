using AutoMapper;
using FladeUp_Api.Data;
using MB_API.Data.Entities.Identity;
using MB_API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MB_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        // GET: api/<SeasonsController>
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;
        private readonly ICloudStorageService _cloudStorage;

        public SeasonsController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, IMapper mapper, AppEFContext appEFContext, ICloudStorageService cloudStorage)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _appEFContext = appEFContext;
            _cloudStorage = cloudStorage;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                List<int> seasons = await _appEFContext.Competitions
                    .Select(c => c.Season)
                    .Distinct()
                    .ToListAsync();

                if (seasons == null)
                    return NotFound();


                return Ok(seasons);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
