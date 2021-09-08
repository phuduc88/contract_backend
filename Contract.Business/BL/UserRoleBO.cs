using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Business.BL
{
    public class UserRoleBO : IUserRoleBO
    {
        #region Fields, Properties

        private readonly IUserRoleRepository roleRepository;
        #endregion

        #region Contructor

        public UserRoleBO(IRepositoryFactory repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.roleRepository = repoFactory.GetRepository<IUserRoleRepository>();
        }

        #endregion

        #region Methods
        public IEnumerable<UserRole> GetList()
        {
            return this.roleRepository.GetAll().Where(p => p.Levels != RoleInfo.SYSTEMADMIN);
        }

        public UserRole GetRoleById(int id)
        {
            return this.roleRepository.GetById(id);
        }

        public UserRole GetRoleByLevels(string levels)
        {
            return this.roleRepository.FillterUserRoleByLevel(levels);
        }

        #endregion
    }
}