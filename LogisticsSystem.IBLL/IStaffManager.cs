using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.IBLL
{
    public interface IStaffManager
    {
        bool Login(string account, string password, string key, out Guid userId);

        DTO.StaffSimpleInfoDto GetSTaffInfoByTel(string tel);

        string GetSectionNameById(Guid id);

        Task CreateAuthCode(Guid staffId, string code);

        Task<bool> VerifyCode(string tel, string code, string key);

        Task ChangePwd(string tel, string password, string key);

        Task LockAccount(string tel);

        Task UnlockAccount(string tel, Guid _operatorId);

        List<DTO.OrderListDto> GetOrderByWebsite(out Guid id, out string websiteLocation, string name = null, string type = null);

        List<DTO.TruckInfoDto> GetTruckByWebsite(Guid? id);

        List<DTO.WebSiteInfoDto> GetAllWebSite();

        Task UpdateOrderStatus(string[] orderIdList, Guid truckId, string type);

        Task<bool> ToWayBill(string code,Guid truckId);

        List<string> GetInitalInfo(string tel);
    }
}
