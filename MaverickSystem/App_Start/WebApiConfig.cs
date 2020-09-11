using System.Web.Http;

namespace MaverickSystem.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "dataApi/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}