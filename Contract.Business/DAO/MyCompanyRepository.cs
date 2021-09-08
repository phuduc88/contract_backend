using Contract.Business.Models;
using Contract.Data.DBAccessor;
using Contract.Common.Extensions;
using System.Collections.Generic;
using System.Linq;
using Contract.Business.Constants;

namespace Contract.Business.DAO
{
    public class MyCompanyRepository : GenericRepository<MyCompany>, IMyCompanyRepository
    {
        public MyCompanyRepository(IDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Get an IEnumerable MyCompany 
        /// </summary>
        /// <returns><c>IEnumerable MyCompany</c> if MyCompany not Empty, <c>null</c> otherwise</returns>
        public IEnumerable<MyCompany> GetList()
        {
            return GetMyCompanyActive();
        }

        /// <summary>
        /// Get an MyCompany by CompanySID.
        /// </summary>
        /// <param name="id">The condition get MyCompany.</param>
        /// <returns><c>Operator</c> if CompanySID on database, <c>null</c> otherwise</returns>
        public MyCompany GetById(int id)
        {
            return this.dbSet.FirstOrDefault(p => p.CompanySID == id && !(p.Deleted ?? false));
        }

        /// <summary>
        /// Get an MyCompany by ContractCode.
        /// </summary>
        /// <param name="id">The condition get MyCompany.</param>
        /// <returns><c>Operator</c> if CompanySID on database, <c>null</c> otherwise</returns>
        

        private IQueryable<MyCompany> GetMyCompanyActive()
        {
            return dbSet.Where(p => !(p.Deleted ?? false));
        }

        public IEnumerable<MyCompany> FilterCompany(ConditionSearchCompany condition)
        {
            var companies = GetMyCompanyActive().Where(p => p.Level_Customer.Equals(CustomerLevel.Sellers));
            if (!condition.CurrentUser.RoleUser.Level.IsEquals(RoleInfo.SYSTEMADMIN))
            {
                companies = companies.Where(p => (p.CompanyID ?? 0) == condition.CompanyId);
            }

            companies = companies.Where(p => (p.Active ?? false));
            if (!condition.Keyword.IsNullOrEmpty())
            {
                companies = companies.Where(p => (p.CompanyName.ToUpper().Contains(condition.Keyword.ToUpper())
                    || p.Email.ToUpper().Contains(condition.Keyword.ToUpper())
                    || p.TaxCode.ToUpper().Contains(condition.Keyword.ToUpper())));
            }

            return companies;
        }


        public bool ContainEmail(string email)
        {
            bool result = false;
            if (!email.IsNullOrEmpty())
            {
                result = Contains(p => ((p.Email ?? string.Empty).ToUpper()).Equals(email.ToUpper()) && !(p.Deleted ?? false));
            }
            return result;
        }

        public bool ContainTaxCode(string taxCopde)
        {
            bool result = false;
            if (!taxCopde.IsNullOrEmpty())
            {
                result = Contains(p => ((p.TaxCode ?? string.Empty).Equals(taxCopde) && !(p.Deleted ?? false)));
            }

            return result;
        }

        public bool ContainEmail(int companyId, string email, string levelCustomer)
        {
            bool result = false;
            if (!email.IsNullOrEmpty())
            {
                result = Contains(p => ((p.Email ?? string.Empty).ToUpper()).Equals(email.ToUpper()) && !(p.Deleted ?? false) && p.CompanyID == companyId && p.Level_Customer.Equals(levelCustomer));
            }
            return result;
        }

        public bool ContainTaxCode(int companyId, string taxCopde, string levelCustomer)
        {
            bool result = false;
            if (!taxCopde.IsNullOrEmpty())
            {
                result = Contains(p => ((p.TaxCode ?? string.Empty).Equals(taxCopde) && !(p.Deleted ?? false)) && p.CompanyID == companyId && p.Level_Customer.Equals(levelCustomer));
            }

            return result;
        }

        private IQueryable<CustomerOfCompany> GetCustomerOfCompany(int companyId)
        {
            var customerOfCompany = (from leve1 in dbSet
                                     where leve1.CompanyID == companyId
                                     && !(leve1.Deleted ?? false)
                                     && (leve1.Active ?? false)
                                     select new CustomerOfCompany
                                     {
                                         CompanySID = leve1.CompanySID,
                                         CompanyName = leve1.CompanyName,
                                         CustomerName = leve1.CompanyName,
                                         TaxCode = leve1.TaxCode,
                                         Email = (leve1.EmailOfContract ?? leve1.Email),
                                         PersonContact = leve1.Delegate,
                                         Tel = leve1.Tel1,
                                         Level_Customer = leve1.Level_Customer,
                                         CustomerIdOfCompany = leve1.CompanySID,

                                     }).Union
                                    (from leve1 in dbSet
                                     join leve2 in this.context.Set<MyCompany>()
                                     on leve1.CompanySID equals leve2.CompanyID
                                     where leve1.CompanyID == companyId
                                     && !(leve2.Deleted ?? false)
                                     && (leve2.Active ?? false)
                                     select new CustomerOfCompany
                                     {
                                         CompanySID = leve2.CompanySID,
                                         CompanyName = leve1.CompanyName,
                                         CustomerName = leve2.CompanyName,
                                         TaxCode = leve2.TaxCode,
                                         Email = (leve2.EmailOfContract ?? leve2.Email),
                                         PersonContact = leve2.Delegate,
                                         Tel = leve2.Tel1,
                                         Level_Customer = leve2.Level_Customer,
                                         CustomerIdOfCompany = leve1.CompanySID,
                                     }).Union
                                    (from leve1 in dbSet
                                     join leve2 in this.context.Set<MyCompany>()
                                     on leve1.CompanySID equals leve2.CompanyID
                                     join leve3 in this.context.Set<MyCompany>()
                                     on leve2.CompanySID equals leve3.CompanyID
                                     where leve1.CompanyID == companyId
                                     && !(leve3.Deleted ?? false)
                                     && (leve3.Active ?? false)
                                     select new CustomerOfCompany
                                     {
                                         CompanySID = leve3.CompanySID,
                                         CompanyName = leve1.CompanyName,
                                         CustomerName = leve3.CompanyName,
                                         TaxCode = leve3.TaxCode,
                                         Email = (leve3.EmailOfContract ?? leve3.Email),
                                         PersonContact = leve3.Delegate,
                                         Tel = leve3.Tel1,
                                         Level_Customer = leve3.Level_Customer,
                                         CustomerIdOfCompany = leve1.CompanySID,
                                     }).Union
                                    (from leve1 in dbSet
                                     join leve2 in this.context.Set<MyCompany>()
                                     on leve1.CompanySID equals leve2.CompanyID
                                     join leve3 in this.context.Set<MyCompany>()
                                     on leve2.CompanySID equals leve3.CompanyID
                                     join leve4 in this.context.Set<MyCompany>()
                                     on leve3.CompanySID equals leve4.CompanyID
                                     where leve1.CompanyID == companyId
                                     && !(leve4.Deleted ?? false)
                                     && (leve4.Active ?? false)
                                     select new CustomerOfCompany
                                     {
                                         CompanySID = leve4.CompanySID,
                                         CompanyName = leve1.CompanyName,
                                         CustomerName = leve3.CompanyName,
                                         TaxCode = leve4.TaxCode,
                                         Email = (leve4.EmailOfContract ?? leve4.Email),
                                         PersonContact = leve4.Delegate,
                                         Tel = leve4.Tel1,
                                         Level_Customer = leve4.Level_Customer,
                                         CustomerIdOfCompany = leve1.CompanySID,
                                     });


            return customerOfCompany.AsQueryable();
        }

        public IEnumerable<MyCompany> FilterAllCustomer(ConditionSearchCustomer condition)
        {
            var companies = GetMyCompanyActive();
            companies = companies.Where(p => p.Level_Customer.Equals(CustomerLevel.Customer));
            if (!condition.CustomerName.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.CompanyName.ToUpper().Contains(condition.CustomerName.ToUpper()));
            }

            if (!condition.TaxCode.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.TaxCode.ToUpper().Contains(condition.TaxCode.ToUpper()));
            }

