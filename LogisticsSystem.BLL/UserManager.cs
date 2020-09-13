using LogisticsSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogisticsSystem.IBLL;
using LogisticsSystem.DAL;

namespace LogisticsSystem.BLL
{
    public class UserManager : IUserManager
    {
        /// <summary>
        /// 用户在线下单
        /// </summary>
        /// <param name="orderInfo">订单信息</param>
        /// <returns></returns>
        public async Task CreateOrder(OrderInfoDto orderInfo)
        {
            var context = new Model.LogisticsContext();

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    //创建订单
                    var order = new Model.Order()
                    {
                        BarCode = orderInfo.BarCode,
                        Region = orderInfo.Region,
                        StartWebsiteId = orderInfo.StartWebsiteId
                    };
                    context.Set<Model.Order>().Add(order);
                    await context.SaveChangesAsync();

                    //添加寄件人信息
                    var cargoSender = new Model.CargoSender()
                    {
                        OrderId = order.Id,
                        Name = orderInfo.SName,
                        MobliePhone = orderInfo.SMobliePhone,
                        Provinces = orderInfo.SProvince,
                        DetailAddress = orderInfo.SAddress,
                        PostCode = orderInfo.SPostCode,
                        TelPhone = orderInfo.STelPhone,
                        FirmName = orderInfo.SFirmName,
                        Location = orderInfo.SLocation
                    };
                    context.Set<Model.CargoSender>().Add(cargoSender);
                    await context.SaveChangesAsync();

                    //添加收件人信息
                    var cargoReceiver = new Model.CargoReceiver()
                    {
                        OrderId = order.Id,
                        Name = orderInfo.RName,
                        MobliePhone = orderInfo.RMobliePhone,
                        Provinces = orderInfo.RProvince,
                        DetailAddress = orderInfo.RAddress,
                        PostCode = orderInfo.RPostCode,
                        TelPhone = orderInfo.RTelPhone,
                        FirmName = orderInfo.RFirmName,
                        Location = orderInfo.RLocation
                    };
                    context.Set<Model.CargoReceiver>().Add(cargoReceiver);
                    await context.SaveChangesAsync();

                    //创建订单明细表
                    var orderDetaile = new Model.OrderDetails()
                    {
                        OrderId = order.Id,
                        Freight = Convert.ToDecimal(orderInfo.Freight),
                        IsInsured = orderInfo.IsInsured,
                        CargoName = orderInfo.CargoName,
                        CargoWeight = orderInfo.CargoWeight,
                        CargoVolume = orderInfo.CargoVolume,
                        UitNum = orderInfo.UitNum,
                        PayType = orderInfo.PayType,
                        Mark = orderInfo.Mark,
                        SenderId = cargoSender.Id,
                        ReceiverId = cargoReceiver.Id,
                        GetGoodsTime = orderInfo.TakeTime,
                        ServiceCharge = Convert.ToDecimal(orderInfo.ServiceCharge)
                    };
                    context.Set<Model.OrderDetails>().Add(orderDetaile);
                    await context.SaveChangesAsync();

                    if (orderInfo.IsInsured)
                    {
                        var insure = new Model.InsuranceInfo()
                        {
                            OrderId = order.Id,
                            InsurerId = cargoSender.Id,
                            Value = orderInfo.CargoValue,
                            Risk = orderInfo.Risk,
                            Fee = decimal.Parse(Tools.MaverickCost.CalcInSurenceFee(Convert.ToDouble(orderInfo.CargoValue)))
                        };
                        context.Set<Model.InsuranceInfo>().Add(insure);
                        await context.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception errer)
                {
                    transaction.Rollback();
                    throw errer;
                }
                finally
                {
                    context.Dispose();
                }
            }
        }

        public Dictionary<Guid, string> GetAllWebSiteLocationByType(string type, string city)
        {
            using (var websiteInfoService = new WebsiteInfoService())
            {
                var webLocations = new Dictionary<Guid, string>();
                var websites = websiteInfoService.GetAll().Where(p => p.Type == type && p.Province.Contains(city));
                foreach (var website in websites)
                    webLocations.Add(website.Id, website.Location);
                return webLocations;
            }
        }

        public async Task<OrderTraceDto> TrackOrder(string code)
        {
            var traceInfo = new OrderTraceDto();

            try
            {
                //得到订单信息
                using (OrderService orderService = new OrderService())
                {
                    var order = orderService.GetAllOrder().Where(p => p.BarCode == code).FirstOrDefault();
                    traceInfo.Code = code;
                    traceInfo.OStatus = order.Status;
                    traceInfo.OrderId = order.Id;

                    using (OrderDetailsService orderDetailsService = new OrderDetailsService())
                    {
                        var odetail = orderDetailsService.GetAll().Where(p=>p.OrderId == order.Id).FirstOrDefault();
                        if (odetail == null)
                            return null;

                        //寄方信息
                        using (CargoSenderService senderService = new CargoSenderService())
                        {
                            var sender = await senderService.GetOneById(odetail.SenderId);

                            if (sender == null)
                                return null;

                            traceInfo.SFAddress = sender.Provinces;
                            traceInfo.SSAddress = sender.DetailAddress;
                            traceInfo.SLocation = sender.Location;
                            traceInfo.SName = sender.Name;
                            traceInfo.STel = sender.MobliePhone;
                        }

                        //收方信息
                        using (CargoReceiverService receiverService = new CargoReceiverService())
                        {
                            var receiver = await receiverService.GetOneById(odetail.ReceiverId);

                            if (receiver == null)
                                return null;

                            traceInfo.RFAddress = receiver.Provinces;
                            traceInfo.RSAddress = receiver.DetailAddress;
                            traceInfo.RLocation = receiver.Location;
                            traceInfo.RName = receiver.Name;
                            traceInfo.RTel = receiver.MobliePhone;
                        }
                    }
                }

                //得到汽车信息
                using (OrderWaybillLinkService orderWaybillLinkService = new OrderWaybillLinkService())
                {
                    var owlink = orderWaybillLinkService.GetAll().Where(p => p.OrderId == traceInfo.OrderId).FirstOrDefault();

                    if (owlink == null)
                        return null;

                    using (WaybillTransportLinkService waybillTransportLinkService = new WaybillTransportLinkService())
                    {
                        var waybill = waybillTransportLinkService.GetAll().Where(p=>p.WayBillId == owlink.WaybillId).FirstOrDefault() ;

                        if (waybill == null)
                            return null;

                        using (TruckTraceService truckTrace = new TruckTraceService())
                        {
                            var truck = truckTrace.GetAll().Where(p=>p.truckId == waybill.TransportInfoId).OrderByDescending(p => p.CreatTime).FirstOrDefault();

                            if (truck == null)
                                return null;

                            traceInfo.TransLocation = truck.Location;
                        }
                    }

                }

                //得到追踪详细
                using (OrderTraceService orderTraceService = new OrderTraceService())
                {
                    var statusList = orderTraceService.GetAll().Where(p => p.OrderId == traceInfo.OrderId).OrderBy(p => p.CreatTime);

                    if (statusList == null)
                        return null;

                    var packegeList = new List<PackegeStatus>();
                    foreach (var item in statusList)
                    {
                        packegeList.Add(new PackegeStatus()
                        {
                            Data = item.CreatTime.ToShortDateString(),
                            Time = item.CreatTime.ToShortTimeString(),
                            Mark = item.Mark
                        });
                    }
                    traceInfo.StatusList = packegeList;
                }

            }
            catch (Exception err)
            {
                return null;
            }

            return traceInfo;
        }
    }
}
