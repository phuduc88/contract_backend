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
    
    public partial class Employee
    {
        public int Id { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Nullable<int> Gender { get; set; }
        public string Mobile { get; set; }
        public Nullable<bool> UsingHSM { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<bool> CreateAccount { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual MyCompany MyCompany { get; set; }
    }
}
