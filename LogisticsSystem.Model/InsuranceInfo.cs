using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class InsuranceInfo : BaseEntity
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        /// <summary>
        /// 保价人姓名
        /// </summary>
        [ForeignKey(nameof(Sender))]
        public Guid InsurerId { get; set; }

        public CargoSender Sender { get; set; }

        [Required, StringLength(maximumLength: 10)]
        [Column(TypeName = "varchar")]
        public string Value { get; set; }

        [Required, StringLength(maximumLength: 5)]
        [Column(TypeName = "varchar")]
        public string Risk { get; set; }

        /// <summary>
        /// 证明材料1(保留字段)
        /// </summary>
        [Column(TypeName = "varchar"), StringLength(maximumLength: 100)]
        public string ProveImage1 { get; set; }

        /// <summary>
        /// 证明材料2(保留字段)
        /// </summary>
        [Column(TypeName = "varchar"), StringLength(maximumLength: 100)]
        public string ProveImage2 { get; set; }

        /// <summary>
        /// 证明材料3(保留字段)
        /// </summary>
        [Column(TypeName = "varchar"), StringLength(maximumLength: 100)]
        public string ProveImage3 { get; set; }

        /// <summary>
        /// 是否出问题了
        /// </summary>
        public bool IsProblem { get; set; } = false;

        /// <summary>
        /// 问题描述
        /// </summary>
        [Column(TypeName = "ntext")]
        public string ProblemDiscription { get; set; }

        public decimal Fee { get; set; }
    }
}
