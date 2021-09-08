using Contract.Data.DBAccessor;
using System.Collections.Generic;

namespace Contract.Business.DAO
{
    public interface ISignOfUserRepository : IRepository<SignOfUser>
    {
        /// <summary>
        /// Get an IEnumerable DocumentType 
        /// </summary>
        /// <returns><c>IEnumerable DocumentType</c> if DocumentType not Empty, <c>null</c> otherwise</returns>
        IEnumerable<SignOfUser> Filter();

        SignOfUser GetDetail(int id);

        IEnumerable<SignOfUser> FilterUseDefault(int id, int companyId);

        SignOfUser GetSignOfUserDefault(int userId);


    }
}
