using System;
using CarRental.DAL.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

            var statusCode = context.Error switch
            {
                SecurityTokenExpiredException tokenExpired => 401,
                NotFoundException notFound => 404,
                _ => 500,
            };

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message,
                statusCode: statusCode);
        }

        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
