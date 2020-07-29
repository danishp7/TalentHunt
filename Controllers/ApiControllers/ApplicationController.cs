using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TalentHunt.Data;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Controllers.ApiControllers
{
    [Authorize]
    [Route("api/applications")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _user;
        private readonly ISharedRepository _sharedRepo;
        public ApplicationController(IApplicationRepository applicationRespository, IMapper mapper,
                                     UserManager<User> userManager, ISharedRepository sharedRepository)
        {
            _repo = applicationRespository;
            _mapper = mapper;
            _user = userManager;
            _sharedRepo = sharedRepository;
        }

        // Post: api/applications/apply
        [HttpPost("apply")]
        public async Task<IActionResult> ApplyforJob(ApplicationDto model)
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest("Please signed in to your account.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            var modelFields = await _repo.ValidateApplicationFields(model);
            if (modelFields == null)
                return BadRequest("Form has invalid data.");

            var application = _mapper.Map<Application>(modelFields);

            var user = await _user.FindByIdAsync(userId);
            application.AppUser = user;

            _sharedRepo.Add(application);

            if (await _sharedRepo.SaveAll())
                return Created("/api/applications/apply/" + application.Id, model);

            return BadRequest("Something went wrong...");

        }
    }
}