using System.ComponentModel.DataAnnotations;

namespace MaverickSystem.Models
{
    public class TruckViewModel
    {
        /// <summary>
        /// 购买价格
        /// </summary>
        [Display(Name = "* 购买价格"), Required(ErrorMessage = "此项为必填项")]
        public decimal Price { get; set; }

        /// <summary>
        /// 车辆型号
        /// </summary>
        [Display(Name = "* 车辆型号"), Required(ErrorMessage = "此项为必填项")]
        public string TruckModel { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        [Display(Name = "* 车辆类型"), Required(ErrorMessage = "此项为必填项")]
        public string TruckType { get; set; }

        /// <summary>
        /// 货车类别
        /// 普通货车、纯电动货车、插电混动货车
        /// </summary>
        [Display(Name = "货车类别")]
        public string TruckClass { get; set; }

        /// <summary>
        /// 货车的大小
        /// 1：微型
        /// 2：轻型
        /// 3：中型
        /// 4：重型
        /// </summary>
        public string TruckSize { get; set; }

        /// <summary>
        /// 轴距
        /// </summary>
        [Display(Name = "* 货车轴距（mm）"), Required(ErrorMessage = "此项为必填项")]
        public string Wheelbase { get; set; }

        /// <summary>
        /// 轴数
        /// </summary>
        [Display(Name = "* 货车轴数"), Required(ErrorMessage = "此项为必填项")]
        public string AxlesNum { get; set; }

        /// <summary>
        /// 车长
        /// </summary>
        [Display(Name = "* 货车车长（m）"), Required(ErrorMessage = "此项为必填项")]
        public string TruckLength { get; set; }

        /// <summary>
        /// 车宽
        /// </summary>
        [Display(Name = "* 货车车宽（m）"), Required(ErrorMessage = "此项为必填项")]
        public string TruckWidth { get; set; }

        /// <summary>
        /// 车高
        /// </summary>
        [Display(Name = "* 货车车高（m）"), Required(ErrorMessage = "此项为必填项")]
        public string TruckHeight { get; set; }

        /// <summary>
        /// 整车重量
        /// </summary>
        [Display(Name = "* 整车重量（t）"), Required(ErrorMessage = "此项为必填项")]
        public string TruckWeight { get; set; }

        /// <summary>
        /// 额定载重（t）
        /// </summary>
        [Display(Name = "* 额定载重（t）"), Required(ErrorMessage = "此项为必填项")]
        public string Load { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        [Display(Name = "* 总重量（t）"), Required(ErrorMessage = "此项为必填项")]
        public string TotalWeight { get; set; }

        /// <summary>
        /// 核载人数
        /// </summary>
        [Display(Name = "* 核载人数"), Required(ErrorMessage = "此项为必填项")]
        public string CarrierNum { get; set; }

        /// <summary>
        /// 排放标准
        /// </summary>
        [Display(Name = "* 排放标准"), Required(ErrorMessage = "此项为必填项")]
        public string Exhaust { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        [Display(Name = "货车车牌")]
        public string LicencePlate { get; set; }

        /// <summary>
        /// 车牌省份简写
        /// </summary>
        [Display(Name = "车牌省份")]
        public string PlateProvince { get; set; }

        /// <summary>
        /// 货箱类型
        /// </summary>
        [Display(Name = "* 货箱类型")]
        public string ContainerType { get; set; }

        /// <summary>
        /// 货箱长度
        /// </summary>
        [Display(Name = "* 货箱长度（m）"), Required(ErrorMessage = "此项为必填项")]
        public string ContainerLength { get; set; }

        /// <summary>
        /// 货箱宽度
        /// </summary>
        [Display(Name = "* 货箱宽度（m）"), Required(ErrorMessage = "此项为必填项")]
        public string ContainerWidth { get; set; }

        /// <summary>
        /// 货箱高度
        /// </summary>
        [Display(Name = "货箱高度（m）")]
        public string ContainerHeight { get; set; }

        /// <summary>
        /// 归属网点
        /// </summary>
        public string Affiliation { get; set; }
    }
}