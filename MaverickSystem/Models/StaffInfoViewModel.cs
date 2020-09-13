using System.ComponentModel.DataAnnotations;

namespace MaverickSystem.Models
{
    public class StaffInfoViewModel
    {
        [Required, StringLength(maximumLength: 20)]
        [Display(Name = "* 姓名")]
        public string Name { get; set; }

        [Required, Phone(ErrorMessage = "请输入正确的格式")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "* 电话")]
        public string Tel { get; set; }

        [Display(Name = "* 邮箱")]
        [EmailAddress(ErrorMessage = "请输入正确的格式"), Required]
        public string Email { get; set; }

        [Required, Display(Name = "* 身份证号")]
        [RegularExpression(@"(^\d{18}$)|(^\d{15}$)", ErrorMessage = "请输入正确的格式")]
        public string IdCard { get; set; }

        [Display(Name = "* 住址(省/市/县)"),Required]
        public string Address { get; set; }

        [Required,Display(Name = "* 职位")]
        public string PositionName { get; set; }

        [Required, Display(Name = "* 所属部门")]
        public string SectionName { get; set; }

        [Required, Display(Name = "* 权限角色")]
        public string RoleName { get; set; }

        [Display(Name = "证件照")]
        public string ImagePath { get; set; }
    }
}