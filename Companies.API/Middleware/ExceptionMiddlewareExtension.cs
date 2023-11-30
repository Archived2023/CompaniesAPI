using Companies.API.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace Companies.API.Middleware
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseConfigureExceptionHandler(this IApplicationBuilder app)
        {



            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        var problemDetailsFactory = app.ApplicationServices.GetRequiredService<ProblemDetailsFactory>();
                        ProblemDetails problemDetails = default!;

                        switch (contextFeature.Error)
                        {
                            case CompanyNotFoundException companyNotFoundException:
                                problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    StatusCodes.Status404NotFound,
                                    companyNotFoundException.Title,
                                    detail: companyNotFoundException.Message);
                                break;
                            default:
                                problemDetails = problemDetailsFactory.CreateProblemDetails(
                                    context,
                                    StatusCodes.Status500InternalServerError,
                                    "Internal server error"
                                    );

                                break;
                        }

                        context.Response.StatusCode = (int)problemDetails.Status!;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsJsonAsync(problemDetails);
                    }

                });
            });
        }
    }
}
