using System;
using CarRental.DAL.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var errorType = context.Error.GetType();

            var statusCode = errorType == typeof(NotFoundException) ? 404 : 500;

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message,
                statusCode: statusCode);
        }

        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
