using LogisticsSystem.Model;
using LogisticsSystem.IDAL;


namespace LogisticsSystem.DAL
{
    public class StaffPowerInfoService : BaseService<StaffPowerInfo>, IStaffPowerInfoService
    {
        public StaffPowerInfoService() : base(new LogisticsContext())
        {
        }
    }
}
