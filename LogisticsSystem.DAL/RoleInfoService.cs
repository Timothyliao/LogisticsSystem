using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
        public RoleInfoService() : base(new LogisticsContext())
        {
        }
    }
}
