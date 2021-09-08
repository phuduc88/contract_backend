using Contract.Business.Models;
using Contract.Common.Extensions;
using Contract.Data.DBAccessor;
using System;

namespace Contract.Business.BL
{
    public static class TransferData
    {
        public static void CopyData(this LoginUser toObject, AccountInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            if (toObject.UserSID == 0)
            {
                toObject.UserID = fromObject.UserID;
            }

            if (fromObject.Password.IsNotNullOrEmpty())
            {
                toObject.Password = fromObject.Password.ToStrim();
            }

            toObject.UserName = fromObject.UserName.ToStrim();
            toObject.Email = fromObject.Email;
            toObject.Mobile = fromObject.Mobile;
            toObject.IsActive = true;
            toObject.AccountDefault = fromObject.AccountDefault;
        }

         
        public static void CopyData(this MyCompany toObject, CompanyInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.CompanyName = fromObject.CompanyName;
            toObject.TaxCode = fromObject.TaxCode;
            toObject.Address = fromObject.Address;
            toObject.Tel1 = fromObject.Tel;
            toObject.Fax = fromObject.Fax;
            toObject.Email = fromObject.Email;
            toObject.PersonContact = fromObject.PersonContact;
            toObject.Delegate = fromObject.Delegate;
            toObject.BankAccount = fromObject.BankAccount;
            toObject.BankName = fromObject.BankName;
            toObject.Description = fromObject.Description;
            toObject.AccountHolder = fromObject.AccountHolder;
            toObject.WebSite = fromObject.WebSite;
        }

        public static void CopyData(this FileSign toObject, FileSignInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.CompanyId = fromObject.CompanyId;
            toObject.FileSourceType = fromObject.FileSourceType;
            toObject.FileConvertType = fromObject.FileConvertType;
            toObject.FileName = fromObject.FileName;
            toObject.FileSize = fromObject.FileSize;
            toObject.FileConvert = fromObject.FileConvert;
            toObject.NumberPage = fromObject.NumberPage;
            toObject.FileNameSave = fromObject.FileNameSave;
            toObject.Orders = fromObject.Orders;
            toObject.DocumentSignId = fromObject.DocumentSignId;
            toObject.Status = fromObject.Status;
        }

        public static void CopyData(this EmployeeSign toObject, EmployeeSignInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.DocumentSingId = fromObject.DocumentSingId;
            toObject.ThreadedSignDocumentId = fromObject.ThreadedSignDocumentId;
            toObject.OrderSign = fromObject.OrderSign;
        }

        public static void CopyData(this EmployeeSignDetail toObject, EmployeeSignDetailInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.EmployeeSignId = fromObject.EmployeeSignId;
            toObject.FileSignId = fromObject.FileSignId;
            toObject.SignType = fromObject.SignType;
            toObject.Page = fromObject.Page;
            toObject.CoordinateX = fromObject.CoordinateX;
            toObject.CoordinateY = fromObject.CoordinateY;
            toObject.Scale = fromObject.Scale;
            toObject.Width = fromObject.Width;
            toObject.Height = fromObject.Height;
            toObject.OrderLink = fromObject.OrderLink;
        }

        public static void CopyData(this DocumentSign toObject, DocumentSignInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.CompanyId = fromObject.CompanyId;
            toObject.DocumentType = fromObject.DocumentType;
            toObject.Status = fromObject.Status;
            toObject.MyselfSign = fromObject.MyselfSign;
            toObject.CurrentStep = fromObject.CurrentStep;
        }

        public static void CopyData(this DocumentType toObject, DocumentTypeInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.DocumentName = fromObject.DocumentName;
            toObject.DocType = fromObject.DocType;
            toObject.Orders = fromObject.Orders;
        }

        public static void CopyData(this SignOfUser toObject, SignOfUserInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.CompanyId = fromObject.CompanyId;
            toObject.UserId = fromObject.UserId;
            toObject.FileName = fromObject.FileName;
            toObject.UseDefault = fromObject.UseDefault;
            toObject.IsDraw = fromObject.IsDraw;
            toObject.Extension = fromObject.Extension;
        }

        public static void CopyData(this ThreadedSignDocument toObject, ThreadedSignDocumentInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.CompanyId = fromObject.CompanyId;
            toObject.DocumentTypeId = fromObject.DocumentTypeId;
            toObject.GroupType = fromObject.GroupType;
            toObject.ReceptionEmail = fromObject.ReceptionEmail;
            toObject.ReceptionFileCopy = fromObject.ReceptionFileCopy;
            toObject.GroupName = fromObject.GroupName;
            toObject.TaxCode = fromObject.TaxCode;
            toObject.Email = fromObject.Email;
            toObject.Orders = fromObject.Orders;
            toObject.Name = fromObject.Name;
            toObject.Adrress = fromObject.Adrress;
        }

         
        public static void CopyData(this Employee toObject, EmployeeInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.FullName = fromObject.FullName;
            toObject.CompanyId = fromObject.CompanyId;
            toObject.Code = fromObject.Code;
            toObject.Email = fromObject.Email;
            toObject.Gender = fromObject.Gender;
            toObject.Mobile = fromObject.Mobile;
            toObject.UsingHSM = fromObject.UsingHSM;
            toObject.Active = fromObject.Active;
        }

