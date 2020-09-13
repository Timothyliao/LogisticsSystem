using System.Web.Mvc;
using System.Web.Routing;

namespace MaverickSystem.Filters
{
    public class maverickSystemAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //当用户存储在cookie中值，且session数据为空时，把cookie中的数据同步到session中
            if (filterContext.HttpContext.Session["userAccount"] == null && filterContext.HttpContext.Request.Cookies["userId"] != null)
            {
                filterContext.HttpContext.Session["userAccount"] = filterContext.HttpContext.Request.Cookies["userAccount"].Value;
                filterContext.HttpContext.Session["userAccount"] = filterContext.HttpContext.Request.Cookies["userId"].Value;
            }

            if (filterContext.HttpContext.Session["loginName"] == null && filterContext.HttpContext.Request.Cookies["userId"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary() {
                    {"controller","Staff" },
                    {"action","Login" }
                });
            }
        }
    }
}