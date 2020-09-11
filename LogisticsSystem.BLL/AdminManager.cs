using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogisticsSystem.IBLL;
using LogisticsSystem.IDAL;
using LogisticsSystem.DTO;
using System.Data.Entity;
using System.Linq;
using LogisticsSystem.DAL;
using LogisticsSystem.Tools;
using System.Data.SqlClient;

namespace LogisticsSystem.BLL
{
    public class AdminManager : IAdminManager
    {
        /// <summary>
        /// 增加多个角色
        /// </summary>
        /// <param name="names">角色名</param>
        /// <returns></returns>
        public async Task AddGroupRole(string[] names)
        {
            using (IRoleInfoService roleInfoService = new RoleInfoService())
            {
                for (int i = 0; i < names.Length; i++)
                    await roleInfoService.CreateAsync(new Model.RoleInfo() { Name = names[i] }, false);
                await roleInfoService.Save();
            }
        }

        /// <summary>
        /// 增加多个职员
        /// </summary>
        /// <param name="staffInfos">职员信息</param>
        /// <param name="operatorId">操作员id</param>
        /// <param name="key">秘钥</param>
        /// <param name="originalRoleName">角色名</param>
        /// <returns></returns>
        public async Task AddGroupStaff(List<StaffBsicInfoDto> staffInfos, Guid operatorId, string key, string originalRoleName)
        {
            List<Guid> modifyIds = new List<Guid>();

            using (IStaffInfoService staffInfoService = new StaffInfoService())
            {
                foreach (var staffInfo in staffInfos)
                {
                    var staff = new Model.StaffInfo()
                    {
                        Name = staffInfo.Name,
                        Tel = staffInfo.Tel,
                        Password = StringEncryptAndDecrypt.AESEncrypt(staffInfo.Tel, key),
                        Email = staffInfo.Email,
                        Address = staffInfo.Address,
                        IdCard = staffInfo.IdCard,
                        ImagePath = staffInfo.ImagePath,
                        SectionId = staffInfo.SectionId,
                        Position = staffInfo.Position
                    };
                    await staffInfoService.CreateAsync(staff, false);
                    modifyIds.Add(staff.Id);
                }
                await staffInfoService.Save();
            }

            using (IAccountOperateLogService accountOperateLogService = new AccountOperateLogService())
            {
                using (IStaffPowerInfoService staffPowerInfoService = new StaffPowerInfoService())
                {
                    using (IRoleInfoService roleInfoService = new RoleInfoService())
                    {

                        foreach (var modifyId in modifyIds)
                        {
                            await accountOperateLogService.CreateAsync(new Model.AccountOperateLog()
                            {
                                OperatorId = operatorId,
                                ModifiedId = modifyId,
                                OPerateType = ('1').ToString()
                            }, false);

                            //初始权限
                            await staffPowerInfoService.CreateAsync(new Model.StaffPowerInfo()
                            {
                                StaffId = modifyId,
                                RoleId = (await roleInfoService.GetAll().Where(p => p.Name == originalRoleName).FirstAsync()).Id //得到对应权限的Id
                            }, false);
                        }

                        //一起更新
                        await accountOperateLogService.Save();
                        await staffPowerInfoService.Save();
                    }
                }
            }
        }

        /// <summary>
        /// 增加一个职员
        /// </summary>
        /// <param name="staffInfo">职员信息</param>
        /// <param name="operatorId">操作员id</param>
        /// <param name="key">秘钥</param>
        /// <param name="originalRoleName">角色名</param>
        /// <returns></returns>
        public async Task AddOneStaff(StaffBsicInfoDto staffInfo, Guid operatorId, string key, string originalRoleName)
        {
            using (IStaffInfoService staffInfoService = new StaffInfoService())
            {
                var staff = new Model.StaffInfo()
                {
                    Name = staffInfo.Name,
                    Tel = staffInfo.Tel,
                    Password = StringEncryptAndDecrypt.AESEncrypt(staffInfo.Tel, key),
                    Email = staffInfo.Email,
                    Address = staffInfo.Address,
                    IdCard = staffInfo.IdCard,
                    ImagePath = staffInfo.ImagePath,
                    SectionId = staffInfo.SectionId,
                    Position = staffInfo.Position
                };
                await staffInfoService.CreateAsync(staff);

                using (IAccountOperateLogService accountOperateLogService = new AccountOperateLogService())
                {
                    await accountOperateLogService.CreateAsync(new Model.AccountOperateLog()
                    {
                        OperatorId = operatorId,
                        ModifiedId = staff.Id,
                        OPerateType = "1"
                    });
                }

                //初始权限
                using (IStaffPowerInfoService staffPowerInfoService = new StaffPowerInfoService())
                {
                    using (IRoleInfoService roleInfoService = new RoleInfoService())
                    {
                        await staffPowerInfoService.CreateAsync(new Model.StaffPowerInfo()
                        {
                            StaffId = staff.Id,
                            RoleId = (await roleInfoService.GetAll().Where(p => p.Name == "一级权限").FirstAsync()).Id
                        });
                    }
                }

            }
        }

