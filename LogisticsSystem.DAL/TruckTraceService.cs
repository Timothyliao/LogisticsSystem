using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsSystem.IDAL;
using LogisticsSystem.Model;

namespace LogisticsSystem.DAL
{
    public class TruckTraceService : BaseService<TruckTrace>, ITruckTraceService
    {
        public TruckTraceService():base(new LogisticsContext())
        {

        }
    }
}