            if (!condition.Delegate.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.Delegate.ToUpper().Contains(condition.Delegate.ToUpper()));
            }

            if (!condition.Tel.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.Tel1.ToUpper().Contains(condition.Tel.ToUpper()));
            }
            if (condition.Status.HasValue)
            {
                companies = companies.Where(p => p.Active == (condition.Status.Value != 0));
            }
            return companies;
        }

        public IEnumerable<MyCompany> FilterCustomer(ConditionSearchCustomer condition, string customerType)
        {
            var companies = GetMyCompanyActive();
            if (condition.ParentCompanyId.HasValue)
            {
                companies = companies.Where(p => p.CompanyID == condition.CompanyID);
            }

            companies = companies.Where(p => p.Level_Customer.Equals(customerType));
            if (!condition.CustomerName.IsNullOrEmpty())
            {
                companies = companies.Where(p => (p.CompanyName.ToUpper().Contains(condition.CustomerName.ToUpper())
                    || p.Delegate.ToUpper().Contains(condition.CustomerName.ToUpper())
                    || p.Tel1.ToUpper().Contains(condition.CustomerName.ToUpper())
                    || p.TaxCode.ToUpper().Contains(condition.CustomerName.ToUpper())));
            }
            return companies;
        }

        public IEnumerable<MyCompany> FilterCustomerManagement(ConditionSearchCustomer condition)
        {
            var companies = GetMyCompanyActive().Where(p => p.Level_Customer.Equals(CustomerLevel.Customer));
            if (!condition.CustomerName.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.CompanyName.ToUpper().Contains(condition.CustomerName.ToUpper()));
            }

            if (!condition.TaxCode.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.TaxCode.ToUpper().Contains(condition.TaxCode.ToUpper()));
            }

            if (!condition.Delegate.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.Delegate.ToUpper().Contains(condition.Delegate.ToUpper()));
            }

            if (!condition.Tel.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.Tel1.ToUpper().Contains(condition.Tel.ToUpper()));
            }
            if (condition.Status.HasValue)
            {
                companies = companies.Where(p => p.Active == (condition.Status.Value != 0));
            }
            return companies;
        }


        public IEnumerable<MyCompany> FilterCustomer(ConditionSearchCustomer condition)
        {
            var companies = GetMyCompanyActive();
            companies = companies.Where(p => p.CompanyID == condition.CompanyID && p.Level_Customer.Equals(CustomerLevel.Customer));
            if (!condition.CustomerName.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.CompanyName.ToUpper().Contains(condition.CustomerName.ToUpper()));
            }

            if (!condition.TaxCode.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.TaxCode.ToUpper().Contains(condition.TaxCode.ToUpper()));
            }

            if (!condition.Delegate.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.Delegate.ToUpper().Contains(condition.Delegate.ToUpper()));
            }

            if (!condition.Tel.IsNullOrEmpty())
            {
                companies = companies.Where(p => p.Tel1.ToUpper().Contains(condition.Tel.ToUpper()));
            }
            if (condition.Status.HasValue)
            {
                companies = companies.Where(p => p.Active == (condition.Status.Value != 0));
            }
            return companies;
        }
    }
}
