using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class WaybillService : BaseService<WayBill>, IWaybillService
    {
        public WaybillService() : base(new LogisticsContext())
        {
        }
    }
}
