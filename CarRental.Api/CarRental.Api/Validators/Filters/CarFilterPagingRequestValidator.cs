using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Service.WebModels.Car;
using FluentValidation;

namespace CarRental.Api.Validators.Filters
{
    public class CarFilterPagingRequestValidator : AbstractValidator<CarFilterPagingRequest>
    {
        public CarFilterPagingRequestValidator()
        {
            RuleFor(req => req.PageNumber).NotNull().GreaterThanOrEqualTo(1);

            RuleFor(req => req.PageSize).NotNull().GreaterThanOrEqualTo(1);
        }
    }
}
