using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.DTO
{
    public class BookingNoteInfoDto
    {
        [Display(Name ="订单编号")]
        public string OrderId { get; set; }
        [Display(Name = "创建时间")]
        public string CreateTime { get; set; }
        [Display(Name = "状态")]
        public string Stutas { get; set; }
        [Display(Name = "申明价值")]
        public string Value { get; set; }
        [Display(Name = "保价费")]
        public string InsureFee { get; set; }
        [Display(Name = "运输费")]
        public string Freight { get; set; }
        [Display(Name = "服务费")]
        public string ServiceFee { get; set; }
        [Display(Name = "付款类型")]
        public string PayType { get; set; }
        [Display(Name = "取货时间")]
        public string GetGoodsTime { get; set; }
        [Display(Name = "备注")]
        public string Mark { get; set; }
        [Display(Name = "货物名称")]
        public string CargoName { get; set; }
        [Display(Name = "货物数量")]
        public string CargoUnit { get; set; }
        [Display(Name = "货物重量")]
        public string CargoWeight { get; set; }
        [Display(Name = "货物体积")]
        public string CargoVolumn { get; set; }
        [Display(Name = "寄件人")]
        public string SenderName { get; set; }
        [Display(Name = "寄件人电话")]
        public string SMobilePhone { get; set; }
        [Display(Name = "寄件人地址")]
        public string SAddress { get; set; }
        [Display(Name = "寄件人公司")]
        public string SFirm { get; set; }
        [Display(Name = "寄件人固话")]
        public string STel { get; set; }
        [Display(Name = "收件人")]
        public string ReceiverName { get; set; }
        [Display(Name = "收件人电话")]
        public string RMobilePhone { get; set; }
        [Display(Name = "收件人地址")]
        public string RAddress { get; set; }
        [Display(Name = "收件人公司")]
        public string RFirm { get; set; }
        [Display(Name = "收件人固话")]
        public string RTel { get; set; }
        [Display(Name = "收件网点")]
        public string WebSiteName { get; set; }
    }
}
