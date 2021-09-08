using Contract.Business.Models;
using Contract.Common;

namespace Contract.Business.BL
{
    public interface ISessionManagerBO
    {
        UserSessionInfo Login(LoginInfo loginInfo);
        UserSessionInfo Logout(string token);
        UserSessionInfo GetUserSession(string token);
        UserSessionInfo ResetPassword(ResetPassword resetPasswordInfo);
        ResultCode UpdatePassword(int userId, ChangePassword updatePasswordInfo);

    }
}
