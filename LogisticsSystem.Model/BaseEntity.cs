using System;

namespace LogisticsSystem.Model
{
    public class BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 是否被删除（伪删除）
        /// </summary>
        public bool IsRemove { get; set; }
    }
}
