using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface IUserRoleBO
    {

        /// <summary>
        /// Search Clients by condition
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IEnumerable<UserRole> GetList();

        UserRole GetRoleById(int id);

        UserRole GetRoleByLevels(string levels);
    }
}
