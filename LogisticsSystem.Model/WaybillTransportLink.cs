using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class WaybillTransportLink : BaseEntity
    {
        [ForeignKey(nameof(WayBill))]
        public Guid WayBillId { get; set; }

        public WayBill WayBill { get; set; }

        [ForeignKey(nameof(TruckInfo))]
        public Guid TransportInfoId { get; set; }

        public TruckInfo TruckInfo { get; set; }
    }
}
