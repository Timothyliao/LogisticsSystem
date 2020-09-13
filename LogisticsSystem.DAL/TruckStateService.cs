using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class TruckStateService : BaseService<TruckState>, ITruckStateService
    {
        public TruckStateService() : base(new LogisticsContext())
        {

        }
    }
}
