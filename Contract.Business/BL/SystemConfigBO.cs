using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Business.BL
{
    public class SystemConfigBO: ISystemConfigBO
    {
        #region Fields, Properties

        private readonly ISystemConfigRepository systemConfigRepository;
        #endregion

        #region Contructor

        public SystemConfigBO(IRepositoryFactory repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.systemConfigRepository = repoFactory.GetRepository<ISystemConfigRepository>();
        }

        #endregion
       
        #region Methods

        public SystemConfigInfo GetSystemConfig()
        {
            var systemConfig = this.systemConfigRepository.GetSystemConfig();
            return new SystemConfigInfo(systemConfig);
        }

        #endregion
       
    }
}