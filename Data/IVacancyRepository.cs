using System.Collections.Generic;
using System.Threading.Tasks;
using TalentHunt.Dtos;
using TalentHunt.Helpers;
using TalentHunt.Models;

namespace TalentHunt.Data
{
    public interface IVacancyRepository
    {
        PostVacancyDto ValidateVacancy(PostVacancyDto postVacancyDto);
        Task<Vacancy> GetVacancy(int id, User loggedInUser);
        Task<PagedList<Vacancy>> GetVacancies(User loggedInUser, Params userParams);
        ICollection<Responsibility> GetResponsibilites(int id);
        Task<bool> IsVacancy(string vacancyName);
    }
}