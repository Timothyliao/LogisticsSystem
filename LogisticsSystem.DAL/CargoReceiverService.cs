using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class CargoReceiverService : BaseService<CargoReceiver>, ICargoReceiverService
    {
        public CargoReceiverService() : base(new LogisticsContext())
        {
        }
    }
}
