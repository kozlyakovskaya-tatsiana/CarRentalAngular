using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Service.WebModels.Booking;
using FluentValidation;

namespace CarRental.Api.Validators.Booking
{
    public class BookingRequestValidator : AbstractValidator<BookingRequest>
    {
        public BookingRequestValidator()
        {
            RuleFor(req => req.PersonName).NotEmpty();

            RuleFor(req => req.PersonSurname).NotEmpty();

            RuleFor(req => req.PersonDateOfBirth).NotNull();

            RuleFor(req => req.PersonPhoneNumber).NotEmpty();

            RuleFor(req => req.PersonPassportSerialNumber).NotEmpty();

            RuleFor(req => req.PersonPassportId).NotEmpty();

            RuleFor(req => req.EndDateOfRenting).NotNull();

            RuleFor(req => req.StartDateOfRenting).NotNull();
        }
    }
}
