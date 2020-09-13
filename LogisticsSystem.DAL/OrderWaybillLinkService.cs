using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class OrderWaybillLinkService : BaseService<OrderWaybillLink>, IOrderWaybillLinkService
    {
        public OrderWaybillLinkService() : base(new LogisticsContext())
        {
        }
    }
}
