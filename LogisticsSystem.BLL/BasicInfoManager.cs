using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsSystem.DTO;
using LogisticsSystem.IBLL;
using LogisticsSystem.DAL;
using System.Data.Entity;

namespace LogisticsSystem.BLL
{
    public class BasicInfoManager : IBasicInfoManager
    {
        public async Task<BookingNoteInfoDto> GetAllOrderMess(string id)
        {
            var bookingNote = new BookingNoteInfoDto();
            using (var OrderService = new OrderService())
            {
                var order = await OrderService.GetAll().Where(p => p.BarCode == id).FirstOrDefaultAsync();
                if (order != null)
                {
                    bookingNote.CreateTime = order.CreatTime.ToLongDateString() + " " + order.CreatTime.ToShortTimeString();
                    bookingNote.OrderId = order.BarCode;
                    switch (order.Status)
                    {
                        case "1": bookingNote.Stutas = "未处理"; break;
                        case "2": bookingNote.Stutas = "接货中"; break;
                        case "3": bookingNote.Stutas = "配货中"; break;
                        case "4": bookingNote.Stutas = "运输中"; break;
                        case "5": bookingNote.Stutas = "配送中"; break;
                        default:
                            bookingNote.Stutas = "已完成";
                            break;
                    }
                    using (var orderDetailsService = new OrderDetailsService())
                    {
                        var orderDetail = await orderDetailsService.GetAll().Where(p => p.OrderId == order.Id).Include(p => p.Sender).Include(p => p.Receiver).FirstOrDefaultAsync();
                        bookingNote.CargoName = orderDetail.CargoName;
                        bookingNote.CargoUnit = orderDetail.UitNum;
                        bookingNote.CargoVolumn = (Convert.ToInt32(orderDetail.UitNum) * Convert.ToDouble(orderDetail.CargoVolume)).ToString();
                        bookingNote.CargoWeight = (Convert.ToInt32(orderDetail.UitNum) * Convert.ToDouble(orderDetail.CargoWeight)).ToString();
                        bookingNote.PayType = orderDetail.PayType ? "现付" : "到付";
                        bookingNote.Mark = orderDetail.Mark;
                        bookingNote.GetGoodsTime = orderDetail.GetGoodsTime;
                        bookingNote.Freight = orderDetail.Freight.ToString("f2");
                        bookingNote.ServiceFee = orderDetail.ServiceCharge.ToString("f2");

                        bookingNote.SenderName = orderDetail.Sender.Name;
                        bookingNote.SMobilePhone = orderDetail.Sender.MobliePhone;
                        bookingNote.SFirm = orderDetail.Sender.FirmName;
                        bookingNote.STel = orderDetail.Sender.TelPhone;
                        bookingNote.SAddress = orderDetail.Sender.Provinces + orderDetail.Sender.DetailAddress;

                        bookingNote.ReceiverName = orderDetail.Receiver.Name;
                        bookingNote.RMobilePhone = orderDetail.Receiver.MobliePhone;
                        bookingNote.RFirm = orderDetail.Receiver.FirmName;
                        bookingNote.RTel = orderDetail.Receiver.TelPhone;
                        bookingNote.RAddress = orderDetail.Receiver.Provinces + orderDetail.Receiver.DetailAddress;

                        if (!orderDetail.IsInsured)
                        {
                            bookingNote.InsureFee = "0";
                            bookingNote.Value = "0";
                        }
                        else
                        {
                            using (var insuranceInfoService = new InsuranceInfoService())
                            {
                                var insurance = await insuranceInfoService.GetAll().Where(p => p.OrderId == order.Id).FirstOrDefaultAsync();
                                bookingNote.InsureFee = insurance.Fee.ToString("f2");
                                bookingNote.Value = insurance.Value;
                            }
                        }

                        if (order.StartWebsiteId != new Guid())
                        {
                            using (var websiteInfoService = new WebsiteInfoService())
                            {
                                var website = await websiteInfoService.GetAll().Where(p => p.Id == order.StartWebsiteId).FirstOrDefaultAsync();
                                bookingNote.WebSiteName = website.SiteName;
                            }
                        }
                        else
                            bookingNote.WebSiteName = "";
                    }
                    return bookingNote;
                }
                else
                    return null;
            }
        }

        public async Task<List<OrderListDto>> GetAllOrderView(int pageIndex, int pageSize)
        {
            using (var orderDetailsService = new OrderDetailsService())
            {
                var list = await orderDetailsService.GetAllByPageOrder(pageSize, pageIndex, false)
                    .Include(p => p.Order).Include(p => p.Sender).Include(p => p.Receiver)
                    .Select(p => new OrderListDto()
                    {
                        OrderId = p.Order.BarCode,
                        CargoName = p.CargoName,
                        CargoWeight = p.CargoWeight,
                        CargoVolume = p.CargoVolume,
                        UnitNUm = p.UitNum,
                        InsuredValue = p.IsInsured ? "1" : "0",
                        Status = p.Order.Status,
                        SenderAddress = p.Sender.Provinces + p.Sender.DetailAddress,
                        ReceiverAddress = p.Receiver.Provinces + p.Receiver.DetailAddress,
                        SenderId = p.SenderId,
                        ReceiverId = p.ReceiverId,
                    }).ToListAsync();
                return list;
            }
        }

        public async Task<List<OrderListDto>> GetAllOrderViewNoPage()
        {
            using (var orderDetailsService = new OrderDetailsService())
            {
                var list = await orderDetailsService.GetAllOrder(false)
                    .Include(p => p.Order).Include(p => p.Sender).Include(p => p.Receiver)
                    .Select(p => new OrderListDto()
                    {
                        OrderId = p.Order.BarCode,
                        CargoName = p.CargoName,
                        CargoWeight = p.CargoWeight,
                        CargoVolume = p.CargoVolume,
                        UnitNUm = p.UitNum,
                        InsuredValue = p.IsInsured ? "1" : "0",
                        Status = p.Order.Status,
                        SenderAddress = p.Sender.Provinces + p.Sender.DetailAddress,
                        ReceiverAddress = p.Receiver.Provinces + p.Receiver.DetailAddress,
                        SenderId = p.SenderId,
                        ReceiverId = p.ReceiverId,
                    }).ToListAsync();
                return list;
            }
        }

        public int GetOrderCount()
        {
            using (var orderService = new OrderService())
            {
                return orderService.GetAll().Count();
            }
        }

        public List<WebsiteInfoLookDto> GetWebsite(string websiteName = null)
        {
            using (var websiteInfoService = new WebsiteInfoService())
            {
                var websites = websiteInfoService.GetAll().Select(p => new WebsiteInfoLookDto()
                {
                    Name = p.SiteName,
                    Province = p.Province,
                    Address = p.Address,
                    ChargeManTel = p.ChargeManTel,
                    ChargeMan = p.ChargeMan,
                    Type = p.Type,
                    CreateTime = p.CreatTime,
                    Id = p.Id
                }).ToList();
                if (string.IsNullOrEmpty(websiteName))
                    return websites;
                else
                    return websites.Where(p => p.Name == websiteName).ToList();
            }
        }
    }
}
