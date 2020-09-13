using System.Web.Mvc;
using LogisticsSystem.IBLL;
using LogisticsSystem.BLL;
using System;
using System.Configuration;
using System.Web;
using LogisticsSystem.Tools;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace MaverickSystem.Controllers
{
    //[Filters.maverickSystemAuth]
    public class StaffController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Inital()
        {
            try
            {
                StaffManager staffManager = new StaffManager();
                var info = staffManager.GetInitalInfo(Session["userAccount"].ToString());
                return Json(new { status = "ok", name = info[0], tel = info[1], img = info[2] }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { status = "false" });
            }
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Models.Front.LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Session[model.Account + "counter"] == null || string.IsNullOrEmpty(Session[model.Account + "counter"].ToString()))
                    Session[model.Account + "counter"] = 0;


                IStaffManager staffManager = new StaffManager();
                string key = ConfigurationManager.AppSettings["EncryptAndDecryptPwdString"].ToString();
                //判断账号是否处于锁死状态
                var staffinfo = staffManager.GetSTaffInfoByTel(model.Account);
                if (staffinfo == null)
                    return Content("<script>alert('账号不存在！');history.go(-1);</script>");
                else if (!staffinfo.Status)
                    return Content("<script>alert('账号已锁！');history.go(-1);</script>");
                else
                {
                    //首次登陆强制修改密码
                    if (model.Account == model.PassWord)
                    {
                        return Content("<script>alert('账号和密码不能相同，为了您的账号安全，请修改密码！');window.location.href='/Staff/Register';</script>");
                    }
                    else
                    {
                        if (staffManager.Login(model.Account, model.PassWord, key, out Guid _userId))
                        {
                            var info = staffManager.GetInitalInfo(model.Account);
                            //记住我--->存cookie
                            Response.Cookies.Add(new HttpCookie("userAccount")
                            {
                                Value = model.Account,
                                Expires = DateTime.Now.AddDays(1)
                            });
                            Response.Cookies.Add(new HttpCookie("userId")
                            {
                                Value = _userId.ToString(),
                                Expires = DateTime.Now.AddDays(1)
                            });
                            Response.Cookies.Add(new HttpCookie("name")
                            {
                                Value = info[0],
                                Expires = DateTime.Now.AddDays(1)
                            });
                            Response.Cookies.Add(new HttpCookie("img")
                            {
                                Value = info[2],
                                Expires = DateTime.Now.AddDays(1)
                            });
                            Response.Cookies.Add(new HttpCookie("Position")
                            {
                                Value = info[1],
                                Expires = DateTime.Now.AddDays(1)
                            });


                            Session["userAccount"] = model.Account;
                            Session["userId"] = _userId;
                            Session["name"] = info[0];
                            Session["img"] = info[2];
                            Session["Position"] = info[1];
                            /*------------此处需配合权限进行页面控制--------------*/
                            return RedirectToAction("Index", "Staff");
                            /*------------此处需配合权限进行页面控制--------------*/
                        }
                        else
                        {
                            int count = Convert.ToInt32(Session[model.Account + "counter"].ToString());
                            //密码错误，尝试次数+1
                            Session[model.Account + "counter"] = count + 1;
                            //达到三次，账号上锁
                            //count++;
                            if (count == 3)
                            {
                                //锁账号
                                await staffManager.LockAccount(model.Account);
                                return Content("<script>alert('密码错误次数超过3次，账号已锁。尽快联系管理员解锁！');history.go(-1);</script>");
                            }
                            else
                                return Content("<script>alert('密码错误！您还剩余" + (3 - count) + "次尝试机会！');history.go(-1);</script>");
                        }
                    }
                }
            }
            else
                return Content("<script>alert('数据校验失败！');history.go(-1);</script>");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login2()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Register(Models.Front.RegisterViewModel model)
        {
            IStaffManager staffManager = new StaffManager();
            string key = ConfigurationManager.AppSettings["EncryptAndDecryptPwdString"];
            if (await staffManager.VerifyCode(model.Account, model.Code, key))
            {
                await staffManager.ChangePwd(model.Account, model.NewPwd, key);

                return Content("<script>alert('修改成功');window.location.href='/Staff/Login';</script>");
            }
            else
                return Content("<script>alert('修改失败，好像出了一点问题！');history.go(-1);</script>");
        }


        [HttpGet]
        public ActionResult SendAuthCode(string tel)
        {
            IStaffManager staffManager = new StaffManager();
            var staff = staffManager.GetSTaffInfoByTel(tel);
            var code = StringEncryptAndDecrypt.GenerateAESKey(5);
            try
            {
                string key = ConfigurationManager.AppSettings["EncryptAndDecryptPwdString"];
                staffManager.CreateAuthCode(staff.Id, StringEncryptAndDecrypt.AESEncrypt(code, key));
                if (SendEmail.Send(code, tel, staff.Email))
                    return Json(new { status = "ok" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { status = "false" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { status = "false" }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetGoods(string name = null, string type = null)
        {
            /*------------此处日后添加权限控制--------------*/


            /*------------此处日后添加权限控制--------------*/
            var staffManager = new StaffManager();
            Guid id;
            string location = string.Empty;
            List<LogisticsSystem.DTO.OrderListDto> data = staffManager.GetOrderByWebsite(out id, out location, name, type);
            List<LogisticsSystem.DTO.TruckInfoDto> trucks = new List<LogisticsSystem.DTO.TruckInfoDto>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    item.Time = item.Time.Split('日')[0].Replace('月', '/');
                    item.ReceiverAddress =
                                item.ReceiverAddress.Split('省', '市')[0] == item.ReceiverAddress.Split('省', '市')[1] ?
                                item.ReceiverAddress.Split('省', '市')[0] : item.ReceiverAddress.Split('省', '市')[0] + item.ReceiverAddress.Split('省', '市')[1];
                }
                trucks = staffManager.GetTruckByWebsite(id);
                foreach (var truck in trucks)
                {
                    if (truck.ContainerHeight == "0")
                        truck.Volumn = (Convert.ToDouble(truck.ContainerLength) * Convert.ToDouble(truck.ContainerWidth) * Convert.ToDouble(truck.TruckHeight)).ToString("f2");
                    else
                        truck.Volumn = (Convert.ToDouble(truck.ContainerLength) * Convert.ToDouble(truck.ContainerWidth) * Convert.ToDouble(truck.ContainerHeight)).ToString("f2");
                }
                data = data.OrderByDescending(p => p.Time).ToList();
            }
            ViewBag.Data = data;
            ViewBag.Id = id;
            ViewBag.Location = location;
            ViewBag.Trucks = trucks;
            ViewBag.WebSites = staffManager.GetAllWebSite();
            return View();
        }

        /// <summary>
        /// 动态路径规划
        /// </summary>
        /// <param name="para">订单的序号字符串，/分隔</param>
        /// <param name="orderId">第一个订单的id</param>
        /// <param name="truckId"></param>
        /// <param name="name">网点的名称</param>
        /// <param name="type">处理的类型，1：接货；2：配送</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DynamicPathPlaning(string para, string orderId, string truckId, string name, string type)
        {
            string[] paraArray = para.Split('/');
            const int STNum = 3, WorkHour = 11, AvgTime = 2;
            var staffManager = new StaffManager();
            Guid id;//the website's id
            string location = string.Empty;//the website's location
            //get order's data
            List<LogisticsSystem.DTO.OrderListDto> datas = staffManager.GetOrderByWebsite(out id, out location, name, type).Where(p => long.Parse(p.OrderId) <= long.Parse(orderId)).ToList();//避免处理期间有新的订单生成
            //get truck's data
            var truck = staffManager.GetTruckByWebsite(id).Where(p => p.TruckId == Guid.Parse(truckId)).FirstOrDefault();
            if (truck.ContainerHeight == "0")
                truck.Volumn = (Convert.ToDouble(truck.ContainerLength) * Convert.ToDouble(truck.ContainerWidth) * Convert.ToDouble(truck.TruckHeight)).ToString("f2");
            else
                truck.Volumn = (Convert.ToDouble(truck.ContainerLength) * Convert.ToDouble(truck.ContainerWidth) * Convert.ToDouble(truck.ContainerHeight)).ToString("f2");

            string[] locations = new string[paraArray.Length];//the location matrix
            double[,] dist = new double[paraArray.Length, paraArray.Length];//the distance matrix
            double[] valueCoefficient = new double[paraArray.Length];//The Value Coefficient Matrix
            double[,] coefficients = new double[STNum, paraArray.Length];//The Restraint Coefficient Matrix
            locations[0] = location;
            valueCoefficient[0] = 0;
            coefficients[0, 0] = double.MaxValue;
            coefficients[1, 0] = double.MaxValue;
            coefficients[2, 0] = double.MaxValue;
            //initialize matrix
            for (int i = 1; i < paraArray.Length; i++)
            {
                var num = int.Parse(paraArray[i]) - 1;
                valueCoefficient[i] = Convert.ToDouble(datas[num].Income);
                locations[i] = datas[num].Location;
                coefficients[0, i] = Convert.ToDouble(datas[num].CargoWeight) * Convert.ToDouble(datas[num].UnitNUm);
                coefficients[1, i] = Convert.ToDouble(datas[num].CargoVolume) * Convert.ToDouble(datas[num].UnitNUm);
                coefficients[2, i] = 0.25;
            }
            coefficients[0, paraArray.Length - 1] = Convert.ToDouble(truck.Load) * 1000;
            coefficients[1, paraArray.Length - 1] = Convert.ToDouble(truck.Volumn);
            coefficients[2, paraArray.Length - 1] = WorkHour - AvgTime;
            //calculate distance
            string key = ConfigurationManager.AppSettings["GdAppKey"];
            string skey = ConfigurationManager.AppSettings["GdSecretKey"];
            for (int i = 0; i < paraArray.Length; i++)
            {
                var aimLocation = locations[i];
                locations.ToList().Remove(aimLocation);
                var orignLocationStr = string.Join("|", locations);
                List<string> distances = WebRequestService.CalcGroupDistance(orignLocationStr, aimLocation, key, skey, "1");
                for (int j = 0; j < distances.Count; j++)
                    dist[i, j] = double.Parse(distances[j]);
                dist[i, i] = 0;
            }

            //Analyze soulation
            DPP dPP = new DPP();
            double _maxValue;
            List<string> soulation = dPP.GetSulotion(dist, valueCoefficient, coefficients, out _maxValue);
            string _path = string.Join("/", soulation);
            return Json(new { path = _path.Trim('/'), maxValue = _maxValue }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更新订单状态信息
        /// </summary>
        /// <param name="oids">订单ID字符串，/分隔开（不带96）</param>
        /// <param name="tid">车辆ID</param>
        /// <param name="tp">处理的类型，1：接货；2：配送</param>
        /// <param name="n">网点的名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ModifyOrderStatus(string oids, string tid, string tp)
        {
            try
            {
                Guid truckID = Guid.Parse(tid);
                string[] orderIdList = oids.Trim('/').Split('/');
                var staffManager = new StaffManager();
                await staffManager.UpdateOrderStatus(orderIdList, truckID, tp);
                return Json(new { status = "complete" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult WayBill()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> WayBill(string code)
        {
            BasicInfoManager basicInfoManager = new BasicInfoManager();
            LogisticsSystem.DTO.BookingNoteInfoDto mess = await basicInfoManager.GetAllOrderMess(code);
            return View(mess);
        }

        [HttpGet]
        public ActionResult MaverickOQC()
        {
            return View();
        }

        /// <summary>
        /// 出货货物检查
        /// </summary>
        /// <param name="oidL"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MaverickOQC(string oidL)
        {
            string oid = Request.Form["oqcvalue"].Trim(';');
            return View();
        }

        /// <summary>
        /// 出货确认
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> OQCCheck(string oid)
        {
            BasicInfoManager basicInfoManager = new BasicInfoManager();
            LogisticsSystem.DTO.BookingNoteInfoDto mess = await basicInfoManager.GetAllOrderMess(oid);
            return Json(new { coplete = "ok", stutas = mess.Stutas }, JsonRequestBehavior.AllowGet);
        }
    }
}