﻿using Contract.Business.Models;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Data.Entity;

namespace Contract.Business.DAO
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        /// <summary>
        /// Get list UserRole by level.
        /// </summary>
        /// <param name="level">The condition get UserRole.</param>
        /// <returns><c>list UserRole </c> if Levels on database, <c>null</c> otherwise</returns>
        IEnumerable<UserRole> FillterUserRole(string level);

        UserRole FillterUserRoleByLevel(string level);

        UserRole GetById(int id);
    }
}
