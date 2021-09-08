﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DataClassesDataContext : DbContext
    {
        public DataClassesDataContext()
            : base("name=DataClassesDataContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DocumentSign> DocumentSigns { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<EmailActive> EmailActives { get; set; }
        public virtual DbSet<EmailActiveFileAttach> EmailActiveFileAttaches { get; set; }
        public virtual DbSet<EmailServer> EmailServers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeSign> EmployeeSigns { get; set; }
        public virtual DbSet<EmployeeSignDetail> EmployeeSignDetails { get; set; }
        public virtual DbSet<FileSign> FileSigns { get; set; }
        public virtual DbSet<LoginUser> LoginUsers { get; set; }
        public virtual DbSet<MyCompany> MyCompanies { get; set; }
        public virtual DbSet<RoleOfUser> RoleOfUsers { get; set; }
        public virtual DbSet<SignOfUser> SignOfUsers { get; set; }
        public virtual DbSet<ThreadedSignDocument> ThreadedSignDocuments { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserRoleDetail> UserRoleDetails { get; set; }
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
    
        public virtual int Dongbo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Dongbo");
        }
    
        public virtual int Dongbo_mauhoadon()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Dongbo_mauhoadon");
        }
    }
}