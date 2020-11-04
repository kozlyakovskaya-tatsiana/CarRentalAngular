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

            CreateMap<IFormFile, ImageFile>()
                .ForMember(img => img.Name, opt => opt.MapFrom(file => file.FileName));

            CreateMap<IFormFile, Image>()
                .ForMember(img => img.Name, opt => opt.MapFrom(file => file.FileName));

            CreateMap<CarCreatingFormDataRequest, CarCreateDto>();

            CreateMap<CarCreateDto, Car>()
                .ForMember(car => car.MainImageFile, opt => opt.MapFrom(dto => dto.MainImageFile))
                .ForMember(car => car.MainImage, opt => opt.MapFrom(dto => dto.MainImageFile));

            CreateMap<Car, CarReadWithImageDto>()
                .ForMember(carDto => carDto.PathToMainImage, opt => opt.MapFrom(car => car.MainImageFile.Path))
                .ForMember(carDto => carDto.ImageDataUrl, opt => opt.MapFrom(car =>  $"data:image/jpg;base64,{Convert.ToBase64String(car.MainImage.ImageDataUrl)}"));
        }
    }
}
