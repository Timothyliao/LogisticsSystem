using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using LogisticsSystem.BLL;
using LogisticsSystem.DTO;

namespace MaverickSystem.Controllers
{
    public class DataController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("dataApi/data/order")]
        public async Task<List<OrderListDto>> GetOrder()
        {
            BasicInfoManager basicInfoManager = new BasicInfoManager();
            var info = (await basicInfoManager.GetAllOrderViewNoPage());
            return info;
        }

        /// <summary>
        /// 站点更新订单位置
        /// </summary>
        /// <param name="ocode">订单条码值,;分隔</param>
        /// <param name="wid">站点id</param>
        /// <returns></returns>
        [Route("dataApi/data/ws")]
        public string PostByWebSite(string ocode, string wid)
        {
            bool status = false;
            List<string> oidList = ocode.Trim().Split(';').ToList();
            try
            {

                status = true;
            }
            catch (Exception)
            {

            }
            return status ? "complete" : "false";
        }


        /// <summary>
        /// 车辆位置信息更新
        /// </summary>
        /// <param name="tid">id</param>
        /// <param name="speed">速度</param>
        /// <param name="dir">方向</param>
        /// <param name="mileage">里程</param>
        /// <param name="unit">部件</param>
        /// <param name="position">经度,纬度</param>
        /// <param name="path">出发地-目的地</param>
        /// <returns></returns>
        [Route("dataApi/data/truckt")]
        public async Task<string> PostByTruck(string tid, string location, string speed, string dir, string mileage, string unit, string position, string path)
        {
            bool status = false;
            try
            {
                DataApiManager dataApiManager = new DataApiManager();
                await dataApiManager.UpdateTruckState(
                        Guid.Parse(tid),
                        new string[] { location, speed, dir, mileage, unit, position, path }
                    );
                status = true;
            }
            catch (Exception err)
            {
                return err.InnerException.Message;
            }

            return status ? "complete" : "false";
        }

        [Route("dataApi/data/test")]
        public async Task<string> GetTest()
        {
            bool status = false;
            try
            {
                DataApiManager dataApiManager = new DataApiManager();
                await dataApiManager.UpdateTruckState(
                        Guid.Parse("59EEC53F-A5D6-49D9-8843-116F442E8E7C"),
                                new string[] { "123.22;56.2323", "123", "222", "lks", "2", "llssk", "12321" }
                            );
                status = true;
            }
            catch (Exception)
            {

            }

            return status ? "complete" : "false";
        }
    }
}