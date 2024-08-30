using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace NEWAPP.Filters.ExceptionFilter
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Check if the exception is of a specific type or handle all exceptions
            if (context.Exception is InvalidOperationException)
            {
                // Handle the exception
                context.Result = new ContentResult
                {
                    StatusCode = 400,
                    Content = "An error occurred during processing your request."
                };
            }
            else
            {
                // Handle other exceptions
                context.Result = new ContentResult
                {
                    StatusCode = 500,
                    Content = "An unexpected error occurred."
                };
            }

            // Optionally, log the exception
            // LogError(context.Exception);

            // Mark the exception as handled
            context.ExceptionHandled = true;
        }
    }
}
