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
    
    public partial class ThreadedSignDocument
    {
        public int Id { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public int DocumentTypeId { get; set; }
        public Nullable<int> GroupType { get; set; }
        public Nullable<bool> ReceptionEmail { get; set; }
        public Nullable<bool> ReceptionFileCopy { get; set; }
        public string GroupName { get; set; }
        public string Email { get; set; }
        public string TaxCode { get; set; }
        public Nullable<int> Orders { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<int> UserCreate { get; set; }
        public string PhoneNumber { get; set; }
        public string IdNumber { get; set; }
        public string Adrress { get; set; }
        public string Name { get; set; }
    }
}