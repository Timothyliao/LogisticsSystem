using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class WaybillTransportLinkService : BaseService<WaybillTransportLink>, IWaybillTransportLinkService
    {
        public WaybillTransportLinkService() : base(new LogisticsContext())
        {
        }
    }
}
