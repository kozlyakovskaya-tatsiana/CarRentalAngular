﻿using CarRental.Service.Models;
using FluentValidation;

namespace CarRental.Api.Validators
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        private readonly string _phoneNumberPattern = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
        public RegisterModelValidator()
        {
            RuleFor(register => register.Email).EmailAddress().WithMessage("Incorrect format of email");

            RuleFor(register => register.Password).MinimumLength(5);

            RuleFor(register => register.PasswordConfirm).Must((reg, confirmPassword) => reg.Password == confirmPassword)
                .WithMessage("Passwords must be the same");

            RuleFor(register => register.Name).NotEmpty().MaximumLength(20);

            RuleFor(register => register.Surname).NotEmpty().MaximumLength(20);

            RuleFor(register => register.DateOfBirth).NotNull();

            RuleFor(register => register.PhoneNumber).Matches(_phoneNumberPattern);
        }
    }
}