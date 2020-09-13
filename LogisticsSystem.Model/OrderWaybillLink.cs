using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class OrderWaybillLink : BaseEntity
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        [ForeignKey(nameof(WayBill))]
        public Guid WaybillId { get; set; }

        public WayBill WayBill { get; set; }
    }
}
