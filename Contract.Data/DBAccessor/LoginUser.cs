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
    
    public partial class LoginUser
    {
        public LoginUser()
        {
            this.RoleOfUsers = new HashSet<RoleOfUser>();
        }
    
        public int UserSID { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Nullable<int> UserRoleSID { get; set; }
        public Nullable<int> CompanySID { get; set; }
        public string Language { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<System.DateTime> LastAccessedTime { get; set; }
        public Nullable<System.DateTime> LastChangedPasswordTime { get; set; }
        public string Mobile { get; set; }
        public Nullable<bool> AccountDefault { get; set; }
        public Nullable<int> EmployeeSignId { get; set; }
    
        public virtual MyCompany MyCompany { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<RoleOfUser> RoleOfUsers { get; set; }
    }
}
