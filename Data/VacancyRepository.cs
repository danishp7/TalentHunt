﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Data
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly TalentHuntContext _ctx;
        private readonly ISharedRepository _sharedRepo;
        public VacancyRepository(TalentHuntContext context, ISharedRepository sharedRepository)
        {
            _ctx = context;
            _sharedRepo = sharedRepository;
        }

        public ICollection<Responsibility> GetResponsibilites(int vacId)
        {
            var resp = _ctx.KeyResponsibilities.Where(r => r.VacancyId == vacId).ToList();
            
            if (resp.Count() == 0)
                return null;

            List<Responsibility> responsibilities = new List<Responsibility>();
            List<int> respIds = new List<int>();

            for (int i = 0; i < resp.Count(); i++)
            {
                respIds.Add(resp[i].ResponsibilityId);
                responsibilities.Add(_ctx.Responsibilities.SingleOrDefault(r => r.Id == respIds[i]));
            }

            //for (int i = 0; i < respIds.Count(); i++)
            //{
            //    responsibilities.Add(_ctx.Responsibilities.SingleOrDefault(r => r.Id == respIds[i]));
            //}
            return responsibilities;
        }

        public async Task<IEnumerable<Vacancy>> GetVacancies(User loggedInUser)
        {
            var vacancies = await _ctx.Vacancies.Where(u => u.AppUser.Id == loggedInUser.Id).ToListAsync();
            if (vacancies == null)
                return null;

            return vacancies;
        }

        public async Task<Vacancy> GetVacancy(int id, User loggedInUser)
        {
            var vacancy = await _ctx.Vacancies
                .Include(r => r.KeyResponsibilities)
                .Include(e => e.Experience)
                .Include(ed => ed.Education)
                .Where(u => u.AppUser.Id == loggedInUser.Id)
                .SingleOrDefaultAsync(v => v.Id == id);
            if (vacancy == null)
                return null;

            return vacancy;

        }

        public PostVacancyDto ValidateVacancy(PostVacancyDto vacancy)
        {
            vacancy.Title = vacancy.Title.ToLower();

            if (string.IsNullOrWhiteSpace(vacancy.Title) || string.IsNullOrWhiteSpace(vacancy.Description) ||
                vacancy.Experience.Years < 0 || string.IsNullOrWhiteSpace(vacancy.Experience.Description) || 
                vacancy.Salary <= 0 || string.IsNullOrWhiteSpace(vacancy.Education.Title) || 
                string.IsNullOrWhiteSpace(vacancy.Education.Description))
                return null;

            if (vacancy.KeyResponsibilities.Count() == 0)
                return null;

            foreach (var responsibility in vacancy.KeyResponsibilities)
            {
                if (string.IsNullOrWhiteSpace(responsibility.Responsibility.Title) ||
                    string.IsNullOrWhiteSpace(responsibility.Responsibility.Description))
                    return null;
            }

            return vacancy;
        }

        public async Task<bool> IsVacancy(string vacancyName)
        {
            var isTitle = await _ctx.Vacancies.FirstOrDefaultAsync(v => v.Title == vacancyName);
            if (isTitle != null)
                return true;
            return false;
        }
    }
}
