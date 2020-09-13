using System.ComponentModel.DataAnnotations;

namespace MaverickSystem.Models.Front
{
    public class LoginViewModel
    {
        [Display(Name = "账号")]
        public string Account { get; set; }

        [Display(Name = "密码")]
        public string PassWord { get; set; }

        public bool IsSave { get; set; }
    }
}