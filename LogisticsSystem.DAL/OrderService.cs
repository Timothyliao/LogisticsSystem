using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        public OrderService() : base(new LogisticsContext())
        {
        }
    }
}
