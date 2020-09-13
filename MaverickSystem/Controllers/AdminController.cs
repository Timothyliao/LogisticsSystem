using System.Web.Mvc;
using LogisticsSystem.IBLL;
using LogisticsSystem.BLL;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Configuration;
using System.Data;
using System;
using System.Data.OleDb;
using System.Collections.Generic;
using LogisticsSystem.Tools;
using System.Linq;
using Newtonsoft.Json;

namespace MaverickSystem.Controllers
{

    [Filters.maverickSystemAuth]
    public class AdminController : Controller
    {
        private string StaffPower = string.Empty;

        public AdminController()
        {
            this.StaffPower = "1";
        }

        [HttpGet]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddRole(Models.RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                string[] names = model.Name.Trim().Split(';', '；');
                IAdminManager adminManager = new AdminManager();
                try
                {
                    if (names.Length > 1)
                        await adminManager.AddGroupRole(names);
                    else
                        await adminManager.AddRole(model.Name);
                }
                catch (Exception)
                {
                    return Content("<script>alert('操作失败！');history.go(-1);</script>");
                }
                return Content("<script>alert('操作成功！');history.go(-1);</script>");
            }
            else
                return Content("<script>alert('操作失败！');history.go(-1);</script>");
        }

        [HttpGet]
        public async Task<ActionResult> AddStaff()
        {
            IAdminManager adminManager = new AdminManager();
            var roleNames = await adminManager.GetAllRoles();
            ViewBag.RoleNames = roleNames;
            var sectionNames = await adminManager.GetAllSections();
            ViewBag.SectionNames = sectionNames;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddStaff(Models.StaffInfoViewModel model)
        {
            //var str = Request.Cookies["userId"].Value;
            var str = Session["userId"].ToString();
            var userid = Guid.Parse(str);

            try
            {
                string _path = Server.MapPath("~/Assets/images/users/");//设定上传的文件路径
                DirectoryInfo directoryInfo = new DirectoryInfo(_path);
                if (!directoryInfo.Exists)
                    directoryInfo.Create();

                HttpPostedFileBase file = Request.Files["ImagePath"];
                //判断是否已经选择上传文件
                if (file != null)
                {
                    string fileType = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    if (fileType != "png" && fileType != "jpg" && fileType != "jpeg")
                        return Content("<script language='javascript'>alert('仅支持jpg、png格式的图片，请重新选择！');history.go(-1);</script>");
                    else
                    {
                        //图片大小限制为2M
                        if (file.ContentLength > 2 * 1024 * 1024)
                            return Content("<script language='javascript'>alert('文件过大！请压缩后上传');history.go(-1);</script>");
                        else
                        {
                            string filenName = DateTime.Now.Ticks + file.FileName;
                            string filepath = _path + filenName;
                            file.SaveAs(filepath);//

                            IAdminManager adminManager = new AdminManager();
                            var staff = new LogisticsSystem.DTO.StaffBsicInfoDto()
                            {
                                Name = model.Name,
                                Tel = model.Tel,
                                Email = model.Email,
                                Address = model.Address,
                                IdCard = model.IdCard,
                                Position = model.PositionName,
                                SectionId = await adminManager.GetSectionIdByName(model.SectionName),
                                ImagePath = filenName
                            };
                            var key = ConfigurationManager.AppSettings["EncryptAndDecryptPwdString"].ToString();
                            await adminManager.AddOneStaff(staff, userid, key, model.RoleName);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return Content("<script language='javascript'>alert('服务器错误，500!');history.go(-1);</script>");
            }


            return Content("<script language='javascript'>alert('创建成功！');history.go(-1);</script>");
        }

        public ActionResult GetTemplate()
        {
            try
            {
                string filePath = Server.MapPath(ConfigurationManager.AppSettings["staffTemplatePath"].ToString());

                //以字符流的形式下载文件
                FileStream fs = new FileStream(filePath, FileMode.Open);
                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                Response.ContentType = "application/octet-stream";
                //通知浏览器下载文件而不是打开
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("staffMessage.xlsx", System.Text.Encoding.UTF8));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception)
            {
                Response.Write("<script language='javascript'>alert('操作失败');history.go(-1);</script>");
                return RedirectToAction(nameof(AddStaff));
            }
            return RedirectToAction(nameof(AddStaff));
        }

        [HttpPost]
        public async Task<ActionResult> UploadAllMess()
        {
            //var str = Request.Cookies["userId"].Value;
            var str = Session["userId"].ToString();
            var userid = Guid.Parse(str);

            string _path = Server.MapPath("~/App_Data/fileTemp/");//设定上传的文件路径
            DirectoryInfo directoryInfo = new DirectoryInfo(_path);
            if (!directoryInfo.Exists)
                directoryInfo.Create();

            //判断是否已经选择上传文件
            HttpPostedFileBase file = Request.Files["excel"];
            string filenName = file.FileName;
            string filepath = _path + filenName;
            file.SaveAs(filepath);//上传路径

            bool hasTitle = true;
            string fileType = filepath.Substring(filepath.LastIndexOf('.') + 1);
            DataTable table = new DataTable();
            try
            {
                using (DataSet ds = new DataSet())
                {
                    string strCon = string.Format("Provider=Microsoft.{0}.0;Extended Properties=\"Excel {1}.0;HDR={2};IMEX=1;\";data source={3};", (fileType == ".xls" ? "Jet.OLEDB.4" : "ACE.OLEDB.12"), (fileType == ".xls" ? 8 : 12), (hasTitle ? "Yes" : "NO"), filepath);
                    string strCom = " SELECT * FROM [Sheet1$]";
                    using (OleDbConnection myConn = new OleDbConnection(strCon))
                    using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn))
                    {
                        myConn.Open();
                        myCommand.Fill(ds);
                    }
                    if (ds == null || ds.Tables.Count <= 0)
                        return Content("<script language='javascript'>alert('导入失败!');history.go(-1);</script>");
                    table = ds.Tables[0].Copy();
                }

                IAdminManager adminManager = new AdminManager();
                List<LogisticsSystem.DTO.StaffBsicInfoDto> messList = new List<LogisticsSystem.DTO.StaffBsicInfoDto>();
                foreach (DataRow row in table.Rows)
                {
                    //无记录时退出
                    if (string.IsNullOrEmpty(row[1].ToString()) || string.IsNullOrEmpty(row[2].ToString()) ||
                        string.IsNullOrEmpty(row[3].ToString()) || string.IsNullOrEmpty(row[4].ToString()) ||
                        string.IsNullOrEmpty(row[5].ToString()) || string.IsNullOrEmpty(row[7].ToString()))
                        break;

                    var staff = new LogisticsSystem.DTO.StaffBsicInfoDto();
                    staff.Name = row[1].ToString();
                    staff.Tel = row[2].ToString();
                    staff.Email = row[3].ToString();
                    staff.Address = row[4].ToString();
                    staff.IdCard = row[5].ToString();
                    staff.ImagePath = "avatar-default.png";
                    staff.SectionId = await adminManager.GetSectionIdByName(row[6].ToString());
                    staff.Position = row[7].ToString();
                    messList.Add(staff);
                }

                var key = ConfigurationManager.AppSettings["EncryptAndDecryptPwdString"].ToString();
                await adminManager.AddGroupStaff(messList, userid, key, ConfigurationManager.AppSettings["originalRole"]);
            }
            catch (Exception)
            {
                return Content("<script language='javascript'>alert('服务器错误，500!');history.go(-1);</script>");
            }

            return Content("<script language='javascript'>alert('导入成功！');history.go(-1);</script>");

        }

        [HttpGet]
        public ActionResult CreateWebsite()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateWebsite(Models.WebSiteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var adminManager = new AdminManager();
                string locationUrl = "https://restapi.amap.com/v3/geocode/geo";
                string lkey = ConfigurationManager.AppSettings["GdAppKey"].ToString();
                string sk = ConfigurationManager.AppSettings["GdSecretKey"].ToString();
                string[] addressArray = model.Province.Split('省', '市');
                string province = string.Empty;
                string city = string.Empty;
                string district = string.Empty;
                string websiteLocation = string.Empty;
                try
                {
                    province = addressArray[0];
                    city = addressArray[1];
                    district = addressArray[2];
                    websiteLocation = WebRequestService.QueryGDLocationByName(locationUrl, lkey, sk, province + city + district + model.Address, city);
                }
                catch (Exception)
                {
                    return Content("<script>alert('地址格式错误');history.go(-1);</script>");
                }


                try
                {
                    var website = new LogisticsSystem.DTO.WebSiteInfoDto()
                    {
                        Name = model.Name,
                        ChargeMan = model.ChargeMan,
                        ChargeManTel = model.ChargeManTel,
                        Province = model.Province,
                        Address = model.Address,
                        Location = websiteLocation,
                        Type = model.Type
                    };
                    string adminId = Session["userId"].ToString();
                    await adminManager.CreateWebSite(website, Guid.Parse(adminId));
                    return Content("<script>alert('创建成功');history.go(-1);</script>");
                }
                catch (Exception error)
                {
                    return Content("<script>alert(" + error.Message + ");history.go(-1);</script>");
                }
            }
            else
                return View();
        }

