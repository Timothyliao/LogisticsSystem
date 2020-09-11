using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsSystem.DTO;

namespace LogisticsSystem.IBLL
{
    public interface IBasicInfoManager
    {
        Task<List<OrderListDto>> GetAllOrderView(int pageIndex, int pageSize);

        Task<List<OrderListDto>> GetAllOrderViewNoPage();

        int GetOrderCount();

        Task<BookingNoteInfoDto> GetAllOrderMess(string id);

        List<WebsiteInfoLookDto> GetWebsite(string websiteName = null);
    }
}
