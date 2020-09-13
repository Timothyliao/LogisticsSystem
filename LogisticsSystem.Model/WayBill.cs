using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class WayBill : BaseEntity
    {
        /// <summary>
        /// 规划路劲（是指经过哪些节点）
        /// </summary>
        [Required, StringLength(maximumLength: 200), Column(TypeName = "varchar")]
        public string PlanPath { get; set; }

        public bool IsFinish { get; set; } = false;

        public DateTime FinishTime { get; set; }
    }
}
