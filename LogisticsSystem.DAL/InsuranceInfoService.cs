using LogisticsSystem.Model;

namespace LogisticsSystem.DAL
{
    public class InsuranceInfoService : DAL.BaseService<InsuranceInfo>, IDAL.IInsuranceInfoService
    {
        public InsuranceInfoService() : base(new LogisticsContext())
        {
        }
    }
}
