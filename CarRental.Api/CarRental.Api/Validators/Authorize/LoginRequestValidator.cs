using CarRental.Service.Identity;
using CarRental.Service.WebModels.Authorize;
using FluentValidation;

namespace CarRental.Api.Validators.Authorize
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator(IUserManagementService userService)
        {
            RuleFor(login => login.Email).NotEmpty().EmailAddress().WithMessage("Incorrect format of email")
                .Must((logModel, login) => userService.IsUserExists(logModel.Email, logModel.Password).Result)
                .WithMessage("Credentials are incorrect.");
        }
    }
}
