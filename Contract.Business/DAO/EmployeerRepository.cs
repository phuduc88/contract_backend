using Contract.Business.Models;
using Contract.Data.DBAccessor;
using Contract.Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using Contract.Business.Constants;
using System.Data.SqlClient;
using System.Data;

namespace Contract.Business.DAO
{
    public class EmployeersRepository : GenericRepository<Employee>, IEmployeesRepository
    {
        public EmployeersRepository(IDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Get an IEnumerable Employeer 
        /// </summary>
        /// <returns><c>IEnumerable Employeer</c> if Employeer not Empty, <c>null</c> otherwise</returns>
        public IEnumerable<Employee> GetList()
        {
            return GetEmployeerActive();
        }

        /// <summary>
        /// Get an Employeer by CompanySID.
        /// </summary>
        /// <param name="id">The condition get Employeer.</param>
        /// <returns><c>Operator</c> if CompanySID on database, <c>null</c> otherwise</returns>
        public Employee GetById(int id)
        {
            return this.dbSet.FirstOrDefault(p => p.Id == id && (p.Active ?? false));
        }

        private IQueryable<Employee> GetEmployeerActive()
        {
            return dbSet.Where(p => (p.Active ?? false));
        }

        public IEnumerable<Employee> Filter(ConditionSearchEmployeer condition)
        {
            var employees = GetEmployeerActive().Where(p => p.CompanyId == condition.CompanyId);
            if (condition.FullName.IsNotNullOrEmpty())
            {
                employees = employees.Where(p => p.FullName.ToUpper().Contains(condition.FullName.ToUpper()));
            }

            if (condition.Code.IsNotNullOrEmpty())
            {
                employees = employees.Where(p => p.Code.ToUpper().Contains(condition.Code.ToUpper()));
            }

           
            if (condition.Gender.HasValue)
            {
                employees = employees.Where(p => p.Gender == condition.Gender.Value);
            }

            return employees;
        }

        public IEnumerable<Employee> FilterGroup(ConditionSearchEmployeer condition)
        {
            var employees = GetEmployeerActive().Where(p => p.CompanyId == condition.CompanyId);
            if ((int)EmployeeSearch_Type.Code == condition.Type && condition.Keywords.Count > 0)
            {
                employees = employees.Where(p => condition.Keywords.Contains(p.Code));
            } 

            return employees;
        }


        public IEnumerable<Employee> Filter(int companyId)
        {
            var employees = GetEmployeerActive().Where(p => p.CompanyId == companyId);
            return employees;
        }



        public Employee Filter(EmployeeSearch condition)
        {
            var employees = GetEmployeerActive().Where(p => p.CompanyId == condition.CompanyInfo.Id);
            if (condition.FullName.IsNotNullOrEmpty())
            {
                employees = employees.Where(p => p.FullName.ToUpper().Contains(condition.FullName.ToUpper()));
            }

            return employees.FirstOrDefault();
        }
    }
}
