using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsSystem.DTO;

namespace LogisticsSystem.IBLL
{
    public interface IUserManager
    {
        Task CreateOrder(OrderInfoDto orderInfo);

        Dictionary<Guid, string> GetAllWebSiteLocationByType(string type, string city);

        Task<OrderTraceDto> TrackOrder(string code);
    }
}
