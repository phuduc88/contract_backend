//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Contract.Data.DBAccessor
{
    using System;
    using System.Collections.Generic;
    
    public partial class MyCompany
    {
        public MyCompany()
        {
            this.Contracts = new HashSet<Contract>();
            this.Employees = new HashSet<Employee>();
            this.LoginUsers = new HashSet<LoginUser>();
        }
    
        public int CompanySID { get; set; }
        public string CompanyType { get; set; }
        public string IsurranceCode { get; set; }
        public string CompanyName { get; set; }
        public string TaxCode { get; set; }
        public string Issued { get; set; }
        public string Careers { get; set; }
        public string License { get; set; }
        public string Address { get; set; }
        public string AddressRegister { get; set; }
        public string Tel1 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string PersonContact { get; set; }
        public string Delegate { get; set; }
        public string BankAccount { get; set; }
        public string AccountHolder { get; set; }
        public string BankName { get; set; }
        public Nullable<int> CityId { get; set; }
        public string CityCode { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string LogoFileName { get; set; }
        public string Level_Customer { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<bool> Active { get; set; }
        public string Mobile { get; set; }
        public string Position { get; set; }
        public string WebSite { get; set; }
        public Nullable<int> Level_Agencies { get; set; }
        public string EmailOfContract { get; set; }
        public Nullable<bool> AutoSendEmail { get; set; }
        public string PrivateKey { get; set; }
        public string VendorToken { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> Expired { get; set; }
    
        public virtual City City { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<LoginUser> LoginUsers { get; set; }
    }
}
