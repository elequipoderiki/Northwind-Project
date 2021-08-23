using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Northwind.WebApi.GlobalErrorHandling
{
    public static class ExceptionMiddlewareExtensions
    {
        //static class mean it must be composed solely of static 
        //members and cannot be subclased
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
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
                await context.Response.WriteAsync(new ErrorDetails()
                {
                  StatusCode = context.Response.StatusCode,
                  Message = "Internal Server Error."
                }.ToString());
              }
            });
          });
        }
    }
}