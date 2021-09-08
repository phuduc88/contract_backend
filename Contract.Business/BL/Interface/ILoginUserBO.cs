using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Business.BL
{
    public interface ILoginUserBO
    {
        IEnumerable<AccountDetail> FillterUser(ConditionSearchUser condition, int skip = int.MinValue, int take = int.MaxValue);

        int CountFillterUser(ConditionSearchUser condition);

        AccountInfo GetAccountInfo(int id, int? companyId, string level);

        ResultCode Create(AccountInfo userInfo);

        ResultCode CreateDefault(AccountInfo userInfo);

        ResultCode Update(int id, AccountInfo userInfo);

        ResultCode Delete(int id, int? companyId, string roleOfUserDelete);

        ResultCode UpdateCurrentUser(LoginUser loginUser);

        ResultCode ChangePassword(int id, PasswordInfo passowrdInfo);

        AccountInfo GetAccountDefaultCompany(int companyId);
    }
}
