using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Service.Models;
using CarRental.Service.WebModels;

namespace CarRental.Service.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserShowDto>();

            CreateMap<UserShowDto, User>();

            CreateMap<UserCreateDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(udto => udto.Email));


            CreateMap<UserCreatingModel, UserCreateDto>();

            CreateMap<EditModel, UserEditDto>();

        }
    }
}
