using System.Net;
using Newtonsoft.Json;
using Splitit.App.Exceptions;

namespace Splitit.App.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidOperationAppException ex)
            {
                _logger.LogError(ex, "InvalidOperationException occurred.");
                await HandleExceptionAsync(context, ex);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                _logger.LogError(ex, "Out of range.");
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhadled Exception throw.");
                await HandleExceptionAsync(context, ex);
            }
        }
        
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error",
                Detailed = exception.Message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private Task HandleExceptionAsync(HttpContext context, ArgumentOutOfRangeException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.RequestedRangeNotSatisfiable;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Out of range",
                Detailed = exception.Message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private Task HandleExceptionAsync(HttpContext context, InvalidOperationAppException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Conflict;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Invalid operation",
                Detailed = exception.Message
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }

}

