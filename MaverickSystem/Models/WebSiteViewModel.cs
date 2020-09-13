using System.ComponentModel.DataAnnotations;

namespace MaverickSystem.Models
{
    public class WebSiteViewModel
    {
        [Display(Name = "* 名称"), Required(ErrorMessage = "此项为必填项")]
        public string Name { get; set; }

        [Display(Name = "* 负责人"), Required(ErrorMessage = "此项为必填项")]
        public string ChargeMan { get; set; }

        [Display(Name = "* 负责人电话"), Required(ErrorMessage = "此项为必填项"), Phone(ErrorMessage = "请输入正确的电话格式")]
        public string ChargeManTel { get; set; }

        [Display(Name = "* 地址(例：湖南省衡阳市蒸湘区)"), Required(ErrorMessage = "此项为必填项")]
        public string Province { get; set; }

        [Display(Name = "* 详细地址"), Required(ErrorMessage = "此项为必填项")]
        public string Address { get; set; }

        [Display(Name = "类型")]
        public string Type { get; set; }
    }
}