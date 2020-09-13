using LogisticsSystem.Model;

namespace LogisticsSystem.DAL
{
    public class OrderTraceService : BaseService<Model.OrderTrace>, IDAL.IOrderTraceService
    {
        public OrderTraceService() : base(new LogisticsContext())
        {
        }
    }
}
