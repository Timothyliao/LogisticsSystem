using System.ComponentModel.DataAnnotations;

namespace MaverickSystem.Models.Front
{
    public class OrderViewModel
    {
        /*---寄方信息---*/
        [Required(ErrorMessage = "此项为必填项")]
        public string SenderName { get; set; }

        public string SCompany { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [Required(ErrorMessage = "此项为必填项")]
        public string SMobliePhone { get; set; }

        /// <summary>
        /// 固话
        /// </summary>
        public string STelPhone { get; set; }

        /// <summary>
        /// 省市区
        /// </summary>
        [Required(ErrorMessage = "此项为必填项")]
        public string SLocation { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Required(ErrorMessage = "此项为必填项")]
        public string SAddress { get; set; }


        /*---收方信息---*/
        [Required(ErrorMessage = "此项为必填项")]
        public string ReceiverName { get; set; }

        public string RCompany { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [Required(ErrorMessage = "此项为必填项")]
        public string RMobliePhone { get; set; }

        /// <summary>
        /// 固话
        /// </summary>
        public string RTelPhone { get; set; }

        /// <summary>
        /// 省市区
        /// </summary>
        [Required(ErrorMessage = "此项为必填项")]
        public string RLocation { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Required(ErrorMessage = "此项为必填项")]
        public string RAddress { get; set; }


        /*---货物信息---*/
        [Required(ErrorMessage = "此项为必填项")]
        public string CargoName { get; set; }

        [Required]
        public string CargoWeight { get; set; }

        /// <summary>
        /// 货物件数
        /// </summary>
        [Required(ErrorMessage = "此项为必填项")]
        public string CargoUnit { get; set; }

        public string CargoVolume { get; set; }


        /*---服务信息---*/
        /// <summary>
        /// 货物申明价值
        /// </summary>
        public string CargoValue { get; set; }

        /// <summary>
        /// 风险规避系数
        /// </summary>
        public string Risk { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        [Required(ErrorMessage = "此项为必填项")]
        public string PayType { get; set; }

        /// <summary>
        /// 取货时间
        /// </summary>
        [Required(ErrorMessage = "此项为必填项")]
        public string TakeTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Mark { get; set; }
    }
}