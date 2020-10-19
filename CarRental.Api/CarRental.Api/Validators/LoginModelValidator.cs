using CarRental.Service;
using CarRental.Service.Identity;
using CarRental.Service.Models;
using FluentValidation;

namespace CarRental.Api.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator(IAuthorizeService authorizeService)
        {
            RuleFor(login => login.Email).EmailAddress().WithMessage("Incorrect format of email")
           .Must((logModel, login) => authorizeService.IsUserCanLogin(logModel).Result)
           .WithMessage("There is no such user in the system.");
        }
    }
}
