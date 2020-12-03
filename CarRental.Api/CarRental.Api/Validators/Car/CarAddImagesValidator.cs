using CarRental.Service.WebModels.Car;
using FluentValidation;

namespace CarRental.Api.Validators.Car
{
    public class CarAddImagesValidator : AbstractValidator<CarAddImagesRequest>
    {
        public CarAddImagesValidator()
        {
            RuleFor(req => req.Images).NotNull();

            RuleFor(req => req.CarId).NotNull();
        }
    }
}
