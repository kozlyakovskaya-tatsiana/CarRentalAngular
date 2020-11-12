using System;
using System.Linq;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.DTO.DocumentsDto;
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

            CreateMap<Car, CarReadTableInfoDto>();

            CreateMap<IFormFile, Document>()
                .ForMember(doc => doc.Type, opt => opt.MapFrom(file => file.ContentType))
                .ForMember(doc => doc.Name, opt => opt.MapFrom(file => file.FileName));

            CreateMap<CarCreateDto, Car>()
                .ForMember(car => car.Documents, opt => opt.MapFrom(dto => dto.Images))
                .ForMember(car => car.Id, opt => opt.MapFrom(dto => Guid.NewGuid()));

            CreateMap<Car, CarReadWithImagesDto>();

            CreateMap<CarCreatingFormDataRequest, CarCreateDto>();

            CreateMap<CarInfoUpdateRequest, CarInfoDto>();

            CreateMap<CarInfoDto, Car>();

            CreateMap<Car, CarEditImagesForReadDto>()
                .ForMember(dto => dto.CarId, opt => opt.MapFrom(car => car.Id))
                .ForMember(dto => dto.CarName, opt => opt.MapFrom(car => car.Mark  + " " + car.Model))
                .ForMember(dto => dto.Images, opt => opt.MapFrom(car => car.Documents));

            CreateMap<CarAddImagesFormDataRequest, CarAddImagesDto>();

            CreateMap<Document, DocumentDto>();

            CreateMap<Car, CarForSmallCardDto>()
                .ForMember(dto => dto.ImageName, opt => opt.MapFrom(car => car.Documents.FirstOrDefault().Name))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(car => car.Mark + " " + car.Model));
        }
    }
}
