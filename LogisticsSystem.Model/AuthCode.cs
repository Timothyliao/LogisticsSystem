using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace LogisticsSystem.Model
{
    public class AuthCode : BaseEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [ForeignKey(nameof(StaffInfo))]
        public Guid StaffId { get; set; }

        public StaffInfo StaffInfo { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required, StringLength(maximumLength: 40), Column(TypeName = "varchar")]
        public string Code { get; set; }
    }
}
