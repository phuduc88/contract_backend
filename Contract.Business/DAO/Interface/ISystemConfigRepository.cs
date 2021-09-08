using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.DAO
{
    public interface ISystemConfigRepository : IRepository<SystemConfig>
    {
        SystemConfig GetSystemConfig();
    }
}
