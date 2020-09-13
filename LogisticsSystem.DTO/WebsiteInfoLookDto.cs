using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.DTO
{
    public class WebsiteInfoLookDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ChargeMan { get; set; }

        public string ChargeManTel { get; set; }

        public string Province { get; set; }

        public string Address { get; set; }

        public string Type { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
