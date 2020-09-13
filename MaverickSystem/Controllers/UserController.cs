using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LogisticsSystem.BLL;
using LogisticsSystem.DAL;
using LogisticsSystem.Model;
using LogisticsSystem.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MaverickSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult MainPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TakeOrder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TakeOrder(Models.Front.OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userManager = new UserManager();
                    string barcode = BarcodeFactory.GetBarcode();
                    bool isInsurance = !(model.CargoValue == null || model.CargoValue == "");
                    string senderPostcode = string.Empty;//寄方邮编
                    string receiverPostcode = string.Empty;//收方邮编
                    string senderLocation = string.Empty;//寄方地址经纬度
                    string receiverLocation = string.Empty;//收方地址经纬度
                    string region = string.Empty;

                    //寄方邮编
                    string jsonPath = Server.MapPath("~/App_Data/PostCode.json");
                    string province = model.SLocation.Trim().Split(' ')[0];
                    string city = model.SLocation.Trim().Split(' ')[1] == "市辖区" ? province : model.SLocation.Trim().Split(' ')[1];
                    string district = model.SLocation.Trim().Split(' ')[2];
                    string address = model.SAddress;
                    string key = ConfigurationManager.AppSettings["PostCodeKey"].ToString();
                    string url = "http://v.juhe.cn/postcode/search";
                    region = Region.GetRegion(province);
                    if (model.SAddress.Contains("乡") || model.SAddress.Contains("镇"))
                    {
                        string paddress = address.Contains("乡") ? address.Substring(0, address.IndexOf("乡") + 1) : address.Substring(0, address.IndexOf("镇") + 1);
                        senderPostcode = WebRequestService.QueryPostCodeByName(url, key, province, city, district, paddress, jsonPath);
                    }
                    else
                        senderPostcode = WebRequestService.QueryPostCodeByName(url, key, province, city, district, jsonPath);

                    //收方邮编
                    string rprovince = model.RLocation.Trim().Split(' ')[0];
                    string rcity = model.RLocation.Trim().Split(' ')[1] == "市辖区" ? rprovince : model.RLocation.Trim().Split(' ')[1];
                    string rdistrict = model.RLocation.Trim().Split(' ')[2];
                    string raddress = model.RAddress;
                    if (model.RAddress.Contains("乡") || model.RAddress.Contains("镇"))
                    {
                        string paddress = raddress.Contains("乡") ? raddress.Substring(0, raddress.IndexOf("乡") + 1) : raddress.Substring(0, raddress.IndexOf("镇") + 1);
                        receiverPostcode = WebRequestService.QueryPostCodeByName(url, key, rprovince, rcity, rdistrict, paddress, jsonPath);
                    }
                    else
                        receiverPostcode = WebRequestService.QueryPostCodeByName(url, key, rprovince, rcity, rdistrict, jsonPath);

                    //寄方地址经纬度
                    string locationUrl = "https://restapi.amap.com/v3/geocode/geo";
                    string lkey = ConfigurationManager.AppSettings["GdAppKey"].ToString();
                    string sk = ConfigurationManager.AppSettings["GdSecretKey"].ToString();
                    senderLocation = WebRequestService.QueryGDLocationByName(locationUrl, lkey, sk, province + city + district + address, city);

                    //收方地址经纬度
                    receiverLocation = WebRequestService.QueryGDLocationByName(locationUrl, lkey, sk, rprovince + rcity + rdistrict + raddress, rcity);
                    double freight = MaverickCost.CalcFreight(Convert.ToDouble(model.CargoWeight) * Convert.ToDouble(model.CargoUnit),
                        Convert.ToDouble(model.CargoVolume) * Convert.ToDouble(model.CargoUnit));
                    double serviceCharge = 0;

                    //得到起始网点
                    var webLocations = userManager.GetAllWebSiteLocationByType("1", city);
                    string origns = string.Join("|", webLocations.Values);
                    List<string> res = WebRequestService.CalcGroupDistance(origns, senderLocation, lkey, sk);
                    string min = res[1];
                    int minIndex = 0, flag = 0;
                    Guid webSiteId = new Guid();
                    for (int i = 1; i < res.Count; i++)
                        if (int.Parse(res[i]) < int.Parse(min))
                        {
                            min = res[i];
                            minIndex = i;
                        }
                    foreach (var locationkey in webLocations.Keys)
                    {
                        if (minIndex == flag)
                        {
                            webSiteId = locationkey;
                            break;
                        }
                        ++flag;
                    }


                    var orderInfo = new LogisticsSystem.DTO.OrderInfoDto()
                    {
                        SName = model.SenderName,
                        SMobliePhone = model.SMobliePhone.Trim(),
                        SProvince = province + city + district,
                        SAddress = model.SAddress.Trim(),
                        SPostCode = senderPostcode,
                        STelPhone = string.IsNullOrEmpty(model.STelPhone) ? "NULL" : model.STelPhone.Trim(),
                        SFirmName = string.IsNullOrEmpty(model.SCompany) ? "NULL" : model.SCompany.Trim(),
                        SLocation = senderLocation,
                        RName = model.ReceiverName.Trim(),
                        RMobliePhone = model.RMobliePhone.Trim(),
                        RProvince = rprovince + rcity + rdistrict,
                        RAddress = raddress,
                        RPostCode = receiverPostcode,
                        RTelPhone = string.IsNullOrEmpty(model.RTelPhone) ? "NULL" : model.RTelPhone.Trim(),
                        RFirmName = string.IsNullOrEmpty(model.RCompany) ? "NULL" : model.RCompany.Trim(),
                        RLocation = receiverLocation,
                        BarCode = barcode,
                        Region = region,
                        Freight = freight,
                        IsInsured = isInsurance,
                        CargoName = model.CargoName.Trim(),
                        CargoWeight = model.CargoWeight.Trim(),
                        CargoVolume = model.CargoVolume.Trim(),
                        UitNum = model.CargoUnit.Trim(),
                        PayType = model.PayType.Trim() == "1",
                        Mark = string.IsNullOrEmpty(model.Mark) ? "NULL" : model.Mark.Trim().Replace(' ', ','),
                        TakeTime = model.TakeTime.Trim(),
                        Risk = string.IsNullOrEmpty(model.Risk) ? "NULL" : model.Risk.Trim(),
                        CargoValue = string.IsNullOrEmpty(model.CargoValue) ? "NULL" : model.CargoValue.Trim(),
                        ServiceCharge = serviceCharge,
                        StartWebsiteId = webSiteId
                    };

                    await userManager.CreateOrder(orderInfo);
                }
                catch (Exception error)
                {
                    return Content("<script>alert('" + error.Message + "');history.go(-1);</script>");
                }
            }
            else
            {
                return Content("<script>alert('数据丢失了！');history.go(-1);</script>");
            }
            return Content("<script>alert('下单成功，感谢对本公司的信任！');window.location.href='/User/MainPage';</script>");
        }

        [HttpGet]
        public ActionResult FillLocation()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> FillLocation(TruckTrace trace)
        {
            if (ModelState.IsValid)
            {
                using (TruckTraceService truckTraceService = new TruckTraceService())
                {
                    var turck = new TruckTrace()
                    {
                        truckId = trace.truckId,
                        Location = trace.Location,
                        Mileage = trace.Mileage,
                        Path = trace.Path,
                        Position = trace.Position,
                        Speed = trace.Speed,
                        Direction = trace.Direction,
                        Type = true,
                        Unit = trace.Unit
                    };
                    try
                    {
                        await truckTraceService.CreateAsync(turck);
                    }
                    catch (Exception err)
                    {

                        throw err;
                    }
                }
            }
            else
            {

            }

            return Json(new { mess = "ok" });
        }

        [HttpGet]
        public ActionResult CargoTrace()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> TraceInfo(string cot)
        {
            UserManager userManager = new UserManager();
            var info = await userManager.TrackOrder(cot);

            //WebRequestService webRequestService = new WebRequestService();
            //string key = ConfigurationManager.AppSettings["GdAppKey"];
            //var res = webRequestService.GetTruckPath(info.SLocation, info.TransLocation, info.RLocation, "9", "3", key);
            //var setps = ((JArray)JsonConvert.DeserializeObject(res)).ToList();

            //info.Steps = setps;
            return Json(JsonConvert.SerializeObject(info), JsonRequestBehavior.AllowGet);
        }
    }
}