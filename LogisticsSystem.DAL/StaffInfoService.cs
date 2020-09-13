using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class StaffInfoService : BaseService<StaffInfo>, IStaffInfoService
    {
        public StaffInfoService() : base(new LogisticsContext())
        {
        }
    }
}
