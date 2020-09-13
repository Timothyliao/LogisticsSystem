using System.Web.Mvc;

namespace LogisticsSystem.Tools
{
    public class SessionValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext) => new SessionValueProvider();
    }
}
