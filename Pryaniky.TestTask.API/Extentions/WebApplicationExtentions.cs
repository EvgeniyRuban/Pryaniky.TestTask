using Microsoft.AspNetCore.Diagnostics;
using Pryaniky.TestTask.Domain.Exceptions;

namespace Pryaniky.TestTask.API.Extentions
{
    public static class WebApplicationExtentions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILogger logger)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            EntityNotFoundException => StatusCodes.Status404NotFound,
                            ProductContainsInOrderException => StatusCodes.Status400BadRequest,
                            ProductInUseException => StatusCodes.Status400BadRequest,
                            ProductUnexistsInOrderException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        logger.LogError("Request Method: {method}; Path: {path}; Query: {query}", context.Request.Method, context.Request.Path, context.Request.QueryString.Value);
                        await context.Response.WriteAsJsonAsync(
                            new
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message
                            });
                    }
                });
            });
        }
    }
}
