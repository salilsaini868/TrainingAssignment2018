using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrainingProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;

            if (filterContext.Controller is Controller controller)
            {
                if (session["user"] == null)
                {
                    filterContext.Result =
                           new RedirectToRouteResult(
                               new RouteValueDictionary{{ "controller", "Login" },
                                          { "Login", "Login" }
                                   //Action Method, controller
                               });
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
