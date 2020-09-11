using System;
using LogisticsSystem.Model;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsSystem.IDAL
{
    public interface IBaseService<T> : IDisposable where T : BaseEntity
    {
        /// <summary>
        /// 增加记录（异步）
        /// </summary>
        /// <param name="model">数据模型</param>
        /// <param name="saved">自动保存</param>
        /// <returns></returns>
        Task CreateAsync(T model, bool saved = true);

        /// <summary>
        /// 更新记录（异步）
        /// </summary>
        /// <param name="model">数据模型</param>
        /// <param name="saved">自动保存</param>
        /// <returns></returns>
        Task EditAsync(T model, bool saved = true);

        /// <summary>
        /// 删除model所指定的记录
        /// </summary>
        /// <param name="model">数据模型</param>
        /// <param name="saved">自动保存</param>
        /// <returns></returns>
        Task RemoveAsync(T model, bool saved = true);

        /// <summary>
        /// 删除指定Id的记录
        /// </summary>
        /// <param name="id">记录的Id</param>
        /// <param name="saved">自动保存</param>
        /// <returns></returns>
        Task RemoverAsync(Guid id, bool saved = true);

        /// <summary>
        /// 保存记录
        /// </summary>
        /// <returns></returns>
        Task Save();

        /// <summary>
        /// 通过Id得到一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetOneById(Guid id);

        /// <summary>
        /// 得到所有的记录
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// 返回结果分页
        /// </summary>
        /// <param name="pageSixe">分页大小</param>
        /// <param name="pageIndex">开始下标</param>
        /// <returns></returns>
        IQueryable<T> GetAllByPage(int pageSize = 15, int pageIndex = 0);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="asc">是否为升序</param>
        /// <returns></returns>
        IQueryable<T> GetAllOrder(bool asc = true);

        /// <summary>
        /// 分页排序
        /// </summary>
        /// <param name="pageSixe">分页大小</param>
        /// <param name="pageIndex">开始下标</param>
        /// <param name="asc">是否为升序</param>
        /// <returns></returns>
        IQueryable<T> GetAllByPageOrder(int pageSize = 15, int pageIndex = 0, bool asc = true);

    }
}
