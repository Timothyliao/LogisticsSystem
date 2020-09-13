using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.DTO
{
    public class StaffSimpleInfoDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }

        public string SectionName { get; set; }

        public string Position { get; set; }

        public bool Status { get; set; }

        public DateTime CreateTime { get; set; }

        public string Address { get; set; }

        public string Photo { get; set; }

        public string IdCard { get; set; }
    }
}
