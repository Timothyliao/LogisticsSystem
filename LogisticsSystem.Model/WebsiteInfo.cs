using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class WebsiteInfo : BaseEntity
    {
        [Required,StringLength(40), Column(TypeName = "varchar")]
        public string Province { get; set; }

        [Required,StringLength(100), Column(TypeName = "varchar")]
        public string Address { get; set; }

        [Required, StringLength(30)]
        public string SiteName { get; set; }

        /// <summary>
        /// 网点地址
        /// </summary>
        [Required, StringLength(40), Column(TypeName = "varchar")]
        public string Location { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        [Required, StringLength(20), Column(TypeName = "varchar")]
        public string ChargeMan { get; set; }

        /// <summary>
        /// 负责人电话
        /// </summary>
        [Required, StringLength(11), Column(TypeName = "varchar")]
        public string ChargeManTel { get; set; }

        /// <summary>
        /// 网点类型
        /// 1：普通网点
        /// 2：集散中心
        /// 3：转运中心
        /// </summary>
        [Column(TypeName ="char")]
        public string Type { get; set; }
    }
}
