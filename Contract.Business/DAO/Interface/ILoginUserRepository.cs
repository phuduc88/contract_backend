using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Data.Entity;

namespace Contract.Business.DAO
{
    public interface ILoginUserRepository : IRepository<LoginUser>
    {
        /// <summary>
        /// Get an IEnumerable LoginUser 
        /// </summary>
        /// <returns><c>IEnumerable LoginUser</c> if LoginUser not Empty, <c>null</c> otherwise</returns>
        IEnumerable<LoginUser> GetList();

        IEnumerable<LoginUser> FilterUser(ConditionSearchUser condition);
        
        /// <summary>
        /// Get an LoginUser by UserId, Password.
        /// </summary>
        /// <param name="userId">The condition get LoginUser.</param>
        /// <param name="password">The condition get LoginUser.</param>
        /// <returns><c>LoginUser</c> if id userId,password exist on database, <c>null</c> otherwise</returns>
        LoginUser Login(string userId, string password);

        /// <summary>
        /// Get an LoginUser by UserSID.
        /// </summary>
        /// <param name="id">The condition get LoginUser.</param>
        /// <returns><c>LoginUser</c> if UserSID on database, <c>null</c> otherwise</returns>
        LoginUser GetById(int id);

        /// <summary>
        /// Get an LoginUser by companyId.
        /// </summary>
        /// <param name="companyId">The condition get LoginUser.</param>
        /// <returns><c>LoginUser</c> if companyId on database, <c>null</c> otherwise</returns>
        LoginUser GetUserDefault(int companyId);

        /// <summary>
        /// Get an LoginUser by Email.
        /// </summary>
        /// <param name="email">The condition get LoginUser.</param>
        /// <returns><c>LoginUser</c> if email on database, <c>null</c> otherwise</returns>
        LoginUser GetByEmail(string email);

        /// <summary>
        /// Get an IEnumerable LoginUser 
        /// </summary>
        /// <param name="idCompany">The condition get list LoginUser.</param>
        /// <returns><c>IEnumerable LoginUser</c> if idCompany exist on database, <c>null</c> otherwise</returns>
        IEnumerable<LoginUser> GetByIdCompany(int idCompany);

        /// <summary>
        /// Check an userId already exists in database
        /// </summary>
        /// <param name="userId">The condition check.</param>
        /// <returns><c>True</c> if userId exist on database, <c>false</c> otherwise</returns>
        bool ContainUserId(string userId);

        /// <summary>
        /// Check an Email already exists in database
        /// </summary>
        /// <param name="email">The condition check.</param>
        /// <returns><c>True</c> if email exist on database, <c>false</c> otherwise</returns>
        bool ContainEmail(string email);

        /// <summary>
        /// Get an IEnumerable LoginUser by Role.
        /// </summary>
        /// <param name="roleName">The condition get LoginUser.</param>
        /// <returns><c>LoginUser</c> if UserSID on database, <c>null</c> otherwise</returns>
        IEnumerable<LoginUser> GetUsersByRoleName(string roleName);

        LoginUser GetByUserId(string userId, int companyId);

        bool ContainAccount(int companyId);        

    }
}
