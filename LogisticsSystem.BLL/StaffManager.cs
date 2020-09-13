using System;
using LogisticsSystem.IBLL;
using LogisticsSystem.IDAL;
using System.Data.Entity;
using LogisticsSystem.Tools;
using LogisticsSystem.DTO;
using System.Linq;
using LogisticsSystem.DAL;
using System.Threading.Tasks;
using LogisticsSystem.Model;
using System.Collections.Generic;

namespace LogisticsSystem.BLL
{
    public class StaffManager : IStaffManager
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="tel">账号（电话）</param>
        /// <param name="password">新密码</param>
        /// <param name="key">秘钥（用于加解密）</param>
        /// <returns></returns>
        public async Task ChangePwd(string tel, string password, string key)
        {
            using (IStaffInfoService staffInfoService = new StaffInfoService())
            {
                var staff = await staffInfoService.GetAll().Where(p => p.Tel == tel).FirstAsync();
                staff.Password = StringEncryptAndDecrypt.AESEncrypt(password, key);
                await staffInfoService.EditAsync(staff);
            }
        }

        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="staffId">创建者Id</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public async Task CreateAuthCode(Guid staffId, string code)
        {
            using (IAuthCodeService authCodeService = new AuthCodeService())
            {
                await authCodeService.CreateAsync(new AuthCode()
                {
                    StaffId = staffId,
                    Code = code
                });
            }
        }

        public List<WebSiteInfoDto> GetAllWebSite()
        {
            using (var websiteInfoService = new WebsiteInfoService())
            {
                return websiteInfoService.GetAllOrder().Where(p => p.Type == "1").Select(p => new WebSiteInfoDto()
                {
                    Name = p.SiteName,
                    Location = p.Location,
                    Address = p.Address
                }).ToList();
            }
        }

        public List<string> GetInitalInfo(string tel)
        {
            using (StaffInfoService staffInfoService = new StaffInfoService())
            {
                var staff = staffInfoService.GetAll().Where(p => p.Tel == tel).FirstOrDefault();
                return new List<string>()
                {
                    staff.Name,
                    staff.Position,
                    staff.ImagePath
                };
            }
        }

        public List<OrderListDto> GetOrderByWebsite(out Guid id, out string websiteLocation, string name = null, string type = null)
        {
            using (var websiteServicce = new WebsiteInfoService())
            {
                if (name == null || type == null)
                {
                    id = new Guid();
                    websiteLocation = null;
                    return null;
                }
                else
                {
                    var website = websiteServicce.GetAll().Where(p => p.SiteName == name).FirstOrDefault();
                    Guid _id = website.Id;
                    websiteLocation = website.Location;
                    using (var orderDetailsService = new OrderDetailsService())
                    {
                        var orderData = orderDetailsService.GetAllOrder(false).Include(p => p.Order).Include(p => p.Sender).Include(p => p.Receiver)
                            .Where(p => p.Order.Status == type && p.Order.StartWebsiteId == _id)
                            .Select(p => new OrderListDto()
                            {
                                OrderId = p.Order.BarCode.Substring(2),
                                SenderAddress = p.Sender.DetailAddress,
                                ReceiverAddress = p.Receiver.Provinces,
                                Time = p.GetGoodsTime,
                                Location = type == "1" ? p.Sender.Location : p.Receiver.Location,
                                Income = p.Freight,
                                CargoWeight = p.CargoWeight,
                                CargoVolume = p.CargoVolume,
                                UnitNUm = p.UitNum
                            }).ToList();
                        id = _id;
                        return orderData;
                    }
                }
            }
        }

        /// <summary>
        /// 根据ID得到部门名称
        /// </summary>
        /// <param name="id">部门ID</param>
        /// <returns>部门名称</returns>
        public string GetSectionNameById(Guid id)
        {
            using (ISectionService sectionService = new SectionService())
            {
                return sectionService.GetAll().Where(p => p.Id == id).FirstOrDefault().Name;
            }
        }

