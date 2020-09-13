using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogisticsSystem.DTO;

namespace LogisticsSystem.IBLL
{
    public interface IAdminManager
    {
        /// <summary>
        /// 解锁冻结账号
        /// </summary>
        /// <param name="tel"></param>
        /// <returns></returns>
        Task ChangeStaffStatusByTel(string staffTel, Guid operatorId);

        Task ChangeStaffStatusById(Guid staffId, Guid operatorId);

        Task<string[]> GetAllRoles();

        Task<string[]> GetAllSections();

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="staffInfo"></param>
        /// <returns></returns>
        Task AddOneStaff(StaffBsicInfoDto staffInfo, Guid operatorId, string key, string originalRoleName);

        Task AddGroupStaff(List<StaffBsicInfoDto> staffInfos, Guid operatorId, string key, string originalRoleName);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="staffInfo"></param>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        Task UpdateOneStaffInfo(StaffBsicInfoDto staffInfo, Guid operatorId);

        Task UpdateGroupStaffInfo(StaffBsicInfoDto[] staffInfos, Guid operatorId);

        /// <summary>
        /// 查询员工简要信息
        /// </summary>
        /// <param name="staffTel"></param>
        /// <returns></returns>
        Task<StaffSimpleInfoDto> QueryStaffByTel(string staffTel);

        Task<StaffSimpleInfoDto> QueryStaffById(Guid staffId);

        Task<List<StaffSimpleInfoDto>> QueryAllStaff();

        Task AddRole(string name);

        Task AddGroupRole(string[] names);

        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="modelType">1表示是ACCOUNT；2表示是FILE；3表示是NOTICE；4表示是WEBSITE</param>
        /// <param name="operatorId"></param>
        /// <param name="modifiedId"></param>
        /// <param name="opType"></param>
        /// <returns></returns>
        Task WriteOPLog(char modelType, Guid operatorId, Guid modifiedId, char opType = '1');

        Task<Guid> GetSectionIdByName(string name);

        Task CreateWebSite(WebSiteInfoDto info, Guid operatorId);

        Task CreateTruck(TruckInfoDto truck);

        Dictionary<Guid, string> GetAllWebSite();

        string GetWebSiteLocationById(Guid id);

        List<TruckInfoDto> GetAllTruck();

        Task<List<LogInfoDto>> GetLOg(string type);

        bool CreateDbBackUp(string sql);

        Task<List<StaffSimpleInfoDto>> GetAllStaffDetial();
    }
}
