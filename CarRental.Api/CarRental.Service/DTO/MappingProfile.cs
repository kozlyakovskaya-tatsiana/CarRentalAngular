using System;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.DTO.UserDtos;
using CarRental.Service.WebModels;
using CarRental.Service.WebModels.Car;
using Microsoft.AspNetCore.Http;

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

            CreateMap<Car, CarReadDto>();

            CreateMap<IFormFile, Document>()
                .ForMember(doc => doc.Name, opt => opt.MapFrom(file => file.FileName))
                .ForMember(doc => doc.Type, opt => opt.MapFrom(file => file.ContentType));

            CreateMap<CarCreateDto, Car>()
                .ForMember(car => car.Documents, opt => opt.MapFrom(dto => dto.Images))
                .ForMember(car => car.Id, opt => opt.MapFrom(dto => Guid.NewGuid()));

            CreateMap<Car, CarReadWithImageDto>();

            CreateMap<CarCreatingFormDataRequest, CarCreateDto>();
        }
    }
}
