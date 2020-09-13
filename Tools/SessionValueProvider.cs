using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace LogisticsSystem.Tools
{
    public class SessionValueProvider : IValueProvider
    {
        public bool ContainsPrefix(string prefix) => HttpContext.Current.Session[prefix] != null;
        public ValueProviderResult GetValue(string key)
        {
            if (HttpContext.Current.Session[key] == null)
                return null;

            return new ValueProviderResult(
                HttpContext.Current.Session[key],
                HttpContext.Current.Session[key].ToString(),
                CultureInfo.CurrentCulture);
        }
    }
}
