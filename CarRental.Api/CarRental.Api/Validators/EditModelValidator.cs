using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Service.Models;
using CarRental.Service.WebModels;
using FluentValidation;

namespace CarRental.Api.Validators
{
    public class EditModelValidator : AbstractValidator<EditModel>
    {
        private readonly string _phoneNumberPattern = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";

        public EditModelValidator()
        {
            RuleFor(model => model.Id).NotEmpty();

            RuleFor(model => model.Name).NotEmpty().MaximumLength(20);

            RuleFor(model => model.Surname).NotEmpty().MaximumLength(20);

            RuleFor(model => model.DateOfBirth).NotNull();

            RuleFor(model => model.PhoneNumber).Matches(_phoneNumberPattern);

            RuleFor(model => model.Email).EmailAddress().WithMessage("Incorrect format of email");
        }
    }
}
