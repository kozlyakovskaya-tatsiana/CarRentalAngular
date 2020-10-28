using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Service.WebModels.Car;
using FluentValidation;

namespace CarRental.Api.Validators.Car
{
    public class CarCreatingRequestValidator : AbstractValidator<CarCreatingRequest>
    {
        public CarCreatingRequestValidator()
        {
            RuleFor(req => req.Mark).NotEmpty();

            RuleFor(req => req.Model).NotEmpty();

            RuleFor(req => req.Carcase).NotEmpty().IsInEnum().WithMessage(req => "There is no such carcase");

            RuleFor(req => req.ReleaseYear).NotEmpty();

            RuleFor(req => req.Transmission).NotEmpty().IsInEnum().WithMessage(req => "There is no such type of transmission");

            RuleFor(req => req.EnginePower).NotEmpty();

            RuleFor(req => req.FuelConsumption).NotEmpty();

            RuleFor(req => req.TankVolume).NotEmpty();

            RuleFor(req => req.FuelType).NotEmpty().IsInEnum().WithMessage(req => "There is no such type of fuel");

            RuleFor(req => req.TrunkVolume).NotEmpty();
        }
    }
}
