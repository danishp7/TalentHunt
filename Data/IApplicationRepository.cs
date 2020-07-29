using System.Collections.Generic;
using System.Threading.Tasks;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Data
{
    public interface IApplicationRepository
    {
        Task<ApplicationDto> ValidateApplicationFields(ApplicationDto model);
    }
}