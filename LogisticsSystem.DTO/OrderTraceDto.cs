using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace LogisticsSystem.DTO
{
    public class OrderTraceDto
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// 寄方一级地址
        /// </summary>
        public string SFAddress { get; set; }

        /// <summary>
        /// 寄方二级地址
        /// </summary>
        public string SSAddress { get; set; }

        /// <summary>
        /// 寄方姓名
        /// </summary>
        public string SName { get; set; }

        /// <summary>
        /// 寄方电话
        /// </summary>
        public string STel { get; set; }

        /// <summary>
        /// 收方一级地址
        /// </summary>
        public string RFAddress { get; set; }

        /// <summary>
        /// 收方二级地址
        /// </summary>
        public string RSAddress { get; set; }

        /// <summary>
        /// 收方姓名
        /// </summary>
        public string RName { get; set; }

        /// <summary>
        /// 收方电话
        /// </summary>
        public string RTel { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OStatus { get; set; }

        /// <summary>
        /// 订单追踪地址
        /// </summary>
        public List<PackegeStatus> StatusList { get; set; }

        /// <summary>
        /// 货车经纬度
        /// </summary>
        public string TransLocation { get; set; }

        /// <summary>
        /// 寄方经纬度
        /// </summary>
        public string SLocation { get; set; }

        /// <summary>
        /// 收方经纬度
        /// </summary>
        public string RLocation { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public List<JToken> Steps { get; set; }
    }
}
