using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.DTO
{
    public class OrderListDto
    {
        public string OrderId { get; set; }

        public string CargoName { get; set; }

        public string CargoWeight { get; set; }

        public string CargoVolume { get; set; }

        public string UnitNUm { get; set; }

        public string InsuredValue { get; set; }

        public string SenderAddress { get; set; }

        public string ReceiverAddress { get; set; }

        public string Status { get; set; }

        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public string Time { get; set; }

        public string Location { get; set; }

        public decimal Income { get; set; }

    }
}
