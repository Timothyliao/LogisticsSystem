using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.Model
{
    public class TruckTrace : BaseEntity
    {
        [ForeignKey(nameof(Truck)), Required]
        public Guid truckId { get; set; }

        public TruckInfo Truck { get; set; }

        /// <summary>
        /// 当前速度
        /// </summary>
        public string Speed { get; set; }

        /// <summary>
        /// 经纬度
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 车辆方向
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// 里程
        /// </summary>
        public string Mileage { get; set; }

        /// <summary>
        /// 部件类型
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 出发地---目的地
        /// </summary>
        public string Path { get; set; }


        public bool Type { get; set; } = true;
    }
}