        /// <summary>
        /// 增加一个角色
        /// </summary>
        /// <param name="name">角色名</param>
        /// <returns></returns>
        public async Task AddRole(string name)
        {
            using (IRoleInfoService roleInfoService = new RoleInfoService())
            {
                await roleInfoService.CreateAsync(new Model.RoleInfo() { Name = name });
            }
        }

        /// <summary>
        /// 移除职员状态
        /// </summary>
        /// <param name="staffId"></param>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        public async Task ChangeStaffStatusById(Guid staffId, Guid operatorId)
        {
            using (var staffInfoService = new StaffInfoService())
            {
                var staff = await staffInfoService.GetOneById(staffId);
                staff.Status = !staff.Status;
                await staffInfoService.EditAsync(staff);

                using (var accountOperateLogService = new AccountOperateLogService())
                {
                    await accountOperateLogService.CreateAsync(new Model.AccountOperateLog()
                    {
                        OperatorId = operatorId,
                        ModifiedId = staffId,
                        OPerateType = "2"
                    });
                }
            }
        }

        public async Task ChangeStaffStatusByTel(string staffTel, Guid operatorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool CreateDbBackUp(string sql)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "127.0.0.1";
            builder.UserID = "sa";
            builder.Password = "ljw19991003=";
            builder.InitialCatalog = "LogisticsSystem";
            using (SqlConnection conn = new SqlConnection(builder.ToString()))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    cmd.Dispose();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }


        }

        /// <summary>
        /// 添加车辆
        /// </summary>
        /// <param name="truck">车辆信息</param>
        /// <returns></returns>
        public async Task CreateTruck(TruckInfoDto truck)
        {
            var mytruck = new Model.TruckInfo()
            {
                Price = truck.Price,
                TruckModel = truck.TruckModel,
                TruckType = truck.TruckType,
                TruckClass = truck.TruckClass,
                TruckSize = truck.TruckSize,
                Wheelbase = truck.Wheelbase,
                AxlesNum = truck.AxlesNum,
                TruckLength = truck.TruckLength,
                TruckWidth = truck.TruckWidth,
                TruckHeight = truck.TruckHeight,
                TruckWeight = truck.TruckWeight,
                Load = truck.Load,
                TotalWeight = truck.TotalWeight,
                CarrierNum = truck.CarrierNum,
                Exhaust = truck.Exhaust,
                LicencePlate = truck.LicencePlate,
                PlateProvince = truck.PlateProvince,
                ContainerType = truck.ContainerType,
                ContainerLength = truck.ContainerLength,
                ContainerWidth = truck.ContainerWidth,
                ContainerHeight = truck.ContainerHeight,
                WebsiteId = truck.AffiliationId
            };
            using (var truckInfoService = new TruckInfoService())
            {
                await truckInfoService.CreateAsync(mytruck);
            }

            using (var truckStateService = new TruckStateService())
            {
                await truckStateService.CreateAsync(new Model.TruckState()
                {
                    TruckId = mytruck.Id,
                    Location = truck.InitLocation
                });
            }
        }

        /// <summary>
        /// 添加网点
        /// </summary>
        /// <param name="info">网点信息</param>
        /// <param name="operatorId">操作员id</param>
        /// <returns></returns>
        public async Task CreateWebSite(DTO.WebSiteInfoDto info, Guid operatorId)
        {
            var website = new Model.WebsiteInfo()
            {
                SiteName = info.Name,
                Province = info.Province,
                Address = info.Address,
                Location = info.Location,
                ChargeMan = info.ChargeMan,
                ChargeManTel = info.ChargeManTel,
                Type = info.Type.Trim()
            };
            using (var websiteInfoService = new WebsiteInfoService())
            {
                await websiteInfoService.CreateAsync(website);
            }
            using (var websiteInfoLogService = new WebsiteOperateLogService())
            {
                await websiteInfoLogService.CreateAsync(new Model.WebSiteOperateLog()
                {
                    WebsiteId = website.Id,
                    OperatorId = operatorId
                });
            }
        }

