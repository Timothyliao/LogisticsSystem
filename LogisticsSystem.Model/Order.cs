using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class Order : BaseEntity
    {
        /// <summary>
        /// 条码值
        /// </summary>
        [Required, StringLength(maximumLength: 20), Column(TypeName = "varchar")]
        public string BarCode { get; set; }

        /// <summary>
        /// 处理订单网点
        /// </summary>
        public Guid StartWebsiteId { get; set; }

        /// <summary>
        /// 配送订单网点
        /// </summary>
        public Guid EndWebsiteId { get; set; }

        /// <summary>
        /// 订单的状态
        /// 1：未处理
        /// 2：接货中
        /// 3：运输中
        /// 4：配送中
        /// 5：已签收
        /// 6：丢失
        /// </summary>
        [Column(TypeName = "char"), StringLength(maximumLength: 1)]
        public string Status { get; set; } = "1";

        [Required, StringLength(maximumLength: 10), Column(TypeName = "varchar")]
        public string Region { get; set; }

        public DateTime FinishTime { get; set; } = DateTime.Now;

    }
}
