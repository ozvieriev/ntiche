using Site.UI.Core;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
namespace Site.UI.Models
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                string errorDescription = null;
                foreach (var value in actionContext.ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errorDescription = error.ErrorMessage;
                        break;
                    }

                    if (!string.IsNullOrEmpty(errorDescription))
                        break;
                }

                actionContext.Response = actionContext.Request.CreateErrorMessageResponse(error_description: errorDescription);
            }
        }
    }
}