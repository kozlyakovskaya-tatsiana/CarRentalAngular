using CarRental.Service.WebModels.Car;
using FluentValidation;

namespace CarRental.Api.Validators.Car
{
    public class CarCreatingFormDataRequestValidator : AbstractValidator<CarCreatingFormDataRequest>
    {
        public CarCreatingFormDataRequestValidator()
        {
            RuleFor(req => req.Mark).NotEmpty();

            RuleFor(req => req.Model).NotEmpty();

            RuleFor(req => req.Carcase).NotNull().IsInEnum().WithMessage(req => "There is no such carcase");

            RuleFor(req => req.ReleaseYear).NotNull();

            RuleFor(req => req.Transmission).NotNull().IsInEnum().WithMessage(req => "There is no such type of transmission");

            RuleFor(req => req.EnginePower).NotNull();

            RuleFor(req => req.FuelConsumption).NotNull();

            RuleFor(req => req.TankVolume).NotNull();

            RuleFor(req => req.FuelType).NotNull().IsInEnum().WithMessage(req => "There is no such type of fuel");

            RuleFor(req => req.TrunkVolume).NotNull();

            RuleFor(req => req.Images).NotNull();
        }
    }
}
