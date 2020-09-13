using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class StaffInfo : BaseEntity
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required, StringLength(maximumLength: 20), Column(TypeName = "varchar")]
        public string Name { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [Required,StringLength(maximumLength:11),Column(TypeName ="char")]
        public string Tel { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required, StringLength(maximumLength: 70), Column(TypeName = "varchar")]
        public string Password { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required, StringLength(maximumLength: 50), Column(TypeName = "varchar")]
        public string Email { get; set; }

        /// <summary>
        /// 照片地址
        /// 只存储  名字.类型
        /// </summary>
        [StringLength(maximumLength: 100), Column(TypeName = "varchar")]
        public string ImagePath { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [Required, StringLength(maximumLength: 100), Column(TypeName = "varchar")]
        public string IdCard { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [Required, StringLength(maximumLength: 20), Column(TypeName = "varchar")]
        public string Position { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        [Required, StringLength(maximumLength: 100), Column(TypeName = "varchar")]
        public string Address { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [Required]
        public Guid SectionId { get; set; }

        /// <summary>
        /// 账号状态
        /// 冻结为false
        /// </summary>
        public bool Status { get; set; } = true;
    }
}
