using CarRental.Service.WebModels;
using FluentValidation;

namespace CarRental.Api.Validators
{
    public class EditUserBaseRequestValidator : AbstractValidator<EditUserBaseRequest>
    {
        private readonly string _phoneNumberPattern = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";

        public EditUserBaseRequestValidator()
        {
            RuleFor(model => model.Id).NotEmpty();

            RuleFor(model => model.Name).NotEmpty().MaximumLength(20);

            RuleFor(model => model.Surname).NotEmpty().MaximumLength(20);

            RuleFor(model => model.DateOfBirth).NotNull();

            RuleFor(model => model.PhoneNumber).Matches(_phoneNumberPattern);
        }
    }
}
