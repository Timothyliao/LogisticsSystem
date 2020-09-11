using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class CargoSender : BaseEntity
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required, StringLength(maximumLength: 20), Column(TypeName = "varchar")]
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Required, StringLength(maximumLength: 11), Column(TypeName = "char")]
        public string MobliePhone { get; set; }

        /// <summary>
        /// 省市区
        /// </summary>
        [Required, StringLength(maximumLength: 40), Column(TypeName = "varchar")]
        public string Provinces { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Required, StringLength(maximumLength: 100), Column(TypeName = "varchar")]
        public string DetailAddress { get; set; }

        /// <summary>
        /// 经纬度信息(用;连接)
        /// </summary>
        [StringLength(maximumLength: 50)]
        [Column(TypeName = "varchar")]
        public string Location { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [Required, StringLength(maximumLength: 10), Column(TypeName = "varchar")]
        public string PostCode { get; set; }

        /// <summary>
        /// 固话
        /// </summary>
        [Required, StringLength(maximumLength: 11), Column(TypeName = "varchar")]
        public string TelPhone { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [Required, StringLength(maximumLength: 40), Column(TypeName = "varchar")]
        public string FirmName { get; set; }
    }
}
