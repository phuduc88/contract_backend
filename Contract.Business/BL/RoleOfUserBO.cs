using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using Contract.Common.Extensions;
using Contract.Data.DBAccessor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Business.BL
{
    public class RoleOfUserBO : IRoleOfUserBO
    {
        #region Fields, Properties

        private readonly IRoleOfUserRepository roleOfUserRepository;
        #endregion
        public RoleOfUserBO(IRepositoryFactory repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.roleOfUserRepository = repoFactory.GetRepository<IRoleOfUserRepository>();
        }

        #region Contructor
        public ResultCode CreatePermission(LoginUser loginUser, List<FunctionInfo> functions)
        {
            try
            {
                loginUser.UserRole.UserRoleDetails.ForEach(p =>
                {
                    FunctionInfo function = functions.FirstOrDefault(i => i.Id == p.Id);
                    if (function != null)
                    {
                        var roleOfUser = new RoleOfUser();
                        roleOfUser.UserSID = loginUser.UserSID;
                        roleOfUser.UserRoleDetailId = p.Id;
                        roleOfUser.FunctionName = p.FunctionName;
                        roleOfUser.Action = function.GetAction();
                        this.roleOfUserRepository.Insert(roleOfUser);
                    }

                });
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ResultCode.UnknownError, ex.Message);
            }

            return ResultCode.NoError;
        }

        public ResultCode UpdatePermission(LoginUser loginUser, List<FunctionInfo> functions)
        {
            try
            {
                functions.ForEach(p =>
                {
                    var roleOfUser = this.roleOfUserRepository.GetRoleOfUser(p.Id, loginUser.UserSID);
                    if (roleOfUser != null)
                    {
                        roleOfUser.Action = p.GetAction();
                        this.roleOfUserRepository.Update(roleOfUser);
                    }

                });
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ResultCode.UnknownError, ex.Message);
            }
            return ResultCode.NoError;
        }

        public List<FunctionInfo> GetPermissionOfUser(LoginUser loginUser)
        {
            List<FunctionInfo> roleOfEmployee = new List<FunctionInfo>();

            //Set active control of permisson
            loginUser.UserRole.UserRoleDetails.ToList().ForEach(p =>
            {
                roleOfEmployee.Add(new FunctionInfo(p.DetailFunction, p.Id, p.Action.ToCharArray()));
            });

            loginUser.RoleOfUsers.ToList().ForEach(p =>
            {
                var functionInfo = roleOfEmployee.FirstOrDefault(i => i.Id == p.Id);
                if (functionInfo != null)
                {
                    functionInfo.SetValue(p.Action.ToCharArray());
                }
            });
            return roleOfEmployee;
        }

        #endregion
    }
}