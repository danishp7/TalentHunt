using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TalentHunt.Data;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Controllers.ApiControllers
{
    [Route("api/vacancies")]
    [ApiController]
    // [Authorize]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyRepository _repo;
        private readonly ISharedRepository _sharedRepo;
        private readonly IMapper _mapper;
        public VacancyController(IVacancyRepository repository, ISharedRepository sharedRepository, IMapper mapper)
        {
            _repo = repository;
            _sharedRepo = sharedRepository;
            _mapper = mapper;
        }

        // POST: api/vacancies
        [HttpPost("postVacancy")]
        public async Task<IActionResult> PostVacancy([FromBody] PostVacancyDto vacancyDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == 0)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            var validateModel = await _repo.ValidateVacancy(vacancyDto);
            if (validateModel == null)
                return BadRequest("Something went wrong...");

            var vacancy = _mapper.Map<Vacancy>(validateModel);
            
            _sharedRepo.Add(vacancy);
            
            if (await _sharedRepo.SaveAll())
            {
                var detailedVacancy = _mapper.Map<DetailedVacancyDto>(vacancy);
                return Created("api/vacancy/" + vacancy.Id, detailedVacancy);
            }
            return BadRequest("Something went wrong...");
        }

        // PUT: api/vacancies
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVacancy(int id, [FromBody] PostVacancyDto vacancyDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == 0)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            var validateModel = await _repo.ValidateVacancy(vacancyDto);
            if (validateModel == null)
                return BadRequest("Something went wrong...");

            var vacancy = await _repo.GetVacancy(id);

            var updatedVacancy = _mapper.Map<PostVacancyDto, Vacancy>(vacancyDto, vacancy);
            await _sharedRepo.SaveAll();

            var returnUpdatedVacancy = _mapper.Map<DetailedVacancyDto>(updatedVacancy);
            
            return Ok(returnUpdatedVacancy);
        }


        // GET: api/vacancies/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVacancy(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId == 0)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            var detailedVacancy = await _repo.GetVacancy(id);
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
        [HttpGet]
        public async Task<IActionResult> GetVacancies()
        {
            //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //if (userId == 0)
            //    return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            var vacancies = await _repo.GetVacancies();
            if (vacancies == null)
                return BadRequest("Something went wrong...");

            var returnVacancies = _mapper.Map<IEnumerable<ConciseVacancyDto>>(vacancies);

            return Ok(returnVacancies);
        }

        // DELETE: api/vacancies/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacancy(int id)
        {
            var vacancy = await _repo.GetVacancy(id);
            if (vacancy == null)
                return NotFound("No such vacancy in our system.");

            _sharedRepo.Delete<Vacancy>(vacancy);

            if (await _sharedRepo.SaveAll())
                return Ok("Vacancy has been deleted successfully!");

            return BadRequest("Something went wrong...");
        }
    }
}
