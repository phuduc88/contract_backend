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
    
    public partial class SignOfUser
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<bool> IsDraw { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public Nullable<bool> UseDefault { get; set; }
        public string ImageName { get; set; }
        public string FileName { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public string Extension { get; set; }
    }
}
