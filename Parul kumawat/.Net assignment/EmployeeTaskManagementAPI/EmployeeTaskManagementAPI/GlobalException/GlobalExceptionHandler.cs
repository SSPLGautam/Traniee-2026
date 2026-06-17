using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EmployeeTaskManagementAPI.GlobalException
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //_logger.LogError(exception, "Exception: {Message}", exception.Message);

            //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //httpContext.Response.ContentType = "application/json";

            var response = new ErrorResponse();

            if(exception is DbUpdateException)
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Title = "Database error";
            }
            else if(exception is UnauthorizedAccessException)
            {
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Title = "Unauthorized";
            }
            else if(exception is ArgumentException)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Title = "Invalid Request";
            }
            else if (exception is InvalidOperationException)
            {
                response.StatusCode = StatusCodes.Status409Conflict;
                response.Title = "Invalid Operation";
            }
            else
            {
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Title = "Something went wrong";
            }
            response.ExceptionMessage = exception.Message;
            httpContext.Response.StatusCode = response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response);

            return true;
        }
    }
}
