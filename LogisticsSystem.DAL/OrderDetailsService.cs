using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class OrderDetailsService : BaseService<OrderDetails>, IOrderDetailsService
    {
        public OrderDetailsService() : base(new LogisticsContext())
        {
        }
    }
}