        /// <summary>
        /// 根据账号得到职员基础信息
        /// </summary>
        /// <param name="tel">账号(电话)</param>
        /// <returns></returns>
        public StaffSimpleInfoDto GetSTaffInfoByTel(string tel)
        {
            using (IStaffInfoService staffInfoService = new StaffInfoService())
            {
                var staff = staffInfoService.GetAll().FirstOrDefault(p => p.Tel == tel);
                if (staff != null)
                {
                    return new StaffSimpleInfoDto()
                    {
                        Id = staff.Id,
                        Name = staff.Name,
                        Tel = staff.Tel,
                        Email = staff.Email,
                        Status = staff.Status,
                        SectionName = GetSectionNameById(staff.SectionId)
                    };
                }
                else
                    return null;
            }
        }

        public List<TruckInfoDto> GetTruckByWebsite(Guid? id)
        {
            if (id == null)
                return null;
            else
            {
                using (var truckInfoService = new TruckInfoService())
                {
                    var trucks = truckInfoService.GetAll().Where(p => p.WebsiteId == id);
                    List<TruckInfoDto> truckInfo = new List<TruckInfoDto>();
                    using (var truckStateService = new TruckStateService())
                    {
                        foreach (TruckInfo truck in trucks)
                        {
                            if (truckStateService.GetAll().Where(p => p.TruckId == truck.Id).FirstOrDefault().Status == "2")
                                truckInfo.Add(new TruckInfoDto()
                                {
                                    LicencePlate = truck.LicencePlate,
                                    Load = truck.Load,
                                    ContainerLength = truck.ContainerLength,
                                    ContainerWidth = truck.ContainerWidth,
                                    ContainerHeight = truck.ContainerHeight,
                                    TruckHeight = truck.TruckHeight,
                                    TruckWidth = truck.TruckWidth,
                                    TruckId = truck.Id,
                                    TruckSize = truck.TruckSize,
                                    TruckWeight = truck.TruckWeight,
                                    PlateProvince = truck.PlateProvince,
                                    AxlesNum = truck.AxlesNum
                                });
                        }
                    }

                    return truckInfo;
                }
            }
        }

        /// <summary>
        /// 冻结账号
        /// </summary>
        /// <param name="tel">账号(电话)</param>
        /// <returns></returns>
        public async Task LockAccount(string tel)
        {
            using (IStaffInfoService staffInfoService = new StaffInfoService())
            {
                var staff = staffInfoService.GetAll().Where(p => p.Tel == tel).FirstOrDefault();
                if (staff != null)
                {
                    staff.Status = false;
                    await staffInfoService.EditAsync(staff);
                }
            }
        }

        /// <summary>
        /// 职员登陆
        /// </summary>
        /// <param name="account">账号(电话)</param>
        /// <param name="password">密码</param>
        /// <param name="key">秘钥(用于加解密)</param>
        /// <param name="userId">职员id(用于返回)</param>
        /// <returns></returns>
        public bool Login(string account, string password, string key, out Guid userId)
        {
            var pwd = StringEncryptAndDecrypt.AESEncrypt(password, key);
            using (IStaffInfoService staffInfoService = new StaffInfoService())
            {
                var user = staffInfoService.GetAll().FirstOrDefaultAsync(p => p.Tel == account && p.Password == pwd);//user在此是一个异步方法
                user.Wait();//等待user执行结束
                var data = user.Result;//拿到user执行的结果
                if (data != null)
                {
                    userId = data.Id;
                    return true;
                }
                else
                {
                    userId = new Guid();
                    return false;
                }
            }
        }

