using System;

namespace MaverickSystem.Models
{
    public class OrderManagementViewModel
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

    }
}