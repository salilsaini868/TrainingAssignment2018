using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrainingProject.Views.Shared
{
    public static class Layout
    {
        public static string IsSelected(this HtmlHelper html, string controllers = "", string cssClass = "selected")
        {
            ViewContext viewContext = html.ViewContext;
            bool isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
                viewContext = html.ViewContext.ParentActionViewContext;

            RouteValueDictionary routeValues = viewContext.RouteData.Values;            
            string currentController = routeValues["controller"].ToString();

            if (String.IsNullOrEmpty(controllers))
                controllers = currentController;

            string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

            return acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }
    }
}