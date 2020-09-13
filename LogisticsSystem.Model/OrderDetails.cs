using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class OrderDetails : BaseEntity
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal Freight { get; set; }

        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceCharge { get; set; }

        /// <summary>
        /// 是否保价
        /// </summary>
        public bool IsInsured { get; set; } = false;

        /// <summary>
        /// 货物名称
        /// </summary>
        [Required, Column(TypeName = "varchar")]
        public string CargoName { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [Required,Column(TypeName = "varchar"),StringLength(maximumLength:15)]
        public string CargoWeight { get; set; }

        /// <summary>
        /// 件数
        /// </summary>
        [Required, Column(TypeName = "varchar"),StringLength(maximumLength:5)]
        public string UitNum { get; set; } = "1";

        /// <summary>
        /// 体积
        /// </summary>
        [Column(TypeName = "varchar"),StringLength(maximumLength:20)]
        public string CargoVolume { get; set; } = "0";

        /// <summary>
        /// 付款类型
        /// true:现付
        /// false:到付
        /// </summary>
        [Required]
        public bool PayType { get; set; } = true;

        [Column(TypeName = "varchar")]
        public string GetGoodsTime { get; set; }

        [Column(TypeName = "ntext")]
        public string Mark { get; set; }


        [ForeignKey(nameof(Sender)), Required]
        public Guid SenderId { get; set; }

        public CargoSender Sender { get; set; }

        [ForeignKey(nameof(Receiver)), Required]
        public Guid ReceiverId { get; set; }

        public CargoReceiver Receiver { get; set; }
    }
}