        public static void CopyData(this MyCompany toObject, MyCompanyInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }
            toObject.CompanyName = fromObject.CompanyName;
            toObject.Address = fromObject.Address;
            toObject.AddressRegister = fromObject.AddressRegister;
            toObject.TaxCode = fromObject.TaxCode;
            toObject.Email = fromObject.Email;
            toObject.PersonContact = fromObject.PersonContact;
            toObject.Position = fromObject.Position;
            toObject.Tel1 = fromObject.Tel;
            toObject.Mobile = fromObject.Mobile;
            toObject.Fax = fromObject.Fax;
            toObject.Delegate = fromObject.Delegate;
            toObject.BankAccount = fromObject.BankAccount;
            toObject.AccountHolder = fromObject.AccountHolder;
            toObject.BankName = fromObject.BankName;
            toObject.Description = fromObject.Description;
            toObject.LogoFileName = fromObject.Logo;
            toObject.Level_Agencies = fromObject.Level_Agencies;
            toObject.EmailOfContract = fromObject.EmailOfContract;
        }

        public static void CopyData(this MyCompany toObject, TokenInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }
            toObject.PrivateKey = fromObject.PrivateKey;
            toObject.VendorToken = fromObject.VendorToken;
            toObject.FromDate = fromObject.FromDate;
            toObject.Expired = fromObject.Expired;
        }

        public static void CopyData(this MyCompany toObject, ContractInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }
            toObject.Address = fromObject.Address;
            toObject.AddressRegister = fromObject.AddressRegister;
            toObject.TaxCode = fromObject.TaxCode;
            toObject.EmailOfContract = fromObject.EmailOfContract;
            toObject.PersonContact = fromObject.PersonContact;
            toObject.Position = fromObject.Position;
            toObject.Tel1 = fromObject.Tel;
            toObject.Mobile = fromObject.Mobile;
            toObject.Fax = fromObject.Fax;
            toObject.Delegate = fromObject.Delegate;
            toObject.BankAccount = fromObject.BankAccount;
            toObject.AccountHolder = fromObject.AccountHolder;
            toObject.BankName = fromObject.BankName;
            toObject.Description = fromObject.Description;
            toObject.LogoFileName = fromObject.Logo;
            toObject.Level_Agencies = fromObject.Level_Agencies;
            toObject.CityId = fromObject.CityId;
            toObject.CityCode = fromObject.CityCode;
            toObject.CompanyID = fromObject.CompanyID;
            toObject.PrivateKey = fromObject.PrivateKey;
            toObject.VendorToken = fromObject.VendorToken;
            toObject.FromDate = fromObject.FromDate;
            toObject.Expired = fromObject.Expired;
            toObject.Issued = fromObject.Issued;
            toObject.License = fromObject.License;
            toObject.CompanyType = fromObject.CompanyType;
            toObject.Careers = fromObject.Careers;
        }

        public static void CopyData(this LoginUser toObject, ContractAccount fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.UserID = fromObject.UserId;
            toObject.UserName = fromObject.UserName;
            toObject.CompanySID = fromObject.CompanyId;
            toObject.Password = fromObject.Password;
            toObject.Email = fromObject.Email;
        }

        public static void CopyData(this EmailActive toObject, EmailInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            //toObject.CompanyID = fromObject.CompanySID;
            toObject.EmailTo = fromObject.EmailTo;
            toObject.Title = fromObject.Subject;
            toObject.ContentEmail = fromObject.Content;
        }


        public static void CopyData(this MyCompany toObject, CustomerInfo fromObject)
        {
            if (fromObject == null)
            {
                return;
            }

            toObject.CompanyName = fromObject.CompanyName;
            toObject.TaxCode = fromObject.TaxCode;
            toObject.Address = fromObject.Address;
            toObject.AddressRegister = fromObject.AddressRegister;
            toObject.Tel1 = fromObject.Tel;
            toObject.Fax = fromObject.Fax;
            toObject.Email = fromObject.Email;
            toObject.PersonContact = fromObject.PersonContact;
            toObject.Delegate = fromObject.Delegate;
            toObject.BankAccount = fromObject.BankAccount;
            toObject.BankName = fromObject.BankName;
            toObject.AccountHolder = fromObject.AccountHolder;
            toObject.Mobile = fromObject.Mobile;
            toObject.Position = fromObject.Position;
            toObject.CityId = fromObject.CityId;
            toObject.CityCode = fromObject.CityCode;
            toObject.WebSite = fromObject.WebSite;
        }
        
    }
}
