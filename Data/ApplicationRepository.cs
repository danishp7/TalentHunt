using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Data
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly TalentHuntContext _ctx;
        public ApplicationRepository(TalentHuntContext talentHuntContext)
        {
            _ctx = talentHuntContext;
        }

        public async Task<Application> GetApplication(int id)
        {
            var app = await _ctx.Applications.SingleOrDefaultAsync(a => a.Id == id);
            if (app == null)
                return null;
            return app;
        }

        public async Task<bool> IsApply(User user, int vacancyId)
        {
            var application = await _ctx.Applications
                .Where(a => a.AppUser == user && a.VacancyId == vacancyId)
                .FirstOrDefaultAsync();

            if (application == null)
                return false;
            return true;
        }

        public async Task<ApplicationDto> ValidateApplicationFields(ApplicationDto model)
        {
            if (await _ctx.Vacancies.FindAsync(model.VacancyId) == null)
                return null;

            if (string.IsNullOrWhiteSpace(model.ApplicantName) || string.IsNullOrWhiteSpace(model.ApplicantEmail) ||
                string.IsNullOrWhiteSpace(model.ApplicantAddress) || string.IsNullOrWhiteSpace(model.ApplicantContactNumber))
                return null;

            if (model.DegreeEndDate.HasValue)
            {
                if(model.DegreeEndDate <= model.DegreeStartDate)
                    return null;
            }

            if (model.EmploymentStartDate.HasValue)
            {
                if (model.EmploymentEndDate.HasValue)
                {
                    if (model.EmploymentEndDate <= model.EmploymentStartDate)
                        return null;
                }
            }

            if (model.EmploymentId != 0)
            {
                if (await _ctx.Employments.FindAsync(model.EmploymentId) == null)
                    return null;
            }

            // check educationlevelid, employmentid, institute id
            if (await _ctx.EducationLevels.FindAsync(model.EducationLevelId) == null)
                return null;

            if (await _ctx.Institutes.FindAsync(model.InstituteId) == null)
                return null;

            return model;
        }
    }
}
