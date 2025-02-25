using Contracts;
using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CompanyEmployees.Extensions
{
    public static class ExceptionMiddleWareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var ContextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (ContextFeature != null)
                    {
                        context.Response.StatusCode = ContextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        logger.LogError($"Something went wrong: {ContextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = ContextFeature.Error.Message,
                        }.ToString());
                    }
                });
            });
        }
    }
}
