using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TalentHunt.Data;
using TalentHunt.Dtos;
using TalentHunt.Helpers;
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
        private readonly IWebHostEnvironment _host;
        private readonly ApplicationSettings _applicationSettings;
        public ApplicationController(IApplicationRepository applicationRespository, IMapper mapper,
                                     UserManager<User> userManager, ISharedRepository sharedRepository,
                                     IWebHostEnvironment host, IOptionsSnapshot<ApplicationSettings> options)
        {
            _repo = applicationRespository;
            _mapper = mapper;
            _user = userManager;
            _sharedRepo = sharedRepository;
            _host = host;
            _applicationSettings = options.Value;
        }


        // Post: api/applications/uploadCv
        [HttpPut("{applicationId}/uploadcv")]
        public async Task<IActionResult> UploadCv(int applicationId, IFormFile file)
        {
            // file upload //
            if (file == null)
                return BadRequest("No file selected");
            if (file.Length == 0)
                return BadRequest("You selected empty file.");
            if (file.Length > _applicationSettings.MaxBytes)
                return BadRequest("Maximum limit for file size exceeded");
            if (!_applicationSettings.AcceptedTypes.Any(t => t == Path.GetExtension(file.FileName)))
            {
                List<string> accepted = new List<string>();
                foreach (var value in _applicationSettings.AcceptedTypes)
                {
                    accepted.Add(value);
                }
                return BadRequest("This file type is not acceptable.");
            }
                

            var application = await _repo.GetApplication(applicationId);
            if (application == null)
                return BadRequest("Unknown application submition...");

            var uploadFolderPath = Path.Combine(_host.WebRootPath, "uploads"); // Path require system.io

            // create directory if doesn't exists
            // we don't want to create this folder manually in deploying
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            // unique file name with same extension as valid uploaded file
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            application.CvName = fileName;
            await _sharedRepo.SaveAll();

            // set path for this file
            var filePath = Path.Combine(uploadFolderPath, fileName);

            // now use stream to read the file and store inside folder
            using (var stream = new FileStream(filePath /*path if file*/, FileMode.Create /*mode of file*/))
            {
                await file.CopyToAsync(stream); // now file read and stored inside folder.
            }

            return Ok("You have successfully applied for the job! We will contact you soon after initial screening.");
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

            // check if user has already applied for this job or not
            var user = await _user.FindByIdAsync(userId);
            var isApply = await _repo.IsApply(user, model.VacancyId);
            if (isApply == true)
                return BadRequest("You have already applied for this job.");

            if (!ModelState.IsValid)
                return BadRequest("Something went wrong...");

            var modelFields = await _repo.ValidateApplicationFields(model);
            if (modelFields == null)
                return BadRequest("Form has invalid data.");

            var application = _mapper.Map<Application>(modelFields);
            application.AppUser = user;

            _sharedRepo.Add(application);

            if (await _sharedRepo.SaveAll())
                return Ok( new {
                    applicationId = application.Id
                });

            return BadRequest("Something went wrong...");

        }
    }
}