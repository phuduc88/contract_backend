using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface ISignOfUserBO
    {
        IEnumerable<SignOfUserInfo> Filter();

        SignOfUserInfo GetDetail(int id);

        ResultCode Create(SignOfUserInfo signOfUser);

        ResultCode Update(int id, SignOfUserInfo signOfUser);

        ResultCode UpdateUseDefault(int id, int companyId);

        ResultCode Delete(int id);

        SignOfUserInfo GetSignOfUseDefault(int userId);

    }
}
