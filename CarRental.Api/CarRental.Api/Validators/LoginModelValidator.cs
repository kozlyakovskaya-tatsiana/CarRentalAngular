using CarRental.Service;
using CarRental.Service.Identity;
using CarRental.Service.Models;
using FluentValidation;

namespace CarRental.Api.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        private readonly IAuthorizeService _authorizeService;

        public LoginModelValidator(IAuthorizeService authorizeService)
        {
            _authorizeService = authorizeService;

            RuleFor(login => login.Email).EmailAddress().WithMessage("Incorrect format of email")
                .Must((logModel, login) => authorizeService.IsUserCanLogin(logModel).Result)
                .WithMessage("There is no such user in the system.");
        }
    }
}