        /// <summary>
        /// 得到所有的角色
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> GetAllRoles()
        {
            using (IRoleInfoService roleInfoService = new RoleInfoService())
            {
                return await roleInfoService.GetAllOrder(false).Select(p => p.Name).ToArrayAsync();
            }
        }

        /// <summary>
        /// 得到所有的部门
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> GetAllSections()
        {
            using (ISectionService sectionService = new SectionService())
            {
                return await sectionService.GetAllOrder().Select(p => p.Name).ToArrayAsync();
            }
        }

        //得到所有的卡车信息
        public List<TruckInfoDto> GetAllTruck()
        {
            using (var truckInfoService = new TruckInfoService())
            {
                var truckList = truckInfoService.GetAllOrder(false).Include(p => p.Website).Select(p => new TruckInfoDto()
                {
                    TruckId = p.Id,
                    Price = p.Price,
                    TruckModel = p.TruckModel,
                    TruckType = p.TruckType,
                    TruckClass = p.TruckClass,
                    TruckSize = p.TruckSize,
                    Wheelbase = p.Wheelbase,
                    AxlesNum = p.AxlesNum,
                    TruckLength = p.TruckLength,
                    TruckWidth = p.TruckWidth,
                    TruckHeight = p.TruckHeight,
                    TruckWeight = p.TruckWeight,
                    Load = p.Load,
                    TotalWeight = p.TotalWeight,
                    CarrierNum = p.CarrierNum,
                    Exhaust = p.Exhaust,
                    LicencePlate = p.LicencePlate,
                    PlateProvince = p.PlateProvince,
                    ContainerType = p.ContainerType,
                    ContainerLength = p.ContainerLength,
                    ContainerWidth = p.ContainerWidth,
                    ContainerHeight = p.ContainerHeight,
                    AffiliationName = p.Website.SiteName,
                    AffiliationId = p.Website.Id,
                    Time = p.CreatTime,
                    Img = p.ImgName
                }).ToList();

                using (var truckstaeService = new TruckStateService())
                {
                    var stateList = truckstaeService.GetAll().ToList();
                    foreach (var truck in truckList)
                        truck.Stutas = stateList.Where(p => p.TruckId == truck.TruckId).Select(p => p.Status).FirstOrDefault();
                }

                return truckList;
            }
        }

        /// <summary>
        /// 根据id得到网点信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<Guid, string> GetAllWebSite()
        {
            using (var webSiteInfoService = new WebsiteInfoService())
            {
                var res = new Dictionary<Guid, string>();
                var websites = webSiteInfoService.GetAll();
                foreach (var website in websites)
                    res.Add(website.Id, website.SiteName);
                return res;
            }
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="type">1:account  2:file  3:notice  4:wesite</param>
        /// <returns></returns>
        public async Task<List<LogInfoDto>> GetLOg(string type)
        {
            List<LogInfoDto> logList = new List<LogInfoDto>();
            if (type == "1")
            {
                using (var accountOperateLogService = new AccountOperateLogService())
                {
                    logList = accountOperateLogService.GetAll().Include(p => p.OpStaffInfo).Include(p => p.MoStaffInfo).Select(p => new LogInfoDto()
                    {
                        CreatTime = p.CreatTime,
                        ModifyType = p.OPerateType,
                        Operator = p.OpStaffInfo.Name,
                        ModifyObj = p.MoStaffInfo.Name,
                        SectionId = p.OpStaffInfo.SectionId
                    }).ToList();
                }
            }
            else
            {
                using (var websiteOperateLogService = new WebsiteOperateLogService())
                {
                    logList = websiteOperateLogService.GetAll().Include(p => p.StaffInfo).Include(p => p.WebsiteInfo).Select(p => new LogInfoDto()
                    {
                        CreatTime = p.CreatTime,
                        ModifyType = p.OPerateType,
                        Operator = p.StaffInfo.Name,
                        ModifyObj = p.WebsiteInfo.SiteName,
                        SectionId = p.StaffInfo.SectionId
                    }).ToList();
                }
            }

            using (var sectionService = new SectionService())
            {
                foreach (var log in logList)
                {
                    log.Section = (await sectionService.GetOneById(log.SectionId)).Name;
                }
            }

            return logList;
        }

        /// <summary>
        /// 根据名称得到部门信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Guid> GetSectionIdByName(string name)
        {
            using (ISectionService sectionService = new SectionService())
            {
                var staff = await sectionService.GetAll().Where(p => p.Name == name).FirstAsync();
                return staff.Id;
            }
        }

        /// <summary>
        /// 得到职员的详细信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<StaffSimpleInfoDto>> GetAllStaffDetial()
        {
            var staffDetailList = new List<StaffSimpleInfoDto>();
            using (StaffInfoService staffInfoService = new StaffInfoService())
            {
                var staffList = await staffInfoService.GetAll().ToListAsync();
                using (SectionService sectionService = new SectionService())
                {

                    foreach (var item in staffList)
                    {
                        var sn = (await sectionService.GetOneById(item.SectionId)).Name;
                        staffDetailList.Add(new StaffSimpleInfoDto()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Tel = item.Tel,
                            Email = item.Email,
                            SectionName = sn,
                            Position = item.Position,
                            Status = item.Status,
                            CreateTime = item.CreatTime,
                            Address = item.Address,
                            Photo = item.ImagePath,
                            IdCard = item.IdCard
                        });
                    }
                }
            }
            return staffDetailList;
        }