        /// <summary>
        /// 生成运货单
        /// </summary>
        /// <param name="code">货物id</param>
        /// <param name="truckId">卡车id</param>
        /// <returns></returns>
        public async Task<bool> ToWayBill(string code, Guid truckId)
        {
            //查询状态是否正确--->修改订单中的状态--->生成waybill--->生成waybillLink
            bool flag = true;
            Order order;
            using (var orderservice = new OrderService())
            {
                order = orderservice.GetAll().Where(p => p.BarCode == code).FirstOrDefault();
                if (order == null || order.Status != "2") flag = false;
            }

            if (flag)
            {
                var context = new LogisticsContext();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //修改订单的状态
                        order.Status = "3";
                        context.Entry(order).State = EntityState.Modified;
                        await context.SaveChangesAsync();

                        //创建waybill
                        WayBill wayBill = new WayBill()
                        {
                            FinishTime = DateTime.Now,
                            PlanPath = "NULL"
                        };
                        context.Set<WayBill>().Add(wayBill);
                        await context.SaveChangesAsync();

                        //创建truck waybill联系
                        WaybillTransportLink transportLink = new WaybillTransportLink()
                        {
                            TransportInfoId = truckId,
                            WayBillId = wayBill.Id
                        };
                        context.Set<WaybillTransportLink>().Add(transportLink);
                        await context.SaveChangesAsync();

                        //创建order waybill联系
                        OrderWaybillLink orderWaybillLink = new OrderWaybillLink()
                        {
                            OrderId = order.Id,
                            WaybillId = wayBill.Id
                        };
                        context.Set<OrderWaybillLink>().Add(orderWaybillLink);
                        await context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception errer)
                    {
                        transaction.Rollback();
                        flag = false;
                        throw errer;
                    }
                    finally
                    {
                        context.Dispose();
                    }
                }
            }

            return flag;
        }

        /// <summary>
        /// 解封职员账号(管理员操作)
        /// </summary>
        /// <param name="tel">账号(电话)</param>
        /// <param name="_operatorId">操作员的ID</param>
        /// <returns></returns>
        public async Task UnlockAccount(string tel, Guid _operatorId)
        {
            using (IStaffInfoService staffInfoService = new StaffInfoService())
            {
                var staff = staffInfoService.GetAll().FirstOrDefault(p => p.Tel == tel);
                if (staff != null && !staff.Status)
                {
                    staff.Status = true;
                    await staffInfoService.CreateAsync(staff);
                    using (IAccountOperateLogService accountOperateLogService = new AccountOperateLogService())
                    {
                        await accountOperateLogService.CreateAsync(new AccountOperateLog()
                        {
                            OperatorId = _operatorId,
                            ModifiedId = staff.Id,
                            OPerateType = "2" //代表update操作
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 规划路径后更新订单状态信息
        /// </summary>
        /// <param name="orderIdList">需要更新的订单Id列表</param>
        /// <param name="truckId">配送的货车Id</param>
        /// <param name="type">1:更新为接货状态    2：更新为配送状态</param>
        /// <returns></returns>
        public async Task UpdateOrderStatus(string[] orderIdList, Guid truckId, string type)
        {
            string status = type == "1" ? "2" : "4";
            using (var orderService = new OrderService())
            {
                for (int i = 0, len = orderIdList.Length; i < len; i++)
                {
                    string code = "96" + orderIdList[i];
                    var order = orderService.GetAll().Where(p => p.BarCode == code).FirstOrDefault();
                    order.Status = status;
                    await orderService.EditAsync(order, false);
                }
                await orderService.Save();
            }

            using (var truckStateService = new TruckStateService())
            {
                var truck = truckStateService.GetAll().Where(p => p.TruckId == truckId).FirstOrDefault();
                truck.Status = "1";
                await truckStateService.EditAsync(truck);
            }
        }

        /// <summary>
        /// 验证验证码的正确性
        /// </summary>
        /// <param name="tel">账号</param>
        /// <param name="code">待验证的code</param>
        /// <param name="key">秘钥(用于加解密)</param>
        /// <returns></returns>
        public async Task<bool> VerifyCode(string tel, string code, string key)
        {
            var staffId = GetSTaffInfoByTel(tel).Id;
            using (IAuthCodeService authCodeService = new AuthCodeService())
            {
                var authCode = await authCodeService.GetAllOrder(false).FirstOrDefaultAsync();
                if (StringEncryptAndDecrypt.AESEncrypt(code, key) == authCode.Code)
                {
                    if (DateTime.Now.AddMinutes(-3) <= authCode.CreatTime)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }


    }
}
