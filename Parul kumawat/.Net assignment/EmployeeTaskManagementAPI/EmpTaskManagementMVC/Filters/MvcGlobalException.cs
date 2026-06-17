using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace EmpTaskManagementMVC.Filters
{
    public class MvcGlobalException : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode;
            string title;
            if(context.Exception is DbUpdateException)
            {
                statusCode = StatusCodes.Status500InternalServerError;
                title = "Database error";
            }
            else if(context.Exception is UnauthorizedAccessException)
            {
                statusCode = StatusCodes.Status401Unauthorized;
                title = "Unauthorized";
            }
            else if(context.Exception is ArgumentException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                title = "Invalid Request";
            }
            else if(context.Exception is InvalidOperationException)
            {
                statusCode = StatusCodes.Status409Conflict;
                title = "Invalid Operation";
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
                title = "Something went wrong";
            }

            context.Result = new ObjectResult(new
            {
                StatusCode = statusCode,
                Title = title,
                ExceptionMessage = context.Exception.Message
            })
            {
                StatusCode = statusCode,
            };
            context.ExceptionHandled = true;
            
        }
    }
}
