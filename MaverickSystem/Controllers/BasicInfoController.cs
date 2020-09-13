using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MaverickSystem.Controllers
{
    public class BasicInfoController : Controller
    {
        [Filters.maverickSystemAuth]
        [HttpGet]
        public async Task<ActionResult> OrderView(int pageIndex = 0, int pageSize = 15)
        {
            var basicInfoManager = new LogisticsSystem.BLL.BasicInfoManager();
            List<LogisticsSystem.DTO.OrderListDto> orderList = new List<LogisticsSystem.DTO.OrderListDto>();
            orderList = await basicInfoManager.GetAllOrderViewNoPage();
            foreach (var order in orderList)
            {
                order.OrderId = order.OrderId.Substring(2);
                order.SenderAddress = order.SenderAddress.Split('省', '市')[1];
                order.ReceiverAddress = order.ReceiverAddress.Split('省', '市')[1];
            }
            ViewBag.orderList = orderList;
            ViewBag.DataCount = orderList.Count();
            return View();
        }

        [Filters.maverickSystemAuth]
        [HttpGet]
        public async Task<ActionResult> BookingNote(string id)
        {
            var basicInfoManager = new LogisticsSystem.BLL.BasicInfoManager();
            LogisticsSystem.DTO.BookingNoteInfoDto bookingNote = await basicInfoManager.GetAllOrderMess("96" + id);
            var price = (Convert.ToDouble(bookingNote.Freight) + Convert.ToDouble(bookingNote.InsureFee) + Convert.ToDouble(bookingNote.ServiceFee)).ToString("f2");
            ViewBag.Order = bookingNote;
            ViewBag.TotalPrice = price;
            return View();
        }

        [Filters.maverickSystemAuth]
        [HttpGet]
        public ActionResult Website(string _Name = null)
        {
            var basicInfoManager = new LogisticsSystem.BLL.BasicInfoManager();
            var websites = basicInfoManager.GetWebsite(_Name);
            ViewBag.HasPower = "1";
            return View(websites);
        }
    }
}