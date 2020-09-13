using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class StaffPowerInfo : BaseEntity
    {
        [ForeignKey(nameof(StaffInfo))]
        public Guid StaffId { get; set; }

        public StaffInfo StaffInfo { get; set; }

        [ForeignKey(nameof(RoleInfo))]
        public Guid RoleId { get; set; }

        public RoleInfo RoleInfo { get; set; }
    }
}
