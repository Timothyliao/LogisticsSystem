using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class TruckInfo : BaseEntity
    {
        /// <summary>
        /// 购买价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 车辆型号
        /// </summary>
        public string TruckModel { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public string TruckType { get; set; }

        /// <summary>
        /// 货车类别
        /// 普通货车、纯电动货车、插电混动货车
        /// </summary>
        public string TruckClass  { get; set; }

        /// <summary>
        /// 货车的大小，预留
        /// 1：微型
        /// 2：轻型
        /// 3：中型
        /// 4：重型
        /// </summary>
        public string TruckSize { get; set; }

        /// <summary>
        /// 轴距
        /// </summary>
        public string Wheelbase { get; set; }

        /// <summary>
        /// 轴数
        /// </summary>
        public string AxlesNum { get; set; }

        /// <summary>
        /// 车长
        /// </summary>
        public string TruckLength { get; set; }

        /// <summary>
        /// 车宽
        /// </summary>
        public string TruckWidth { get; set; }

        /// <summary>
        /// 车高
        /// </summary>
        public string TruckHeight { get; set; }

        /// <summary>
        /// 整车重量
        /// </summary>
        public string TruckWeight { get; set; }

        /// <summary>
        /// 额定载重（t）
        /// </summary>
        public string Load { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public string TotalWeight { get; set; }

        /// <summary>
        /// 核载人数
        /// </summary>
        public string CarrierNum { get; set; }

        /// <summary>
        /// 排放标准
        /// </summary>
        public string Exhaust { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        [StringLength(maximumLength: 20), Column(TypeName = "varchar")]
        public string LicencePlate { get; set; }

        /// <summary>
        /// 车牌省份简写
        /// </summary>
        public string PlateProvince { get; set; }

        /// <summary>
        /// 货箱类型
        /// </summary>
        public string ContainerType { get; set; }

        /// <summary>
        /// 货箱长度
        /// </summary>
        public string ContainerLength { get; set; }

        /// <summary>
        /// 货箱宽度
        /// </summary>
        public string ContainerWidth { get; set; }

        /// <summary>
        /// 货箱高度
        /// </summary>
        public string ContainerHeight { get; set; }

        [ForeignKey(nameof(Website))]
        public Guid WebsiteId { get; set; }

        public WebsiteInfo Website { get; set; }

        public string ImgName { get; set; }


    }
}
