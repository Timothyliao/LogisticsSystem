using System.ComponentModel.DataAnnotations;

namespace MaverickSystem.Models
{
    public class RoleViewModel
    {
        [Required, Display(Name = "角色名称")]
        public string Name { get; set; }
    }
}