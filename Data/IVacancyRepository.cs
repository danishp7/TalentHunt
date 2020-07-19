using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Data
{
    public interface IVacancyRepository
    {
        Task<PostVacancyDto> ValidateVacancy(PostVacancyDto postVacancyDto);
        Task<Vacancy> GetVacancy(int id);
        Task<IEnumerable<Vacancy>> GetVacancies();
        ICollection<Responsibility> GetResponsibilites(int id);

    }
}