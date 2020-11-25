using System;
using System.Linq;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.DTO.DocumentDtos;
using CarRental.Service.DTO.RentalPointDtos;
using CarRental.Service.DTO.UserDtos;
using CarRental.Service.WebModels;
using CarRental.Service.WebModels.Car;
using CarRental.Service.WebModels.RentalPoint;
using CarRental.Service.WebModels.User;
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

            CreateMap<Car, CarReadTableInfoDto>()
                .ForMember(dto => dto.RentalPointName, opt => opt.MapFrom(c => c.RentalPoint.Name));

            CreateMap<IFormFile, Document>()
                .ForMember(doc => doc.Type, opt => opt.MapFrom(file => file.ContentType))
                .ForMember(doc => doc.Name, opt => opt.MapFrom(file => file.FileName));

            CreateMap<CarCreateDto, Car>()
                .ForMember(car => car.Documents, opt => opt.MapFrom(dto => dto.Images))
                .ForMember(car => car.Id, opt => opt.MapFrom(dto => Guid.NewGuid()));

            CreateMap<Car, CarReadWithImagesDto>()
                .ForMember(dto => dto.RentalPointName, opt => opt.MapFrom(c => c.RentalPoint.Name));

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

            CreateMap<RentalPointCreateRequest, RentalPointCreateDto>();

            CreateMap<RentalPointEditRequest, RentalPointEditDto>();

            CreateMap<RentalPointCreateDto, RentalPoint>()
                .ForPath(point => point.Location.Address, opt => opt.MapFrom(dto => dto.Address))
                .ForPath(point => point.Location.Lat, opt => opt.MapFrom(dto => dto.Lat))
                .ForPath(point => point.Location.Lng, opt => opt.MapFrom(dto => dto.Lng))
                .ForPath(point => point.Location.City.Name, opt => opt.MapFrom(dto => dto.City))
                .ForPath(point => point.Location.City.Country.Name, opt => opt.MapFrom(dto => dto.Country));

            CreateMap<RentalPointEditDto, RentalPoint>()
                .ForPath(point => point.Location.Address, opt => opt.MapFrom(dto => dto.Address))
                .ForPath(point => point.Location.Lat, opt => opt.MapFrom(dto => dto.Lat))
                .ForPath(point => point.Location.Lng, opt => opt.MapFrom(dto => dto.Lng))
                .ForPath(point => point.Location.City.Name, opt => opt.MapFrom(dto => dto.City))
                .ForPath(point => point.Location.City.Country.Name, opt => opt.MapFrom(dto => dto.Country));

            CreateMap<RentalPoint, RentalPointLocationDto>()
                .ForMember(dto => dto.Address, opt => opt.MapFrom(point => point.Location.Address))
                .ForMember(dto => dto.City, opt => opt.MapFrom(point => point.Location.City.Name))
                .ForMember(dto => dto.Country, opt => opt.MapFrom(point => point.Location.City.Country.Name))
                .ForMember(dto => dto.Lat, opt => opt.MapFrom(point => point.Location.Lat))
                .ForMember(dto => dto.Lng, opt => opt.MapFrom(point => point.Location.Lng));

            CreateMap<RentalPoint, RentalPointTableInfoDto>()
                .ForMember(dto => dto.Address, opt => opt.MapFrom(p => p.Location.Address))
                .ForMember(dto => dto.City, opt => opt.MapFrom(p => p.Location.City.Name))
                .ForMember(dto => dto.Country, opt => opt.MapFrom(p => p.Location.City.Country.Name))
                .ForMember(dto => dto.CarsAmount, opt => opt.MapFrom(p => p.Cars.Count));
        }
    }
}
