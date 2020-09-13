using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.IBLL
{
    public interface IDataApiManager
    {
        Task UpdateTruckState(Guid tid, string[] para);
    }
}
