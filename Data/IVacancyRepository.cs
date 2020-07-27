using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Data
{
    public interface IVacancyRepository
    {
        PostVacancyDto ValidateVacancy(PostVacancyDto postVacancyDto);
        Task<Vacancy> GetVacancy(int id, User loggedInUser);
        Task<IEnumerable<Vacancy>> GetVacancies(User loggedInUser);
        ICollection<Responsibility> GetResponsibilites(int id);
        Task<bool> IsVacancy(string vacancyName);
    }
}