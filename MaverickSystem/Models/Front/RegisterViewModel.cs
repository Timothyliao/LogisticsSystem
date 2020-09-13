using System.ComponentModel.DataAnnotations;

namespace MaverickSystem.Models.Front
{
    public class RegisterViewModel
    {
        [Required, StringLength(11)]
        public string Account { get; set; }

        [Required, StringLength(6)]
        public string Code { get; set; }

        [Required, StringLength(25)]
        public string OldPwd { get; set; }

        [Required, Compare(nameof(OldPwd), ErrorMessage = "两次密码不一致"), StringLength(25)]
        public string NewPwd { get; set; }
    }
}