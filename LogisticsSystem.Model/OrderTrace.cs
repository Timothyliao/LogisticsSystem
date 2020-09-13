using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class OrderTrace : BaseEntity
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(WebsiteInfo))]
        public Guid WebsiteId { get; set; }

        public string Mark { get; set; }

        public Order Order { get; set; }

        public WebsiteInfo WebsiteInfo { get; set; }
    }
}
