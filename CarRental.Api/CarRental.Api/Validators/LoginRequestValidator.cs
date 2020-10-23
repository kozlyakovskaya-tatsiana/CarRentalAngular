using CarRental.Service.Identity;
using CarRental.Service.WebModels;
using FluentValidation;

namespace CarRental.Api.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator(IUserManagementService userService)
        {
            RuleFor(login => login.Email).EmailAddress().WithMessage("Incorrect format of email")
                .Must((logModel, login) => userService.IsUserExists(logModel.Email, logModel.Password).Result)
                .WithMessage("Credentials are incorrect.");
        }
    }
}
