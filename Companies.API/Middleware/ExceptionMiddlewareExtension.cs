using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if(contextFeature != null)
                    {
                        var problemDetails = new ProblemDetails
                        {
                            Status = context.Response.StatusCode,
                            Title = contextFeature.Error.Message
                        };

                        await context.Response.WriteAsJsonAsync(problemDetails);
                    }

                });
            });
        }
    }
}
