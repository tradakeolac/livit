namespace Livit.Web.Infrastructure.ErrorHandling
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Service.Exceptions;
    using System;
    using System.Net;

    public static class ErrorConfigurationExtensions
    {
        public static void UseContentNegotiateExceptionHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(
             builder =>
             {
                 builder.Run(
                 async context =>
                 {
                     context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                     context.Response.ContentType = "application/json";
                     var ex = context.Features.Get<IExceptionHandlerFeature>();
                     if (ex != null)
                     {
                         var err = JsonConvert.SerializeObject(CreateModel(ex));
                         await context.Response.WriteAsync(err).ConfigureAwait(false);
                     }
                 });
             }
            );
        }

        private static ErrorDataModel CreateModel(IExceptionHandlerFeature exceptionFeature)
        {
            var businessException = GetBusinessException(exceptionFeature);

            return new ErrorDataModel
            {
                Message = "Internal server error occurred while processing the request!",
                Details = businessException.Message,
                Code = businessException.Code,
                Type = businessException.Type,
                HelpLink = businessException.HelpLink,
                DateTime = DateTime.UtcNow
            };
        }

        private static BusinessException GetBusinessException(IExceptionHandlerFeature exceptionFeature)
        {
            if (exceptionFeature != null && exceptionFeature.Error != null && exceptionFeature.Error is BusinessException)
            {
                return (BusinessException)exceptionFeature.Error;
            }

            return new UnKnowBusinessException("Internal server error occurred.");
        }
    }
}