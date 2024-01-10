using AutoMapper;
using MB_API.Constants;
using MB_API.Data;
using MB_API.Data.Entities.Identity;
using MB_API.Interfaces;
using MB_API.Requests;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FladeUp_Api.Data;
using MB_API.Requests.User;

namespace MB_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;
        private readonly ICloudStorageService _cloudStorage;
        private readonly IEmailService _emailService;
        public UserController(UserManager<UserEntity> userManager, IJwtTokenService jwtTokenService, IMapper mapper, AppEFContext appEFContext, ICloudStorageService cloudStorage, IEmailService emailService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _appEFContext = appEFContext;
            _cloudStorage = cloudStorage;
            _emailService = emailService;
        }


        [HttpPost("UpdateCountry")]
        public async Task<IActionResult> UpdateCountry([FromQuery] int countryId, [FromQuery] string userName)
        {
            try
            {

                var user = await _userManager.FindByNameAsync(userName);
                
                if(user == null) 
                    return NotFound();

                if (_appEFContext.Countries.Where(c => c.Id == countryId).FirstOrDefault() == null)
                    return BadRequest(701);

                user.CountryId = countryId;
                _appEFContext.Update(user);
                await _appEFContext.SaveChangesAsync();

                var country = _appEFContext.Countries.Where(c => c.Id == countryId).FirstOrDefault();
                
                return Ok(country.Character);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("country")]
        public async Task<IActionResult> GetCountry([FromQuery] string userName)
        {
            try
            {

                var user = await _userManager.FindByNameAsync(userName);

                if (user == null)
                    return NotFound();

                var country = _appEFContext.Countries.Where(c => c.Id == user.CountryId).FirstOrDefault();

                return Ok(country);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        //[HttpPost("loginGoogle")]
        //public async Task<IActionResult> LoginGoogle([FromForm] GoogleLoginRequest model)
        //{
        //    try
        //    {
        //        Console.Write("sdfsd");

        //        GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();
        //        GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(model.Token, settings).Result;

        //        if (payload == null)
        //            return BadRequest("Login error!");

        //        var user = await _userManager.FindByEmailAsync(payload.Email);
        //        var token = "";

        //        if (user == null)
        //        {
        //            var client = new HttpClient();
        //            string fileName = null;
        //            string extension = ".png";

        //            using (var response = await client.GetAsync(payload.Picture))
        //            {

        //                var fileExp = response.Content.Headers.ContentType.MediaType.Replace("image/", ".");
        //                var dirSave = Path.Combine("images/user_avatars/");
        //                var rndName = Path.GetRandomFileName();
        //                var imageName = dirSave + rndName + fileExp;
        //                Stream stream = response.Content.ReadAsStream();
        //                _cloudStorage.UploadFileAsync(new FormFile(stream, 0, stream.Length, null, rndName), imageName);

        //                var newUser = new UserEntity()
        //                {
        //                    Name = payload.Name,
        //                    Email = payload.Email,
        //                    Image = imageName,
        //                    UserName = "user" + _userManager.Users.Count() + 1.ToString(),
        //                    Description = "",
        //                    Verified = false,
        //                    Country = model.Country,
        //                    CountryCode = model.CountryCode.ToLower(),
        //                    City = "",
        //                    IsLightTheme = false,
        //                    Header = null,
        //                    Instagram = null,
        //                    Facebook = null,
        //                    Twitter = null,
        //                    CreatedAt = DateTime.UtcNow,
        //                };
        //                var result = await _userManager.CreateAsync(newUser);

        //                if (result.Succeeded)
        //                {
        //                    var id = await _userManager.GetUserIdAsync(newUser);
        //                    newUser.UserName = "user" + id.ToString();
        //                    await _userManager.UpdateAsync(newUser);
        //                    result = await _userManager.AddToRoleAsync(newUser, Roles.User);
        //                    token = await _jwtTokenService.CreateToken(newUser);
        //                    return Ok(new { token });
        //                }
        //            }
        //        }
        //        if (!user.EmailConfirmed)
        //        {
        //            user.EmailConfirmed = true;
        //            await _userManager.UpdateAsync(user);
        //        }
        //        token = await _jwtTokenService.CreateToken(user);
        //        return Ok(new { token });

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var token = "";

                
                if (user == null)
                    return BadRequest("User doen`t exist");

                if (!user.EmailConfirmed)
                    return BadRequest("Email is not confirmed! To you email address was sended message with confirmation button(link)");

                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);

                if(!passwordCheck)
                    return BadRequest("Wrong password");

                token = await _jwtTokenService.CreateToken(user);
                return Ok(new { token });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromForm] ConfirmEmailRequest model)
        {
            try
            {
                var user = await _appEFContext.Users.Where(u => u.Id == model.Id).SingleOrDefaultAsync();

                if (user == null)
                    return BadRequest("User doesn`t exist!");

                var base64EncodedBytes = System.Convert.FromBase64String(model.Token);
                var token = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                var res = await _userManager.ConfirmEmailAsync(user, token);

                if(res.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(res.ToString());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
