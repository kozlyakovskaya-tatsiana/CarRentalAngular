using CarRental.Service.WebModels.Authorize;
using FluentValidation;

namespace CarRental.Api.Validators.Authorize
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        private readonly string _phoneNumberPattern = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
        public RegisterRequestValidator()
        {
            RuleFor(register => register.Email).NotEmpty().EmailAddress().WithMessage("Incorrect format of email");

            RuleFor(register => register.Password).NotEmpty().MinimumLength(5);

            RuleFor(register => register.PasswordConfirm).NotEmpty().Must((reg, confirmPassword) => reg.Password == confirmPassword)
                .WithMessage("Passwords must be the same");

            RuleFor(register => register.Name).NotEmpty().MaximumLength(20);

            RuleFor(register => register.Surname).NotEmpty().MaximumLength(20);

            RuleFor(register => register.DateOfBirth).NotNull();

            RuleFor(register => register.PhoneNumber).NotEmpty().Matches(_phoneNumberPattern);
        }
    }
}
