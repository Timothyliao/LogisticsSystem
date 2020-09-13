using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class WebsiteOperateLogService : BaseService<WebSiteOperateLog>, IWebsiteOperateLogService
    {
        public WebsiteOperateLogService() : base(new LogisticsContext())
        {
        }
    }
}
