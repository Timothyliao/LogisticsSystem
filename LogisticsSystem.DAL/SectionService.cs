using LogisticsSystem.IDAL;
using LogisticsSystem.Model;

namespace LogisticsSystem.DAL
{
    public class SectionService : BaseService<Section>, ISectionService
    {
        public SectionService() : base(new LogisticsContext())
        {
        }
    }
}
