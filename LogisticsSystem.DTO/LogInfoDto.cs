using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.DTO
{
    public class LogInfoDto
    {
        public Guid SectionId { get; set; }

        public DateTime CreatTime { get; set; }

        public string ModifyType { get; set; }

        public string Operator { get; set; }

        public string Section { get; set; }

        public string ModifyObj { get; set; }
    }
}
