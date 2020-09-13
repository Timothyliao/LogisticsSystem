using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class WebsiteInfoService : BaseService<WebsiteInfo>, IWebsiteInfoService
    {
        public WebsiteInfoService() : base(new LogisticsContext())
        {
        }
    }
}
