using IgcRestApi.DataConversion;
using IgcRestApi.Exceptions;
using IgcRestApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace IgcRestApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            var dataConverter = AutoMapperDataConverter.GetDataConverter();

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {

                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        CoreApiExceptionModel coreApiExceptionModel = null;

                        if (contextFeature.Error is CoreApiException coreApiException)
                        {
                            context.Response.StatusCode = (int)coreApiException.StatusCode;
                            coreApiExceptionModel = dataConverter.Convert<CoreApiExceptionModel>(coreApiException);
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            coreApiExceptionModel = new CoreApiExceptionModel()
                            {
                                StatusCode = HttpStatusCode.InternalServerError,
                                Message = contextFeature.Error.Message
                            };

                        }


                        await context.Response.WriteAsync(coreApiExceptionModel.ToString());

                    }
                });
            });
        }
    }
}
