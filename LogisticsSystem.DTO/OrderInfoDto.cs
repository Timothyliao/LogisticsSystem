using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.DTO
{
    public class OrderInfoDto
    {
        /*---寄件方---*/
        public string SName { get; set; }

        public string SMobliePhone { get; set; }

        public string SProvince { get; set; }

        public string SAddress { get; set; }

        public string SPostCode { get; set; }

        public string STelPhone { get; set; }

        public string SFirmName { get; set; }

        public string SLocation { get; set; }
        /*---收件方---*/
        public string RName { get; set; }

        public string RMobliePhone { get; set; }

        public string RProvince { get; set; }

        public string RAddress { get; set; }

        public string RPostCode { get; set; }

        public string RTelPhone { get; set; }

        public string RFirmName { get; set; }

        public string RLocation { get; set; }

        /*---货物信息---*/
        public string BarCode { get; set; }

        public string Region { get; set; }

        public double Freight { get; set; }

        public bool IsInsured { get; set; }

        public string CargoName { get; set; }

        public string CargoWeight { get; set; }

        public string UitNum { get; set; }

        public string CargoVolume { get; set; }

        public bool PayType { get; set; }

        public string Mark { get; set; }

        public string TakeTime { get; set; }

        public string Risk { get; set; }

        public string CargoValue { get; set; }

        public double ServiceCharge { get; set; }

        public Guid StartWebsiteId { get; set; }
    }
}
