using LogisticsSystem.IBLL;
using LogisticsSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsSystem.Model;

namespace LogisticsSystem.BLL
{
    public class DataApiManager : IDataApiManager
    {
        /// <summary>
        /// 更新车辆位置信息
        /// </summary>
        /// <param name="tid">车辆id</param>
        /// <param name="para">参数：location, speed, dir, mileage, unit, position, path</param>
        public async Task UpdateTruckState(Guid tid, string[] para)
        {
            using (TruckTraceService truckTraceService = new TruckTraceService())
            {
                var truckTrace = new TruckTrace()
                {
                    truckId = tid,
                    Location = para[0],
                    Speed = para[1],
                    Direction = para[2],
                    Mileage = para[3],
                    Unit = para[4],
                    Position = para[5],
                    Path = para[6],
                };
                try
                {
                    await truckTraceService.CreateAsync(truckTrace);
                }
                catch (Exception err)
                {

                    throw err;
                }
            }
        }
    }
}
