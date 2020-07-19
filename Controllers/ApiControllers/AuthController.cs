using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TalentHunt.Data;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repo = repository;
            _mapper = mapper;
            _config = configuration;
        }

        // Post: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var checkUser = await _repo.Register(user, userDto.Password);
            if (checkUser == null)
                return BadRequest("Something went wrong with request...");

            var newUser = _mapper.Map<LogInUserDto>(checkUser);

            return Created("api/auth/" + checkUser.Id, newUser);
        }

        // Post: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoggingUserDto user)
        {
            var loggingInUser = await _repo.Login(user.UserName, user.Password);
            if (loggingInUser == null)
                return BadRequest("User name or password is wrong...");

            var loggedInUser = _mapper.Map<LogInUserDto>(loggingInUser);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loggingInUser.Id.ToString()),
                new Claim(ClaimTypes.Name, loggingInUser.UserName)
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

            return Ok(new {
                token = tokenHandler.WriteToken(token),
                user = loggedInUser 
            });
        }
    }
}