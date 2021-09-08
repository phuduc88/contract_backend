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
    public class Permission
    {
        private readonly IRoleOfUserRepository roleOfUserRepository;
        private readonly IUserRoleRepository roleRepository;
        private readonly IUserRoleDetailRepository roleDetailRepository;
        public Permission(IRepositoryFactory repoFactory)
        {
            this.roleOfUserRepository = repoFactory.GetRepository<IRoleOfUserRepository>();
            this.roleRepository = repoFactory.GetRepository<IUserRoleRepository>();
            this.roleDetailRepository = repoFactory.GetRepository<IUserRoleDetailRepository>();
        }

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
                    else
                    {
                        UserRoleDetail roleDetail = GetRoleDetailById(p.Id);
                        if (roleDetail != null)
                        {
                            roleOfUser = new RoleOfUser();
                            roleOfUser.UserSID = loginUser.UserSID;
                            roleOfUser.UserRoleDetailId = p.Id;
                            roleOfUser.FunctionName = roleDetail.FunctionName;
                            roleOfUser.Action = p.GetAction();
                            this.roleOfUserRepository.Insert(roleOfUser);
                        }

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
                var functionInfo = roleOfEmployee.FirstOrDefault(i => i.Id == p.UserRoleDetailId);
                if (functionInfo != null)
                {
                    functionInfo.SetValue(p.Action.ToCharArray());
                }
            });
            return roleOfEmployee;
        }

        public List<FunctionInfo> FunctionByLevel(string level)
        {
            List<FunctionInfo> roleOfEmployee = new List<FunctionInfo>();
            var userRoles = this.roleRepository.FillterUserRoleByLevel(level);
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


        public UserRoleDetail GetRoleDetailById(int id)
        {
            UserRoleDetail roleDetail = this.roleDetailRepository.GetById(id);
            return roleDetail;
        }
    }

}

