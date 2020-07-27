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
using Newtonsoft.Json;
using TalentHunt.Data;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Controllers.ApiControllers
{
    [AllowAnonymous]
    [Route("api/vacancies")]
    [ApiController]
    // [Authorize]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyRepository _repo;
        private readonly ISharedRepository _sharedRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _user;
        public VacancyController(IVacancyRepository repository, ISharedRepository sharedRepository,
                                 IMapper mapper, UserManager<User> userManager)
        {
            _repo = repository;
            _sharedRepo = sharedRepository;
            _mapper = mapper;
            _user = userManager;
        }

        // POST: api/vacancies
        [Authorize]
        [HttpPost("postVacancy")]
        public async Task<IActionResult> PostVacancy([FromBody] PostVacancyDto vacancyDto)
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest("Please signed in to your account.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            if (await _repo.IsVacancy(vacancyDto.Title.ToLower()))
                return BadRequest("Job Post available for similar title.");

            var validateModel = _repo.ValidateVacancy(vacancyDto);
            if (validateModel == null)
                return BadRequest("Something went wrong...");

            var currentlyLoginUserName = User.Identity.Name;
            var loggedInUser = await _user.FindByEmailAsync(currentlyLoginUserName);
            
            var vacancy = _mapper.Map<Vacancy>(validateModel);
            vacancy.AppUser = loggedInUser;

            _sharedRepo.Add(vacancy);
            
            if (await _sharedRepo.SaveAll())
            {
                var detailedVacancy = _mapper.Map<DetailedVacancyDto>(vacancy);
                return Created("api/vacancy/" + vacancy.Id, detailedVacancy);
            }
            return BadRequest("Something went wrong...");
        }

        // PUT: api/vacancies
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacancy(int id, [FromBody] PostVacancyDto vacancyDto)
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest("Please signed in to your account.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            var validateModel = _repo.ValidateVacancy(vacancyDto);
            if (validateModel == null)
                return BadRequest("Something went wrong...");

            var currentlyLoginUserName = User.Identity.Name;
            var loggedInUser = await _user.FindByEmailAsync(currentlyLoginUserName);

            var vacancy = await _repo.GetVacancy(id, loggedInUser);
            if (vacancy == null)
                return NotFound("No such Job post available.");

            var updatedVacancy = _mapper.Map<PostVacancyDto, Vacancy>(vacancyDto, vacancy);
            await _sharedRepo.SaveAll();

            var returnUpdatedVacancy = _mapper.Map<DetailedVacancyDto>(updatedVacancy);
            
            return Ok(returnUpdatedVacancy);
        }


        // GET: api/vacancies/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVacancy(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest("Please signed in to your account.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            var currentlyLoginUserName = User.Identity.Name;
            var loggedInUser = await _user.FindByEmailAsync(currentlyLoginUserName);

            var detailedVacancy = await _repo.GetVacancy(id, loggedInUser);
            if (detailedVacancy == null)
                return BadRequest("Something went wrong...");

            var responsibilities = _repo.GetResponsibilites(id);
            
            var responsibilitiesDto = _mapper.Map<ICollection<ResponsibilityDto>>(responsibilities);
            var returnVacancy = _mapper.Map<DetailedVacancyDto>(detailedVacancy);

            var returnedVacancy = new ReturnVacancyDto
            {
                Title = returnVacancy.Title,
                Description = returnVacancy.Description,
                Education = returnVacancy.Education,
                Responsibilities = responsibilitiesDto,
                Experience = returnVacancy.Experience,
                Salary = returnVacancy.Salary,
            };
            return Ok(returnedVacancy);
        }

        // GET: api/vacancies
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetVacancies()
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest("Please signed in to your account.");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            var currentlyLoginUserName = User.Identity.Name;
            var loggedInUser = await _user.FindByEmailAsync(currentlyLoginUserName);

            var vacancies = await _repo.GetVacancies(loggedInUser);
            if (vacancies == null)
                return BadRequest("Something went wrong...");

            var returnVacancies = _mapper.Map<IEnumerable<ConciseVacancyDto>>(vacancies);

            return Ok(returnVacancies);
        }

    }
}
