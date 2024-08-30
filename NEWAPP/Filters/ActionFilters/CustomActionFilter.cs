using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace NEWAPP.Filters.ActionFilters
{
    public class CustomActionFilter:IActionFilter
    {
        private readonly ICommonService _commonService;

        public CustomActionFilter(ICommonService commonService)
        {
            _commonService = commonService;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _commonService.ExecutedCommonTask();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //if (!context.ModelState.IsValid)
            //{
            //    // Handle the case where the model state is not valid.
            //    // For example, you can set the result to a view that displays the validation errors.
            //    context.Result = new BadRequestObjectResult(context.ModelState);
            //    return;
            //}
            _commonService.ExecuteingCommonTask();
        }
    }
}
