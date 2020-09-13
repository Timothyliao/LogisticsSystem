using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class RoleInfo : BaseEntity
    {
        [Required, StringLength(maximumLength: 20), Column(TypeName = "varchar")]
        public string Name { get; set; }
    }
}
