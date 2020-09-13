using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class TruckState : BaseEntity
    {
        [ForeignKey(nameof(TruckInfo))]
        public Guid TruckId { get; set; }

        public TruckInfo TruckInfo { get; set; }

        [Required, StringLength(maximumLength: 20), Column(TypeName = "varchar")]
        public string Location { get; set; }

        /// <summary>
        /// 货车状态
        /// 1：运输中
        /// 2：空闲
        /// </summary>
        public string Status { get; set; } = "2";
    }
}
