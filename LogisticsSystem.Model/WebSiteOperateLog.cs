using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsSystem.Model
{
    public class WebSiteOperateLog : BaseEntity
    {
        /// <summary>
        /// 操作员Id
        /// </summary>
        [ForeignKey(nameof(StaffInfo))]
        public Guid OperatorId { get; set; }

        public StaffInfo StaffInfo { get; set; }

        /// <summary>
        /// 操作的类型
        /// 1：ADD操作
        /// 2：Update操作
        /// 3：Remove操作
        /// 4：Query操作
        /// </summary>
        public string OPerateType { get; set; } = "1";

        /// <summary>
        /// 操作的对象Id
        /// </summary>
        [ForeignKey(nameof(WebsiteInfo))]
        public Guid WebsiteId { get; set; }

        public WebsiteInfo WebsiteInfo { get; set; }
    }
}
