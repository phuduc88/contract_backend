using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;
using Contract.Common.Extensions;
using Contract.Business.Constants;

namespace Contract.Business.DAO
{
    public class LoginUserRepository : GenericRepository<LoginUser>, ILoginUserRepository
    {
        public LoginUserRepository(IDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Get an IEnumerable LoginUser 
        /// </summary>
        /// <returns><c>IEnumerable LoginUser</c> if LoginUser not Empty, <c>null</c> otherwise</returns>
        public IEnumerable<LoginUser> GetList()
        {
            return GetLoginUserActive();
        }

        /// <summary>
        /// Get an LoginUser by UserId, Password.
        /// </summary>
        /// <param name="userId">The condition get LoginUser.</param>
        /// <param name="password">The condition get LoginUser.</param>
        /// <returns><c>LoginUser</c> if id userId,password exist on database, <c>null</c> otherwise</returns>
        public LoginUser Login(string userId, string password)
        {
            var loginUser = dbSet.Where(p => !(p.Deleted ?? false)).FirstOrDefault(p => p.UserID == userId && p.Password == password && (p.IsActive ?? false)); //&& !(p.MyCompany.Deleted ?? false) && (p.MyCompany.Active ?? false));
            return loginUser;
        }

        /// <summary>
        /// Get an LoginUser by UserSID.
        /// </summary>
        /// <param name="id">The condition get LoginUser.</param>
        /// <returns><c>Operator</c> if UserSID on database, <c>null</c> otherwise</returns>
        public LoginUser GetById(int id)
        {
            return GetLoginUserActive().FirstOrDefault(p => p.UserSID == id);
        }

        /// <summary>
        /// Get an LoginUser by Email.
        /// </summary>
        /// <param name="email">The condition get LoginUser.</param>
        /// <returns><c>LoginUser</c> if email on database, <c>null</c> otherwise</returns>
        public LoginUser GetByEmail(string email)
        {
            return GetLoginUserActive().FirstOrDefault(p => p.Email.Equals(email));
        }

        public IEnumerable<LoginUser> FilterUser(ConditionSearchUser condition)
        {
            var loginUsers = this.dbSet.Where(p => condition.ChildLevels.Contains(p.UserRole.Levels) && !(p.Deleted ?? false));
            if (condition.CompanyId.HasValue)
            {
                loginUsers = loginUsers.Where(p => p.CompanySID == condition.CompanyId);
            }

            if (!string.IsNullOrWhiteSpace(condition.Keyword))
            {
                loginUsers = loginUsers.Where(p => (p.UserID.ToUpper().Contains(condition.Keyword.ToUpper())
                    || p.Email.ToUpper().Contains(condition.Keyword.ToUpper())
                    || p.UserName.ToUpper().Contains(condition.Keyword.ToUpper())));
            }

            return loginUsers;
        }

         

        public IEnumerable<LoginUser> GetByIdCompany(int idCompany)
        {
            return GetLoginUserActive().Where(p => p.CompanySID == idCompany);
        }

        /// <summary>
        /// Check an userId already exists in database
        /// </summary>
        /// <param name="userId">The condition check.</param>
        /// <returns><c>True</c> if userId exist on database, <c>false</c> otherwise</returns>
        public bool ContainUserId(string userId)
        {
            return Contains(p => !(p.Deleted ?? false) && p.UserID == userId);
        }

        /// <summary>
        /// Check an Email already exists in database
        /// </summary>
        /// <param name="email">The condition check.</param>
        /// <returns><c>True</c> if email exist on database, <c>false</c> otherwise</returns>
        public bool ContainEmail(string email)
        {
            bool result = true;
            if (!email.IsNullOrEmpty())
            {
                result = Contains(p => ((p.Email ?? string.Empty).ToUpper()).Equals(email.ToUpper()) && !(p.Deleted ?? false));
            }
            return result;
        }

        /// <summary>
        /// Get an IEnumerable LoginUser by Role.
        /// </summary>
        /// <param name="roleName">The condition get LoginUser.</param>
        /// <returns><c>LoginUser</c> if roleName on database, <c>null</c> otherwise</returns>
        public IEnumerable<LoginUser> GetUsersByRoleName(string roleName)
        {
            return this.dbSet.Where(p => p.UserRole.Levels.Equals(roleName));
        }

        private IQueryable<LoginUser> GetLoginUserActive()
        {
            return dbSet.Where(p => !(p.Deleted ?? false));
        }

        public LoginUser GetByUserId(string userId, int companyId)
        {
            return GetLoginUserActive().FirstOrDefault(p => p.UserID.Equals(userId) && p.CompanySID == companyId);
        }

        public bool ContainAccount(int companyId)
        {
            return Contains(p => p.CompanySID == companyId && !(p.Deleted ?? false));
        }

        public LoginUser GetUserDefault(int companyId)
        {
            return GetLoginUserActive().FirstOrDefault(p => p.CompanySID == companyId && (p.AccountDefault ?? false));
        }
    }
}
