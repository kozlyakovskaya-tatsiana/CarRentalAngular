using System;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.DAL.Enums;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.DTO.UserDtos;
using CarRental.Service.WebModels;
using CarRental.Service.WebModels.Car;


namespace CarRental.Service.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserReadDto>();

            CreateMap<UserReadDto, User>();

            CreateMap<UserCreateDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(udto => udto.Email));

            CreateMap<UserUpdateDto, User>();

            CreateMap<UserCreatingRequest, UserCreateDto>();

            CreateMap<EditUserRequest, UserUpdateDto>();

            CreateMap<EditUserBaseRequest, UserDtoBase>();

            CreateMap<UserDtoBase, User>();

            CreateMap<CarDtoBase, Car>();

            CreateMap<Car, CarDtoBase>();

            CreateMap<CarCreatingRequest, CarDtoBase>();

            CreateMap<Car, CarReadDto>();
        }
    }
}
