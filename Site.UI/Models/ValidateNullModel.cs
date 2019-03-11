using Site.UI.Core;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Site.UI.Models
{
    public class ValidateNullModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            foreach (var item in actionContext.ActionArguments)
            {
                if (!object.Equals(item.Value, null))
                    return;
            }

            actionContext.Response = actionContext.Request.CreateErrorMessageResponse(error_description: "Model is required.");
        }
    }
}