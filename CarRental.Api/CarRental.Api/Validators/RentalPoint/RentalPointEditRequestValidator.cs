using CarRental.Service.WebModels.RentalPoint;
using FluentValidation;

namespace CarRental.Api.Validators.RentalPoint
{
    public class RentalPointEditRequestValidator : AbstractValidator<RentalPointEditRequest>
    {
        public RentalPointEditRequestValidator()
        {
            RuleFor(req => req.Id).NotEmpty();

            RuleFor(req => req.Country).NotEmpty();

            RuleFor(req => req.City).NotEmpty();

            RuleFor(req => req.Address).NotEmpty();

            RuleFor(req => req.Lat).NotNull();

            RuleFor(req => req.Lng).NotNull();

            RuleFor(req => req.Name).NotEmpty();
        }
    }
}
