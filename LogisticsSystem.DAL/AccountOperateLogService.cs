using LogisticsSystem.IDAL;
using LogisticsSystem.Model;

namespace LogisticsSystem.DAL
{
    public class AccountOperateLogService : BaseService<AccountOperateLog>, IAccountOperateLogService
    {
        public AccountOperateLogService() : base(new LogisticsContext())
        {
        }
    }
}
