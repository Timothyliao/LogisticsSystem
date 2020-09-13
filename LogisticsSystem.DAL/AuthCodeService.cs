using LogisticsSystem.Model;
using LogisticsSystem.IDAL;

namespace LogisticsSystem.DAL
{
    public class AuthCodeService : BaseService<AuthCode>, IAuthCodeService
    {
        public AuthCodeService() : base(new LogisticsContext())
        {
        }
    }
}
