using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TalentHunt.Data;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Controllers.ApiControllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _user;
        private readonly SignInManager<User> _signInUser;

        public AuthController(IAuthRepository repository, IMapper mapper, IConfiguration configuration,
                              UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _repo = repository;
            _mapper = mapper;
            _config = configuration;
            _user = userManager;
            _signInUser = signInManager;
        }

        // Post: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            // set password
            var password_hash_key = _repo.CreatePasswordWithEncryption(userDto.Password);
            User user = new User
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                NormalizedUserName = userDto.Email.ToUpper().Normalize(),
                NormalizedEmail = userDto.Email.ToUpper().Normalize()
            };

            var isUserEmail = await _user.FindByEmailAsync(user.Email);
            if (isUserEmail != null)
                return BadRequest("User exist with this email address. kindly provide different email id.");

            var isUserAdded = await _user.CreateAsync(user, userDto.Password);
            if (!isUserAdded.Succeeded)
                return BadRequest("Something went wrong...");

            var newUser = _mapper.Map<LogInUserDto>(user);
            return Created("api/auth/" + user.Id, newUser);
        }

        // Post: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoggingUserDto logInUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            var user = await _user.FindByNameAsync(logInUserDto.Email);
            if (user == null)
                return BadRequest("No such user exist, please signup!");

            var isPassword = await _user.CheckPasswordAsync(user, logInUserDto.Password);
            if (isPassword == false)
                return BadRequest("Incorrect UserName or Password.");

            var isLogin = await _signInUser.PasswordSignInAsync(logInUserDto.Email, logInUserDto.Password, false, false);
            if (isLogin.Succeeded)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var key = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(2000),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                var returnUser = _mapper.Map<LogInUserDto>(user);
                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    user = returnUser,
                    message = "Login Successfully"
                });
            }
            return BadRequest("Incorrect UserName or Password");
            
        }

        // Get: api/auth/logout
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // it will signout the user.
            await _signInUser.SignOutAsync();
            return Ok("Logout Successfully!");
        }
    }
}