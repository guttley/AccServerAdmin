using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AccServerAdmin.Service.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorDetails = GetErrorDetails(context.Response, exception);
            context.Response.StatusCode = errorDetails.StatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(errorDetails.ToString());
        }

        private static ErrorDetails GetErrorDetails(HttpResponse response, Exception error)
        {
            if (error is KeyNotFoundException)
            {
                return new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = error.Message
                };
            }

            return new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Internal Server Error."
            };
        }

    }
}
