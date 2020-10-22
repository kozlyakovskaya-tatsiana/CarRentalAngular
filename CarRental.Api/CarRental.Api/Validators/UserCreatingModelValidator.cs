using CarRental.Service.WebModels;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Api.Validators
{
    public class UserCreatingModelValidator : AbstractValidator<UserCreatingRequest>
    {
        private readonly string _phoneNumberPattern = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";

        public UserCreatingModelValidator(RoleManager<IdentityRole> roleManager)
        {
            RuleFor(model => model.Name).NotEmpty().MaximumLength(20);

            RuleFor(model => model.Surname).NotEmpty().MaximumLength(20);

            RuleFor(model => model.DateOfBirth).NotNull();

            RuleFor(model => model.PhoneNumber).NotEmpty().Matches(_phoneNumberPattern);

            RuleFor(model => model.Email).NotEmpty().EmailAddress().WithMessage("Incorrect format of email");

            RuleFor(model => model.Password).NotEmpty().MinimumLength(5);

            RuleFor(model => model.Role).NotEmpty();
        }
    }
}
