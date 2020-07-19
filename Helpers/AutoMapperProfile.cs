using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterUserDto, User>().ReverseMap();
            CreateMap<LogInUserDto, User>().ReverseMap();

            CreateMap<PostVacancyDto, Vacancy>().ReverseMap();

            CreateMap<ExperienceDto, Experience>().ReverseMap();

            CreateMap<JobPostDto, JobPost>().ReverseMap();

            CreateMap<ResponsibilityDto, Responsibility>().ReverseMap();

            CreateMap<KeyResponsibilityDto, KeyResponsibility>().ReverseMap();

            CreateMap<DetailedVacancyDto, Vacancy>().ReverseMap();

            CreateMap<ConciseVacancyDto, Vacancy>().ReverseMap();
        }
    }
}
