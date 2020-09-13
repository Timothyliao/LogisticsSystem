using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class CargoSenderService : BaseService<CargoSender>, ICargoSenderService
    {
        public CargoSenderService() : base(new LogisticsContext())
        {
        }
    }
}
