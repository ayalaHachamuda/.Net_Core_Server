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
            CreateMap<AdminUser, AdminUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap(); 
            CreateMap<Competition, CompetitionDto>().ReverseMap();
            CreateMap<Recipe, RecipeDto>().ReverseMap();
            CreateMap<Vote, VoteDto>().ReverseMap();
        }
    }
}
