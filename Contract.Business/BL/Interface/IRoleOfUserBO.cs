using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface IRoleOfUserBO
    {

        /// <summary>
        /// Search Clients by condition
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        ResultCode CreatePermission(LoginUser loginUser, List<FunctionInfo> functions);

        ResultCode UpdatePermission(LoginUser loginUser, List<FunctionInfo> functions);

        List<FunctionInfo> GetPermissionOfUser(LoginUser loginUser);
    }
}
