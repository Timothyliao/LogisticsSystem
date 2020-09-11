using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MaverickSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "MainPage", id = UrlParameter.Optional }
            );
        }
    }
}
