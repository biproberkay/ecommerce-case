using Berkay.ECommerceCase.Persistance.Wrappers;
using System.Net;
using System.Text.Json;

namespace Berkay.ECommerceCase.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        /// <summary>
        /// This delegate indicates Http. I mean Hypertext transfer protocol <see cref="Microsoft.AspNetCore.Http.RequestDelegate"/>
        /// </summary>
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            //Headers are read-only, response has already started.
            //httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = ChooseCode(exception);
            //var x = JsonSerializer.Serialize(exception);
            var responseBodyObject = await CustomResult<string>.FailAsync(exception.Message+"\n---\nsee inner exception:\n"+exception.InnerException?.Message);
            var responseBodyString = JsonSerializer.Serialize(responseBodyObject);
            //var x = httpContext.Request.Body.
            await httpContext.Response.WriteAsync(responseBodyString);
        }

        private static int ChooseCode(Exception exception)
        {
            return exception switch
            {
                OperationCanceledException => (int)HttpStatusCode.RequestTimeout,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                FormatException => (int)HttpStatusCode.BadRequest,
                IndexOutOfRangeException => (int)HttpStatusCode.BadRequest,
                InvalidOperationException => (int)HttpStatusCode.BadRequest,
                NullReferenceException => (int)HttpStatusCode.BadRequest,
                NotImplementedException => (int)HttpStatusCode.NotImplemented,
                FileNotFoundException => (int)HttpStatusCode.NotFound,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,

                _ => (int)HttpStatusCode.InternalServerError,
            };
        }
    }
}
