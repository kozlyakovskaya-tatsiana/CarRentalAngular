﻿using CarRental.Service.Identity;
using CarRental.Service.WebModels;
using FluentValidation;

namespace CarRental.Api.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginRequest>
    {
        public LoginModelValidator(IUserService userService)
        {
            RuleFor(login => login.Email).EmailAddress().WithMessage("Incorrect format of email")
           .Must((logModel, login) => userService.IsUserExists(logModel.Email, logModel.Password).Result)
           .WithMessage("Credentials are incorrect.");
        }
    }
}
