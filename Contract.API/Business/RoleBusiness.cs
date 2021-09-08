using Contract.Business.BL;
using Contract.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace Contract.API.Business
{
    public class RoleBusiness : BaseBusiness
    {
        #region Fields, Properties

        private IUserRoleBO roleBO;

        #endregion Fields, Properties

        #region Contructor

        public RoleBusiness(IBOFactory boFactory)
        {
            this.roleBO = boFactory.GetBO<IUserRoleBO>();
        }

        #endregion Contructor

        #region Methods

        public IEnumerable<FunctionInfo> GetList()
        {
            List<FunctionInfo> roleOfEmployee = new List<FunctionInfo>();
            var roleByLevels = this.roleBO.GetRoleByLevels(this.CurrentUser.RoleUser.Level);
            if (roleByLevels == null)
            {
                return roleOfEmployee;
            }

            roleByLevels.UserRoleDetails.ToList().ForEach(p =>
            {
                roleOfEmployee.Add(new FunctionInfo(p.DetailFunction, p.Id, p.Action.ToCharArray()));
            });
            return roleOfEmployee;
        }

        public IEnumerable<FunctionInfo> GetFunctionByLevel(string level)
        {
            return FunctionByLevel(level);
        }

        private IList<FunctionInfo> FunctionByLevel(string level)
        {
            List<FunctionInfo> roleOfEmployee = new List<FunctionInfo>();
            var userRoles = this.roleBO.GetRoleByLevels(level);
            if (userRoles == null)
            {
                return roleOfEmployee;
            }

            userRoles.UserRoleDetails.ToList().ForEach(p =>
            {
                roleOfEmployee.Add(new FunctionInfo(p.DetailFunction, p.Id, p.Action.ToCharArray()));
            });

            return roleOfEmployee;
        }
        #endregion
    }
}