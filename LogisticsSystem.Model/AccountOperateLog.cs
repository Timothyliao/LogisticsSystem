using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class AccountOperateLog : BaseEntity
    {
        // <summary>
        /// 操作员Id
        /// </summary>
        [ForeignKey(nameof(OpStaffInfo))]
        public Guid OperatorId { get; set; }

        public StaffInfo OpStaffInfo { get; set; }

        /// <summary>
        /// 操作的对象Id
        /// </summary>
        [ForeignKey(nameof(MoStaffInfo))]
        public Guid ModifiedId { get; set; }

        public StaffInfo MoStaffInfo { get; set; }

        /// <summary>
        /// 操作的类型
        /// 1：ADD操作
        /// 2：Update操作
        /// 3：Remove操作
        /// 4：Query操作
        /// </summary>
        public string OPerateType { get; set; } = "1";

    }
}