        [HttpGet]
        public ActionResult CreateTruck()
        {
            var adminManager = new AdminManager();
            var websites = adminManager.GetAllWebSite();
            ViewBag.Keys = websites.Keys;
            ViewBag.Websites = websites;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateTruck(Models.TruckViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var adminManager = new AdminManager();
                    var websiteId = Guid.Parse(model.Affiliation);
                    var location = adminManager.GetWebSiteLocationById(websiteId);
                    double weight = Convert.ToDouble(model.TotalWeight.Trim());
                    string size = weight <= 1.8 ? "1" : weight <= 6 ? "2" : weight <= 14 ? "3" : "4";
                    var truck = new LogisticsSystem.DTO.TruckInfoDto()
                    {
                        Price = model.Price,
                        TruckModel = model.TruckModel,
                        TruckType = model.TruckType,
                        TruckClass = model.TruckClass,
                        Wheelbase = model.Wheelbase,
                        AxlesNum = model.AxlesNum,
                        TruckLength = model.TruckLength,
                        TruckWidth = model.TruckWidth,
                        TruckHeight = model.TruckHeight,
                        TruckWeight = model.TruckWeight,
                        Load = model.Load,
                        TotalWeight = model.TotalWeight,
                        CarrierNum = model.CarrierNum,
                        Exhaust = model.Exhaust,
                        LicencePlate = model.LicencePlate,
                        PlateProvince = model.PlateProvince,
                        ContainerType = model.ContainerType,
                        ContainerLength = model.ContainerLength,
                        ContainerWidth = model.ContainerWidth,
                        ContainerHeight = model.ContainerHeight,
                        TruckSize = size,
                        AffiliationId = websiteId,
                        InitLocation = location
                    };
                    await adminManager.CreateTruck(truck);
                }
                catch (Exception)
                {
                    return Content("<script>alert('数据走丢了！');history.go(-1);</script>");
                }
                return Content("<script>alert('创建成功！');window.location.href='/Admin/CreateTruck';</script>");
            }
            else
                return Content("<script>alert('请正确输入！');history.go(-1);</script>");
        }

        [HttpGet]
        public ActionResult TruckView()
        {
            var adminManager = new AdminManager();
            var truckList = adminManager.GetAllTruck();
            ViewBag.DataCount = truckList.Count();
            ViewBag.HasPower = "1";//操作权限
            return View(truckList);
        }

        [HttpGet]
        public ActionResult TruckDetails(Guid id)
        {
            var adminManager = new AdminManager();
            var truckList = adminManager.GetAllTruck().Where(p => p.TruckId == id).FirstOrDefault();
            var websites = adminManager.GetAllWebSite();
            ViewBag.Keys = websites.Keys;
            ViewBag.Websites = websites;
            return View(truckList);
        }

        [HttpGet]
        public async Task<ActionResult> ACCManagement()
        {
            var adminManager = new AdminManager();
            var staffList = await adminManager.QueryAllStaff();
            ViewBag.DataCount = staffList.Count;
            ViewBag.HasPower = "1";
            return View(staffList);
        }

        [HttpGet]
        public ActionResult DBBackuP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DBBackuP(string id = null)
        {
            string path = Request.Form["dbPath"].ToString();
            if (string.IsNullOrEmpty(path))
                path = Server.MapPath("~/DbBackup") + @"\LogisticsSystem.Bak";
            string sql = string.Format("BACKUP DATABASE LogisticsSystem TO DISK = '{0}'", path);
            AdminManager adminManager = new AdminManager();
            if (adminManager.CreateDbBackUp(sql))
                return Content("<script>alert('备份成功！');history.go(-1);</script>");
            else
                return Content("<script>alert('备份失败！');history.go(-1);</script>");
        }

        [HttpGet]
        public async Task<ActionResult> FileLOg()
        {
            /*----权限判断-----*/
            if (StaffPower == "1")
            {
                AdminManager adminManager = new AdminManager();
                List<LogisticsSystem.DTO.LogInfoDto> data = await adminManager.GetLOg("2");
                ViewBag.DataCount = data.Count;
                return View(data);
            }
            else
            {
                return Content("<script>alert('你没有权限查看！');history.Go(-1)</script>");
            }
        }

        [HttpGet]
        public async Task<ActionResult> AccLOg()
        {
            /*----权限判断-----*/
            if (StaffPower == "1")
            {
                AdminManager adminManager = new AdminManager();
                List<LogisticsSystem.DTO.LogInfoDto> data = await adminManager.GetLOg("1");
                ViewBag.DataCount = data.Count;
                return View(data);
            }
            else
            {
                return Content("<script>alert('你没有权限查看！');history.Go(-1)</script>");
            }
        }

        [HttpGet]
        public async Task<ActionResult> NoticeLOg()
        {
            /*----权限判断-----*/
            if (StaffPower == "1")
            {
                AdminManager adminManager = new AdminManager();
                List<LogisticsSystem.DTO.LogInfoDto> data = await adminManager.GetLOg("3");
                ViewBag.DataCount = data.Count;
                return View(data);
            }
            else
            {
                return Content("<script>alert('你没有权限查看！');history.Go(-1)</script>");
            }
        }

        [HttpGet]
        public async Task<ActionResult> WebsiteLOg()
        {
            /*----权限判断-----*/
            if (StaffPower == "1")
            {
                AdminManager adminManager = new AdminManager();
                List<LogisticsSystem.DTO.LogInfoDto> data = await adminManager.GetLOg("4");
                ViewBag.DataCount = data.Count;
                return View(data);
            }
            else
            {
                return Content("<script>alert('你没有权限查看！');history.Go(-1)</script>");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ChangeAccStatus(string acc)
        {
            Guid opId = Guid.Parse(Session["userId"].ToString());
            AdminManager adminManager = new AdminManager();
            try
            {
                await adminManager.ChangeStaffStatusById(Guid.Parse(acc), opId);
                return Json(new { status = "complete" });
            }
            catch (Exception)
            {
                return Json(new { status = "error" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> StaffDetil()
        {
            AdminManager adminManager = new AdminManager();
            //ViewBag.staffList = await adminManager.GetAllStaffDetial();
            ViewBag.SectionList = await adminManager.GetAllSections();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetStaffDetilBySn(string sn)
        {
            AdminManager adminManager = new AdminManager();
            var x = await adminManager.GetAllStaffDetial();
            var staffList = x.Where(p => p.SectionName == Server.UrlDecode(sn)).Select(p => p.Name).ToList();
            var res = JsonConvert.SerializeObject(staffList);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetStaffDetilByStn(string stn)
        {
            AdminManager adminManager = new AdminManager();
            var x = await adminManager.GetAllStaffDetial();
            var staffList = x.Where(p => p.Name == Server.UrlDecode(stn)).ToList();
            var res = JsonConvert.SerializeObject(staffList);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        
        public async Task<ActionResult> WibillAssign()
        {
            AdminManager adminManager = new AdminManager();
            BasicInfoManager basicInfoManager = new BasicInfoManager();
            ViewBag.truckList = adminManager.GetAllTruck().Where(p=>p.Stutas == "2");
            ViewBag.orderList = (await basicInfoManager.GetAllOrderViewNoPage()).Where(p=>p.Status == "2");
            return View();
        }
    }
}