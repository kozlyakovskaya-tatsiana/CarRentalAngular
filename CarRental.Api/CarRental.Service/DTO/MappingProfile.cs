using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Service.Models;

namespace CarRental.Service.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();

            CreateMap<RegisterModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(reg => reg.Email));

            CreateMap<EditModel, User>();

            CreateMap<UserCreatingModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(model => model.Email)); ;
        }
    }
}
