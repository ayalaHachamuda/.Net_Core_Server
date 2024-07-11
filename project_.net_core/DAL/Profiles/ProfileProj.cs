using AutoMapper;
using DAL.Dtos;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Profiles
{
    public class ProfileProj:Profile
    {
        public ProfileProj() 
        {
            CreateMap<AdminUser, AdminUserDto>();
            CreateMap<User, UserDto>().ReverseMap(); 
            CreateMap<Competition, CompetitionDto>();
            CreateMap<Recipe, RecipeDto>();
            CreateMap<Vote, VoteDto>();
        }
    }
}
