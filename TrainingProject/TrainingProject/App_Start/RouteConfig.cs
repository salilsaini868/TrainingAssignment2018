using System.Web.Mvc;
using System.Web.Routing;

namespace TrainingProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Dashboard",
                 url: "Dashboard",
                defaults: new { controller = "Dashboard", action = "Index" }
             );

            routes.MapRoute(
                name: "Product",
                url: "Product",
                defaults: new { controller = "Product", action = "Listing" }
             );

            routes.MapRoute(
                 name: "Category",
                 url: "Category",
                 defaults: new { controller = "Category", action = "Listing" }
             );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "LoginPage", id = UrlParameter.Optional }
            );
        }
    }
}
