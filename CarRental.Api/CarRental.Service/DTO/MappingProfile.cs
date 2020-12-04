using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using AutoMapper;
using CarRental.DAL.Entities;
using CarRental.Service.DTO.BookingDtos;
using CarRental.Service.DTO.CarDtos;
using CarRental.Service.DTO.DocumentDtos;
using CarRental.Service.DTO.RentalPointDtos;
using CarRental.Service.DTO.UserDtos;
using CarRental.Service.Filter;
using CarRental.Service.WebModels;
using CarRental.Service.WebModels.Booking;
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
            CreateMap<User, UserForRead>();

            CreateMap<UserForRead, User>();

            CreateMap<UserForCreate, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(udto => udto.Email));

            CreateMap<UserForUpdate, User>();

            CreateMap<UserCreatingRequest, UserForCreate>();

            CreateMap<EditUserRequest, UserForUpdate>();

            CreateMap<EditUserBaseRequest, UserBase>();

            CreateMap<UserBase, User>();

            CreateMap<Car, CarTableInfo>()
                .ForMember(dto => dto.RentalPointName, opt => opt.MapFrom(c => c.RentalPoint.Name));

            CreateMap<IFormFile, Document>()
                .ForMember(doc => doc.Type, opt => opt.MapFrom(file => file.ContentType))
                .ForMember(doc => doc.Name, opt => opt.MapFrom(file => file.FileName));

            CreateMap<CarForCreate, Car>()
                .ForMember(car => car.Documents, opt => opt.MapFrom(dto => dto.Images))
                .ForMember(car => car.Id, opt => opt.MapFrom(dto => Guid.NewGuid()));

            CreateMap<Car, CarWithImages>()
                .ForMember(dto => dto.RentalPointName, opt => opt.MapFrom(c => c.RentalPoint.Name));

            CreateMap<CarCreatingRequest, CarForCreate>();

            CreateMap<CarInfoUpdateRequest, CarInfo>();

            CreateMap<CarInfo, Car>();

            CreateMap<Car, CarForEditImages>()
                .ForMember(dto => dto.CarId, opt => opt.MapFrom(car => car.Id))
                .ForMember(dto => dto.CarName, opt => opt.MapFrom(car => car.Mark  + " " + car.Model))
                .ForMember(dto => dto.Images, opt => opt.MapFrom(car => car.Documents));

            CreateMap<CarAddImagesRequest, CarForAddImages>();

            CreateMap<Document, DocumentBaseInfo>();

            CreateMap<Car, CarForSmallCard>()
                .ForMember(dto => dto.ImageName, opt => opt.MapFrom(car => car.Documents.FirstOrDefault().Name))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(car => car.Mark + " " + car.Model));

            CreateMap<RentalPointCreateRequest, RentalPointForCreate>();

            CreateMap<RentalPointEditRequest, RentalPointForEdit>();

            CreateMap<RentalPointForCreate, RentalPoint>()
                .ForPath(point => point.Location.Address, opt => opt.MapFrom(dto => dto.Address))
                .ForPath(point => point.Location.Lat, opt => opt.MapFrom(dto => dto.Lat))
                .ForPath(point => point.Location.Lng, opt => opt.MapFrom(dto => dto.Lng))
                .ForPath(point => point.Location.City.Name, opt => opt.MapFrom(dto => dto.City))
                .ForPath(point => point.Location.City.Country.Name, opt => opt.MapFrom(dto => dto.Country));

            CreateMap<RentalPointForEdit, RentalPoint>()
                .ForPath(point => point.Location.Address, opt => opt.MapFrom(dto => dto.Address))
                .ForPath(point => point.Location.Lat, opt => opt.MapFrom(dto => dto.Lat))
                .ForPath(point => point.Location.Lng, opt => opt.MapFrom(dto => dto.Lng))
                .ForPath(point => point.Location.City.Name, opt => opt.MapFrom(dto => dto.City))
                .ForPath(point => point.Location.City.Country.Name, opt => opt.MapFrom(dto => dto.Country));

            CreateMap<RentalPoint, RentalPointLocation>()
                .ForMember(dto => dto.Address, opt => opt.MapFrom(point => point.Location.Address))
                .ForMember(dto => dto.City, opt => opt.MapFrom(point => point.Location.City.Name))
                .ForMember(dto => dto.Country, opt => opt.MapFrom(point => point.Location.City.Country.Name))
                .ForMember(dto => dto.Lat, opt => opt.MapFrom(point => point.Location.Lat))
                .ForMember(dto => dto.Lng, opt => opt.MapFrom(point => point.Location.Lng));

            CreateMap<RentalPoint, RentalPointTableInfo>()
                .ForMember(dto => dto.Address, opt => opt.MapFrom(p => p.Location.Address))
                .ForMember(dto => dto.City, opt => opt.MapFrom(p => p.Location.City.Name))
                .ForMember(dto => dto.Country, opt => opt.MapFrom(p => p.Location.City.Country.Name))
                .ForMember(dto => dto.CarsAmount, opt => opt.MapFrom(p => p.Cars.Count));

            CreateMap<BookingRequest, BookingInfo>();

            CreateMap<BookingInfo, BookingInfoForRead>()
                .ForMember(read => read.CarImageName, opt => opt.MapFrom(b => b.Car.Documents[0].Name))
                .ForMember(read => read.RentalPointName, opt => opt.MapFrom(b => b.Car.RentalPoint.Name))
                .ForMember(read => read.RentalPointId, opt => opt.MapFrom(b => b.Car.RentalPoint.Id))
                .ForMember(read => read.CarName, opt => opt.MapFrom(b => b.Car.Mark + b.Car.Model))
                .ForMember(read => read.RentalPointAddress, opt =>
                    opt.MapFrom(b => string.Join(",", b.Car.RentalPoint.Location.City.Country.Name,
                        b.Car.RentalPoint.Location.City.Name, b.Car.RentalPoint.Location.Address)))
                .ForMember(read => read.BookingStatusName, 
                    opt => opt.MapFrom(b => b.BookingStatus.GetType().GetMember(b.BookingStatus.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name))
                .ForMember(read => read.BookingId, opt => opt.MapFrom(b => b.Id));

            CreateMap<Country, CountryBaseInfo>()
                .ForMember(info => info.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(info => info.Name, opt => opt.MapFrom(c => c.Name));

            CreateMap<City, CityBaseInfo>()
                .ForMember(info => info.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(info => info.Name, opt => opt.MapFrom(c => c.Name));
        }
    }
}
