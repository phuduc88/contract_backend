using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System;
using System.Linq;
using Contract.Common.Extensions;
namespace Contract.Business.BL
{
    public class Account
    {
        private readonly ILoginUserRepository loginRepository;
        private readonly IMyCompanyRepository companyRepository;
        private readonly IUserRoleRepository userRoleRepository;
        private readonly Permission permisson;
        public Account(IRepositoryFactory repoFactory)
        {
            this.loginRepository = repoFactory.GetRepository<ILoginUserRepository>();
            this.companyRepository = repoFactory.GetRepository<IMyCompanyRepository>();
            this.userRoleRepository = repoFactory.GetRepository<IUserRoleRepository>();
            this.permisson = new Permission(repoFactory);
        }

        public void CreateAccountClient(Client clientInfo, string userId, string password)
        {
            ContractAccount account = BuildClientAccount(clientInfo, userId, password);
            var loginUser = CreateLogin(account);
            var functionByRole = this.permisson.FunctionByLevel(account.Level_Role);
            this.permisson.CreatePermission(loginUser, functionByRole);
        }


        private ContractAccount BuildClientAccount(Client clientInfo, string userId, string password)
        {
            string loginId = userId;
            ContractAccount account = new ContractAccount();
            account.UserName = clientInfo.PersonContact;
            account.Email = clientInfo.Email;
            account.Password = password;
            if (account.Password.IsNullOrEmpty())
            {
                account.Password = CretePassword();
            }
            if (loginId.IsNullOrEmpty())
            {
                loginId = clientInfo.TaxCode.IsNullOrEmpty() ? account.Password : clientInfo.TaxCode;
            }
            account.ClientId = clientInfo.ID;
            account.UserId = GetUserId(loginId);
            account.Level_Role = RoleInfo.CLIENT;
            return account;
        }


        private bool UserIsExisted(string userId)
        {
            bool isExitUserId = this.loginRepository.ContainUserId(userId);
            if (isExitUserId)
            {
                throw new BusinessLogicException(ResultCode.UserAccountMgtConflictResourceUserId,
                                  string.Format("Create User failed because user with UserId = [{0}] is exist.", userId));
            }

            return isExitUserId;
        }

        private MyCompany GetCustomer(int customerId)
        {
            var customer = companyRepository.GetById(customerId);
            if (customer == null)
            {
                throw new BusinessLogicException(ResultCode.NotFoundResourceId,
                                   string.Format("Get MyCompany info failed because Country with CompanySID =[{0}] not found.", customerId));
            }

            return customer;
        }

        private string CretePassword()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
        }
        private string GetUserId(MyCompany company, int contractType)
        {
            string userId = company.TaxCode.ToAscii();
            ContractType contactype = contractType.ToString().ParseEnum<ContractType>();
            if (contactype == ContractType.Agencies)
            {
                userId = company.Delegate.GetAccount();
            }
            if (userId.IsNullOrEmpty())
            {
                throw new BusinessLogicException(ResultCode.ContractLoginUserIsEmpty, "Tax code or Delegate is null or empty");
            }

            return MergeAccountWithDatabase(userId);
        }

        private string GetUserId(string loginId)
        {
            string userId = loginId.ToAscii();
            if (userId.IsNullOrEmpty())
            {
                throw new BusinessLogicException(ResultCode.ContractLoginUserIsEmpty, "Client Id is null or empty");
            }
            return MergeAccountWithDatabase(userId);
        }

        private string MergeAccountWithDatabase(string userId)
        {
            string newUserId = userId;
            int numberReCheckAcount = 20;
            int nextId = 0;
            do
            {
                --numberReCheckAcount;
                try
                {
                    if (!UserIsExisted(newUserId))
                    {
                        return newUserId;
                    }
                }
                catch (Exception)
                {
                    nextId = nextId + 1;
                    newUserId = string.Format("{0}_{1}", userId, nextId);
                }

            } while (numberReCheckAcount > 0);

            return newUserId;
        }

        private string GetLevelRoleByContractType(int contractType)
        {
            ContractType contactype = contractType.ToString().ParseEnum<ContractType>();
            if (contactype == ContractType.Agencies)
            {
                return RoleInfo.SALE;
            }
            return RoleInfo.CUSTOMER;
        }
        private LoginUser CreateLogin(ContractAccount account)
        {
            UserRole role = GetUserRole(account.Level_Role);
            return CreateLoginUser(account, role);
        }
        private UserRole GetUserRole(string level)
        {
            var userRole = this.userRoleRepository.FillterUserRole(level).FirstOrDefault();
            if (userRole == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            return userRole;
        }
        private LoginUser CreateLoginUser(ContractAccount userInfo, UserRole userRole)
        {
            UserIsExisted(userInfo.UserId);
            var loginUserInfo = new LoginUser();
            loginUserInfo.CopyData(userInfo);
            loginUserInfo.UserRoleSID = userRole.UserRoleSID;
            loginUserInfo.IsActive = true;
            loginUserInfo.CreatedDate = DateTime.Now;
            this.loginRepository.Insert(loginUserInfo);
            return loginUserInfo;
        }

    }
}
