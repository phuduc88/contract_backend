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
    
    public partial class RoleOfUser
    {
        public int Id { get; set; }
        public Nullable<int> UserSID { get; set; }
        public Nullable<int> UserRoleDetailId { get; set; }
        public string FunctionName { get; set; }
        public string Action { get; set; }
    
        public virtual LoginUser LoginUser { get; set; }
    }
}
