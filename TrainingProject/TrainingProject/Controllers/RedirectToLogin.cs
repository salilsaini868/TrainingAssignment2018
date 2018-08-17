using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrainingProject.Controllers
{
    public class RedirectToLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;

            if (filterContext.Controller is Controller controller)
            {
                if (session != null && session["user"] == null)
                {
                    filterContext.Result =
                           new RedirectToRouteResult(
                               new RouteValueDictionary{{ "controller", "Login" },
                                          { "LoginPage", "Login" }
                               });
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }

}