using System.Web.Mvc;
using System.Web.Routing;

namespace AjdemeSi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{language}/{controller}/{action}/{id}",
                defaults: new { language = "en", controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "AjdemeSi.Controllers" }
            );
        }
    }
}