        /// <summary>
        /// 得到网点的经纬度
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetWebSiteLocationById(Guid id)
        {
            using (var websiteInfoService = new WebsiteInfoService())
            {
                return websiteInfoService.GetAll().Where(p => p.Id == id).Select(p => p.Location).FirstOrDefault();
            }
        }

        /// <summary>
        /// 查询所有的职员
        /// </summary>
        /// <returns></returns>
        public async Task<List<StaffSimpleInfoDto>> QueryAllStaff()
        {
            using (var staffInfoService = new StaffInfoService())
            {
                List<StaffSimpleInfoDto> staffDtoList = new List<StaffSimpleInfoDto>();
                var staffList = await staffInfoService.GetAllOrder().ToListAsync();

                using (var sectionService = new SectionService())
                {
                    foreach (var staff in staffList)
                    {
                        string sectionName = sectionService.GetAll().Where(p => p.Id == staff.SectionId).FirstOrDefault().Name;
                        staffDtoList.Add(new StaffSimpleInfoDto()
                        {
                            Id = staff.Id,
                            Name = staff.Name,
                            Tel = staff.Tel,
                            Position = staff.Position,
                            Status = staff.Status,
                            Email = staff.Email,
                            SectionName = sectionName
                        });
                    }
                }
                return staffDtoList;
            }
        }


        public async Task<StaffSimpleInfoDto> QueryStaffById(Guid staffId)
        {
            throw new NotImplementedException();
        }

        public async Task<StaffSimpleInfoDto> QueryStaffByTel(string staffTel)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateGroupStaffInfo(StaffBsicInfoDto[] staffInfos, Guid operatorId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateOneStaffInfo(StaffBsicInfoDto staffInfo, Guid operatorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="modelType">日志的类型</param>
        /// <param name="operatorId">操作员的ID</param>
        /// <param name="modifiedId">被操作对象的ID</param>
        /// <param name="opType">操作的类型</param>
        /// <returns></returns>
        public async Task WriteOPLog(char modelType, Guid operatorId, Guid modifiedId, char opType = '1')
        {
            if (modelType == '1')
            {
                using (IAccountOperateLogService accountOperateLogService = new AccountOperateLogService())
                {
                    await accountOperateLogService.CreateAsync(new Model.AccountOperateLog()
                    {
                        OperatorId = operatorId,
                        ModifiedId = modifiedId,
                        OPerateType = opType.ToString()
                    });
                }
            }
            else
            {
                using (IWebsiteOperateLogService websiteOperateLogService = new WebsiteOperateLogService())
                {
                    await websiteOperateLogService.CreateAsync(new Model.WebSiteOperateLog()
                    {
                        OperatorId = operatorId,
                        WebsiteId = modifiedId,
                        OPerateType = opType.ToString()
                    });
                }
            }
        }

    }
}
