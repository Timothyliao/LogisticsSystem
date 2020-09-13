using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class TruckInfoService : BaseService<TruckInfo>, ITruckInfoService
    {
        public TruckInfoService() : base(new LogisticsContext())
        {
        }
    }
}
