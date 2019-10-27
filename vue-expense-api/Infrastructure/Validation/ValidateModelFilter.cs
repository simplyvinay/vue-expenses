using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace vue_expense_api.Infrastructure.Validation
{
    public class ValidateModelFilter : IActionFilter
    {
        public void OnActionExecuting(
            ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                var result = new ContentResult();
                var errors = new Dictionary<string, string[]>();

                foreach (var valuePair in filterContext.ModelState)
                {
                    errors.Add(
                        valuePair.Key,
                        valuePair.Value.Errors.Select(x => x.ErrorMessage).ToArray());
                }

                string content = JsonConvert.SerializeObject(new {errors});
                result.Content = content;
                result.ContentType = "application/json";

                filterContext.HttpContext.Response.StatusCode = 422; 
                filterContext.Result = result;
            }
        }

        public void OnActionExecuted(
            ActionExecutedContext filterContext)
        {

        }
    }
}