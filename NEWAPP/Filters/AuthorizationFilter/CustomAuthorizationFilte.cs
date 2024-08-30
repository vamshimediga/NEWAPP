using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NEWAPP.Filters.AuthorizationFilter
{
    public class CustomAuthorizationFilte : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Example logic: Check if the user is authenticated
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                // User is not authenticated, redirect to login page
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
