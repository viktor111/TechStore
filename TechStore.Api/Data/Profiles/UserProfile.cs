using System;
using AutoMapper;
using TechStore.Api.Data.Enteties;
using TechStore.Models.Models;

namespace TechStore.Api.Data.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
    }
}
