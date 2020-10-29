using CarRental.Service.WebModels;
using FluentValidation;

namespace CarRental.Api.Validators.User
{
    public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
    {
        private readonly string _phoneNumberPattern = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";

        public EditUserRequestValidator()
        {
            RuleFor(model => model.Id).NotEmpty();

            RuleFor(model => model.Name).NotEmpty().MaximumLength(20);

            RuleFor(model => model.Surname).NotEmpty().MaximumLength(20);

            RuleFor(model => model.DateOfBirth).NotNull();

            RuleFor(model => model.PhoneNumber).NotEmpty().Matches(_phoneNumberPattern);

            RuleFor(model => model.Role).NotEmpty();
        }
    }
}
