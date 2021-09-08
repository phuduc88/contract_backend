
using Contract.Business.Models;
using System.Collections.Generic;

namespace Contract.Business.Constants
{
    public static class ContentType
    {
        public const string Original = "original";
        public const string User = "user";
    }


    public static class UserPermission
    {

        public const string CompanyManagement_Create = "companyManagement_C";
        public const string CompanyManagement_Read = "companyManagement_R";
        public const string CompanyManagement_Update = "companyManagement_U";
        public const string CompanyManagement_Delete = "companyManagement_D";

        public const string InvoiceRelease_Create = "invoiceRelease_C";
        public const string InvoiceRelease_Read = "invoiceRelease_R";
        public const string InvoiceRelease_Update = "invoiceRelease_U";
        public const string InvoiceRelease_Delete = "invoiceRelease_D";

        public const string InvoiceNotification_Create = "invoiceNotification_C";
        public const string InvoiceNotification_Read = "invoiceNotification_R";
        public const string InvoiceNotification_Update = "invoiceNotification_U";
        public const string InvoiceNotification_Delete = "invoiceNotification_D";

        public const string BundleRelease_Create = "bundleRelease_C";
        public const string BundleRelease_Read = "bundleRelease_R";
        public const string BundleRelease_Update = "bundleRelease_U";
        public const string BundleRelease_Delete = "bundleRelease_D";

        public const string ClientManagement_Create = "clientManagement_C";
        public const string ClientManagement_Read = "clientManagement_R";
        public const string ClientManagement_Update = "clientManagement_U";
        public const string ClientManagement_Delete = "clientManagement_D";

        public const string ProductManagement_Create = "productManagement_C";
        public const string ProductManagement_Read = "productManagement_R";
        public const string ProductManagement_Update = "productManagement_U";
        public const string ProductManagement_Delete = "productManagement_D";

        public const string InvoiceManagement_Create = "invoiceManagement_C";
        public const string InvoiceManagement_Read = "invoiceManagement_R";
        public const string InvoiceManagement_Update = "invoiceManagement_U";
        public const string InvoiceManagement_Delete = "invoiceManagement_D";
        public const string InvoiceManagement_Rejected = "invoiceManagement_H";
        public const string InvoiceManagement_Approve = "invoiceManagement_P";

        public const string InvoiceAdjustment_Create = "invoiceAdjustment_C";
        public const string InvoiceAdjustment_Read = "invoiceAdjustment_R";
        public const string InvoiceAdjustment_Update = "invoiceAdjustment_U";
        public const string InvoiceAdjustment_Delete = "invoiceAdjustment_D";

        public const string SubstituteInvoice_Create = "substituteInvoice_C";
        public const string SubstituteInvoice_Read = "substituteInvoice_R";
        public const string SubstituteInvoice_Update = "substituteInvoice_U";
        public const string SubstituteInvoice_Delete = "substituteInvoice_D";

        public const string InvoiceCancel_Create = "invoiceCancel_C";
        public const string InvoiceCancel_Read = "invoiceCancel_R";
        public const string InvoiceCancel_Update = "invoiceCancel_U";
        public const string InvoiceCancel_Delete = "invoiceCancel_D";

        public const string ListInvoiceRelease_Create = "listInvoiceRelease_C";
        public const string ListInvoiceRelease_Read = "listInvoiceRelease_R";
        public const string ListInvoiceRelease_Delete = "listInvoiceRelease_D";
        public const string ListInvoiceRelease_Update = "listInvoiceRelease_U";

        public const string InvoiceCheckout_Create = "invoiceCheckout_C";
        public const string InvoiceCheckout_Read = "invoiceCheckout_R";
        public const string InvoiceCheckout_Delete = "invoiceCheckout_D";
        public const string InvoiceCheckout_Update = "invoiceCheckout_U";

        public const string CancelPayment_Create = "cancelPayment_C";
        public const string CancelPayment_Read = "cancelPayment_R";
        public const string CancelPayment_Delete = "cancelPayment_D";
        public const string CancelPayment_Update = "cancelPayment_U";

        public const string DivisionInformation_Create = "divisionInformation_C";
        public const string DivisionInformation_Read = "divisionInformation_R";
        public const string DivisionInformation_Update = "divisionInformation_U";
        public const string DivisionInformation_Delete = "divisionInformation_D";

        public const string AccountManagement_Create = "accountManagement_C";
        public const string AccountManagement_Read = "accountManagement_R";
        public const string AccountManagement_Update = "accountManagement_U";
        public const string AccountManagement_Delete = "accountManagement_D";

        public const string Report_Read = "report_R";
        public const string DigitalSignature_Read = "digitalSignature_R";

        public const string AgenciesManagement_Create = "agenciesManagement_C";
        public const string AgenciesManagement_Read = "agenciesManagement_R";
        public const string AgenciesManagement_Update = "agenciesManagement_U";
        public const string AgenciesManagement_Delete = "agenciesManagement_D";

        public const string EmailServerManagement_Create = "emailServerManagement_R";
        public const string EmailServerManagement_Update = "emailServerManagement_U";

        public const string EmailActiveManagement_Create = "emailActiveManagement_R";
        public const string EmailActiveManagement_Update = "emailActiveManagement_U";

        public const string ContractManagement_Create = "contractManagement_C";
        public const string ContractManagement_Read = "contractManagement_R";
        public const string ContractManagement_Update = "contractManagement_U";
        public const string ContractManagement_Delete = "contractManagement_D";
        public const string ContractManagement_Approve = "contractManagement_A";

        public const string CustomerManagement_Create = "customerManagement_C";
        public const string CustomerManagement_Read = "customerManagement_R";
        public const string CustomerManagement_Delete = "customerManagement_D";
        public const string CustomerManagement_Update = "customerManagement_U";

        public const string EmployeesManagement_Create = "employeesManagement_C";
        public const string EmployeesManagement_Read = "employeesManagement_R";
        public const string EmployeesManagement_Update = "employeesManagement_U";
        public const string EmployeesManagement_Delete = "employeesManagement_D";

        public const string TemplateManagement_Create = "templateManagement_C";
        public const string TemplateManagement_Read = "templateManagement_R";
        public const string TemplateManagement_Update = "templateManagement_U";
        public const string TemplateManagement_Delete = "templateManagement_D";

        public const string TokenManagement_Create = "tokenManagement_C";
        public const string TokenManagement_Read = "tokenManagement_R";
        public const string TokenManagement_Update = "tokenManagement_U";
        public const string TokenManagement_Delete = "tokenManagement_D";

        public const string CurrenciesManagement_Create = "currenciesManagement_C";
        public const string CurrenciesManagement_Read = "currenciesManagement_R";
        public const string CurrenciesManagement_Update = "currenciesManagement_U";
        public const string CurrenciesManagement_Delete = "currenciesManagement_D";


        public const string InvoiceApproved_Read = "invoiceApproved_R";
        public const string InvoiceApproved_Sign = "invoiceApproved_S";

        public const string InvoiceCreated_Approve = "invoiceCreated_P";
        public const string InvoiceCreated_Delete = "invoiceCreated_R";

        public const string ReportCancelling_Create = "reportCancelling_C";
        public const string ReportCancelling_Read = "reportCancelling_R";

        public const string ReportManagement_Read = "reportManagement_R";
        public const string ReportInvoiceByUser_Read = "reportInvoiceByUser_R";

        public const string AdminInvoiceManagement_Read = "adminInvoiceManagement_R";
        public const string AdminInvoiceManagement_Update = "adminInvoiceManagement_U";
        public const string AdminCustomerManagement_Read = "adminCustomerManagement_R";
        public const string AdminCustomerManagement_Update = "adminCustomerManagement_U";

    }

    public static class MsgApiResponse
    {
        public const string ExecuteSeccessful = "Successful";
        public const string DataInvalid = "Data information is invalid.";
        public const string NotAuthorized = "You are not authorized to use this function";
        public const string ResouceIdNotFound = "Id is not found in data of client";
        public const string DataNotFound = "Data not found";
        public const string HaveNotPermissionData = "Have not permission data";
        public const string CacheNotDataResponse = "Cache not data response";
        public const string IsExistIsuranceCode = "Isurance code is existed in data.";
        public const string FileUploadIvalid = "File upload isvalid";
    }

    public static class MsgApiContractResponse
    {
        public const string CanNotChangeContractSended = "Can't change status contract because it is sended";
        public const string CanNotChangeContractRollBack = "Only change the status of the contract when the contract is canceled";
        public const string CanNotConnectionContract = "Only connection Contract of the contract when the contract is new";
        public const string ResouceIdNotFound = "Id is not found in data of client";
        public const string DataNotFound = "Data not found";
        public const string HaveNotPermissionData = "Have not permission data";
        public const string CacheNotDataResponse = "Cache not data response";
    }

    public static class Authentication
    {
        public const string LoginSuccessful = "Login successful.";
        public const string LogoutSuccessful = "Logout successful.";
        public const string TokenInvalid = "Token is invalid";
        public const string TokenEnded = "Token is expired.";
        public const string NotAuthentication = "Token not authentication";
        public const string LoginFaild = "Login failed because username or password not match";
        public const string TokenBanned = "Token  was banned";
    }
    public static class EmployeeManagementInfo
    {
        public const string IsContantsInDeclaration = "This is employee is exist in declaration";
    }

    public static class ClientManagementInfo
    {
        public const int ClientNameMaxLength = 150;
        public const int AddressMaxLength = 250;
        public const int TelMaxLength = 25;
        public const int FaxMaxLength = 25;
        public const int ContactPersionMaxLength = 50;
        public const int EmailMaxLength = 50;

        public const string ErrorMsgCheckMaxLength = "Number of characters in the [{0}] exceeded {1} character";

        public const string ClientClientNameIsEmpty = "You have to enter at least a name of client";
        public const string ClientAddressIsEmpty = "You have to enter at least a address of client";
        public const string ClientEmailInvalidFormat = "Format of email is invalid";
        public const string NotPermissionData = "You have not permission to access this client";

        public const string OrderByColumnDefault = "Id";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
            {"NAME","CustomerName"},
            {"CODE","CustomerCode"},
            {"ADDRESS","Address"},
            {"EMAIL","Email"},
            {"MOBILE","Mobile"},
            {"CUSTOMERID","CustomerID"},
        };
    }

    public static class LoginUserInfo
    {

        public const int UserIdMaxLength = 50;
        public const string MsgUserIdMaxLength = "User id exceeds maximum length";
        public const string MsgUserIdIsEmpty = "User name cannot be blank";

        public const int UserNameMaxLength = 500;
        public const string MsgUserNameMaxLength = "User's name exceeds maximum length";
        public const string MsgUserNameIsEmpty = "User name cannot be blank";

        public const int NewPasswordMaxLength = 50;

        public const int CurrentPasswordMaxLength = 50;
        public const string CurrentPasswordOverMaxLenght = "Current password length exceeds maximum length";
        public const string NewPasswordOverMaxLenght = "New password length exceeds maximum length";
        public const string CurrentPasswordIsBlank = "Current password cannot be blank";
        public const string NewPasswordIsBlank = "Current password cannot be blank";

        public const int EmailMaxLength = 50;
        public const string MsgEmailMaxLength = "Email address exceeds maximum length";
        public const string MsgEmailIsEmpty = "Email cannot be blank";
        public const string MsgEmailInvalid = "Format of email is invalid";

        public const int PasswordMaxLength = 50;
        public const string MsgPasswordMaxLength = "Password exceeds maximum length";
        public const string MsgPasswordIsEmpty = "Password cannot be blank";

        public static Dictionary<string, string> OrderByColumn =
             new Dictionary<string, string>(){
                {"USERID", "UserID"},
                {"EMAIL", "Email"}                
            };

        public const string OrderByColumnDefault = "UserID";
        public static Dictionary<string, string[]> PermissionDelete =
            new Dictionary<string, string[]>();
    }

    public static class PlaceHolder
    {
        public const string CompanyInfo = "$CompanyName";
        public const string CompanyCode = "$CompanyCode";
        public const string CompanyAddress = "$CompanyAddress";
        public const string ContractCode = "$ContractCode";
        public const string TaxCode = "$TaxCode";
        public const string CompanyPhone = "$CompanyPhone";
        public const string CompanyEmail = "$CompanyEmail";
        public const string DocumentNo = "$DocumentNo";
        public const string CreateBy = "$CreatedBy";
        public const string SignBy = "$SignBy";
        public const string DateAddAddress = "$DateTimeAndAdrress";
        public const string BankAccount = "$BankAccount";
        public const string OpenAddress = "$OpenAddress";
        public const string Branch = "$Branch";
        public const string Reason = "$Reason";

        public const string BenefitLevelName = "$BenefitLevelName";
        public const string Resource = "$Resource";
        public const string BenefitLevelCode = "$BenefitLevelCode";
        public const string SalaryBase = "$SalaryBase";
        public const string TyleNSNN = "$TyleNSNN ";

        //DT02
        public const string NumberBHXHHT = "$SoSoBHXHHT";
        public const string NumberBHYTHT = "$SoTheBHYTHT";


        public const string NotificationUserNotLogin = "NOTIFICATION_USERNOTLOGIN";
        public const string RemindDealerAdmin = "REMIND_DEALERADMIN";
        public const string NoticeReleaseInvoice = "Notice_Release_Invoice";

        // Place Holder
        public const string PlaceHolderUsername = "{Username}";
        public const string PlaceHolderUserId = "{UserId}";
        public const string PlaceHolderUrl = "{Url}";
        public const string PlaceHolderEmail = "{Email}";
        public const string PlaceHolderPassword = "{Password}";
        public const string PlaceHolderOldPassword = "{OldPassword}";
        public const string PlaceHolderOldEmail = "{OldEmail}";
        public const string PlaceHolderOldUsername = "{OldUsername}";
        public const string PlaceHolderLastAccessedTime = "{LastAccessedTime}";

        public const string PlaceHolderCompanyName = "$CompanyName";
        public const string PlaceHolderCustomerName = "$CustomerName";
        public const string PlaceHolderVerificationCode = "$VerificationCode";
        public const string PlaceHolderInvoiceNo = "$invNumber";
        public const string PlaceHolderInvoicePattern = "$pattern";
        public const string PlaceHolderInvoiceSeria = "$serial";
        public const string PlaceHolderClientId = "$clientId";
        public const string PlaceHolderClientPassword = "$clientPassword";

        public const string PlaceHolderInvoiceNoOld = "$NewInvMumber";
        public const string PlaceHolderInvoicePatternOld = "$Newpattern";
        public const string PlaceHolderInvoiceSeriaOld = "$NewSerial";
        public const string PlaceHolderInvoiceLinkDowLoad = "$DestroyRecordUrl";

        public const string PlaceHolderInvoiceDate = "@InvoiceDate";
        public const string PlaceHolderInvoiceTotal = "$Total";
        public const string PlaceHolderInvoiceTotalTax = "$TaxAmount";
        public const string PlaceHolderInvoiceSum = "$Sum";
    }

    public static class CompanyManagementInfo
    {

        public const int EnglishCompanyNameMaxLength = 150;
        public const string MsgEnglishCompanyNameMaxLength = "English name exceeds maximum length";

        public const int LocalCompanyNameMaxLength = 150;
        public const string MsgLocalCompanyNameMaxLength = "Local name exceeds maximum length";

        public const string MsgCompanyNameCannotBeBlank = "Company name cannot blank";

        public const int EnglishAddressMaxLength = 150;
        public const string MsgEnglishAddressMaxLength = "English address exceeds maximum length";

        public const int LocalAddressMaxLength = 150;
        public const string MsgLocalAddressMaxLength = "Local address exceeds maximum length";

        public const string MsgAddressCannotBeBlank = "Company address cannot blank";

        public const int PhoneNumberMaxLength = 25;
        public const string MsgPhoneNumberMaxLength = "Phone number exceeds maximum length";
        public const string MsgPhoneNumberInvalid = "Phone number is invalid";

        public const int FaxNumberMaxLength = 25;
        public const string MsgFaxNumberMaxLength = "Fax number exceeds maximum length";
        public const string MsgFaxNumberInvalid = "Fax number is invalid";

        public const int HomePageMaxLength = 150;
        public const string MsgHomePageMaxLength = "URL exceeds maximum length";

        public const decimal MinTax = 0;
        public const decimal MaxTax = 99.99M; // Base on database design (Decimal(4,2))
        public const string TaxNotInRange = "Tax must be a positive number and range from 0 to 99.99";

        public const int AdminNameMaxLength = 50;
        public const string MsgAdminNameMaxLength = "Admin's name exceeds maximum length";
        public const string MsgAdminNameCannotBeBlank = "Admin's name cannot be blank";

        public const int AdminEmailMaxLength = 50;
        public const string MsgAdminEmailMaxLength = "Admin's email address exceeds maximum length";
        public const string MsgAdminEmailCannotBeBlank = "Admin's email address cannot be blank";
        public const string MsgAdminEmailInvalid = "Admin's email address is invalid";

        public const string MsgCountryOfUserNotExits = "The Country of user does not exists";
        public const string MsgRoleDoesNotExists = "The Role of User does not exists";
        public const string MsgDealerAdminNotFound = "Cannot get dealer admin account of the Company";
        public const string NotPermissionData = "You have not permission to access this company";

        public const int CompanyNameMaxLength = 150;
        public const string MsgCompanyNameMaxLength = "Company name exceeds maximum length";
    }

    public static class CompanySortColumn
    {
        public const string OrderByColumnDefault = "CompanySID";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"NAME","CompanyName"},
           {"TAX","TaxCode"},
           {"EMAIL","Email"},
           {"PERSONCONTACT","PersonContact"},
           {"TEL","Tel1"},
        };
    }

    public static class AgenciesSortColumn
    {
        public const string OrderByColumnDefault = "CompanySID";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"NAME","CompanyName"},
           {"ADDRESS","Address"},
           {"EMAIL","Email"},
           {"PERSONCONTACT","PersonContact"},
           {"TEL","Tel1"},
        };
    }

    public static class EmployeerSortColumn
    {
        public const string OrderByColumnDefault = "Id";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"FULLNAME","FullName"},
           {"ID","Id"},
           {"CODE","Code"},
           {"ContractCODE","ContractCode"},
           {"ContractNO","ContractNo"},
           {"IDENTITYCAR","IdentityCar"},
           {"BIRTHDAY","Birthday"},
           {"HOSPITALFIRSTREGISTNAME","HospitalFirstRegistName"},
        };
    }

    public static class DocumentSortColumn
    {
        public const string OrderByColumnDefault = "DateCreated";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"DECLARATIONCODE","DeclarationCode"},
           {"DOCUMENTNO","DocumentNo"},
           {"DECLARATIONNAME","DocumentFile"},
           {"SENDDATE","SendDate"},
           {"STATUS","Status"},
           {"CREATEDATE","DateCreated"},
        };
    }

    public static class SubmitDeclarationSortColumn
    {
        public const string OrderByColumnDefault = "SendDate";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"SENDDATE","SendDate"},
           {"DECLARATIONNAME","DeclarationName"},
           {"ID","Id"},
           {"DECLARATIONCODE","DeclarationCode"},
        };
    }

    public static class UseInvoiceAction
    {
        public const string OrderByColumnDefault = "MyCompany.CompanyName";
        public static Dictionary<string, string> OrderByColumn =
             new Dictionary<string, string>(){
                {"COMPANYNAME", "MyCompany.CompanyName"},
                {"NAMETAXDEPARTMENT", "TaxDepartment.Name"},
                {"CREATED", "Created"},
                {"Status", "Status"},
            };
    }

    public static class OrderType
    {
        public const string Asc = "ASCEND";
        public const string Desc = "DESCEND";
        public static Dictionary<string, string> DcType =
            new Dictionary<string, string>()
            {
                {"ASCEND", "ASCEND"},
                {"DESCEND", "DESCEND"},
            };
    }

    public static class RoleInfo
    {
        public const string SYSTEMADMIN = "SYSTEM_ADMIN";
        public const string SALE = "SALE";
        public const string SALE_EMPLOYER = "SALE_EMPLOYER";
        public const string CUSTOMER = "CUSTOMER";
        public const string USER = "USER";
        public const string CLIENT = "CLIENT";
    }

    public enum FileTypeUpload
    {
        Logo,
        SignaturePicture
    }

    public static class CacheResponseRegion
    {
        public const string MergeItem = "Merge-Item";
        public const string AppSetting = "App-Setting";
    }

    public enum DownloadFileType
    {
        Excel,
        Pdf
    }

    public static class FileExtension
    {
        public const string DocX = ".docx";
        public const string Doc = ".doc";
        public const string Pdf = ".pdf";
        public const string Xlsx = ".xlsx";
        public const string Xls = ".xls";
        public const string Jpg = ".jpg";
        public const string Jpeg = ".jpeg";
        public const string Png = ".png";
        public static List<string> ExtenFileAllowUpload = new List<string>() { DocX, Doc, Pdf, Xls, Xlsx, Jpeg, Jpg, Png };
        public static Dictionary<string, string> TypeFile = new Dictionary<string, string>
        {
           {DocX,"application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
           {Doc, "application/msword"},
           {Pdf,"application/pdf"},
           {Xls,"application/vnd.ms-excel"},
           {Xlsx, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
           {Jpg, "image/jpeg"},
           {Jpeg, "image/jpeg"},
           {Png, "image/png"},
        };
    } 

    public static class NotificationManagementInfo
    {
        public const int SubjectMaxLength = 150;
        public const string MsgSubjectIsEmpty = "Subject is required";
        public const string MsgSubjectMaxLength = "Subject exceeds maximum length";

        public const int ContentNameMaxLength = 500;
        public const string MsgContentIsEmpty = "Content is required";
        public const string MsgContentMaxLength = "Content exceeds maximum length";
    }

    public static class ReleaseInvoiceSortColumn
    {
        public const string OrderByColumnDefault = "Id";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"CODE","InvoiceCode"},
           {"SYMBOL","InvoiceSymbol"},
           {"NO","InvoiceNo"},
           {"CUSTOMERNAME","CustomerName"},
           {"RELEASESTATUS","Status"},
           {"DATEINVOICE","InvoiceDate"},
           {"NOTE","InvoiceNote"},
        };
    }

    public static class ReleaseListInvoiceSortColumn
    {
        public const string OrderByColumnDefault = "Id";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"DESCRIPTION","Description"},
           {"ID","id"},
           {"DATERELEASE","DateRelease"},
        };
    }

    public static class InvoiceSortColumn
    {
        public const string OrderByColumnDefault = "Id";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"CODE","InvoiceCode"},
           {"SYMBOL","InvoiceSymbol"},
           {"NO","InvoiceNo"},
           {"CUSTOMERNAME","CustomerName"},
           {"NUMBERACCOUNT","NumberAccount"},
           {"DATEINVOICE","InvoiceDate"},
           {"STATUS","InvoiceStatus"},
           {"PAYMENTED","Paymented"},
           {"NOTE","InvoiceNote"},
           {"ID","InvoiceNo"},
        };
    }

    public static class ReportDetailUseSortColumn
    {
        public const string OrderByColumnDefault = "InvoiceCode";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"CODE","InvoiceCode"},
           {"SYMBOL","InvoiceSymbol"},
           {"NO","InvoiceNo"},
           {"CUSTOMERNAME","CustomerName"},
           {"CUSTOMERCODE","CustomerCode"},
           {"DATERELEASE","DateRelease"},
           {"INVOICETYPE","invoiceType"},
           {"TOTAL","Total"},
        };
    }


    public static class ReplacesInvoiceSortColumn
    {
        public const string OrderByColumnDefault = "Code";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"CODE","Code"},
           {"CODESUBSTITUTE","CodeSubstitute"},
           {"SYMBOL","Symbol"},
           {"SYMBOLSUBSTITUTE","SymbolSubstitute"},
           {"NO","No"},
           {"NOSUBSTITUTE","NoSubstitute"},
           {"NOTE","Note"},
           {"NOTESUBSTITUTE","NoteSubstitute"},
        };
    }

    public static class InvoiceCompanyUseSortColumn
    {
        public const string OrderByColumnDefault = "Code";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"CODE","Code"},
           {"ADDRESS","Name"},
        };
    }

    public static class ScheduleJobInfo
    {
        public const int RoleIdDealer = 5;//DEALER_USER
        public const int RoleDealerAdmin = 4;//DEALER_ADMIN

        public const string RecurringJobQueue = "RECURRING";
        public const string BackgroundJobQueue = "BACKGROUNDJOB";
        public const string DelayedJobQueue = "DELAYED";

        public const string DailyCheckUserLoginJobId = "ScheduleJobBusiness.NotificationUsersNotLogin";
        public const string DailyReminDealerAdmin = "ScheduleJobBusiness.RemindCheckUser";
        public const string DailyAutomaticResetPassword = "ScheduleJobBusiness.AutoResetPasswords";

        public const string RecuringDaily = "Daily";
        public const string RecuringMonthly = "Monthly";
        public const string Minutely = "Minutely";
    }
    public static class EmailType
    {
        public const string ResetPassword = "RESET_PASSWORD";
    }
    public static class AssetData
    {
        public const string Contract = "Contract";
        public const string Declaration = "Declaration";
        public const string DocumentTemplates = "DocumentTemplates";
    }

    public static class CustomerLevel
    {
        public const string Customer = "Cus";
        public const string Sellers = "Seller";
    }

    public static class ContractSortColumn
    {
        public const string OrderByColumnDefault = "Id";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"COMPANYID","CompanyId"},
           {"CUSTOMERNAME","CustomerName"},
           {"TAXTCODE","TaxtCode"},
           {"NO","ContractNo"},
           {"NUMBERINVOICE","NumberInvoice"},
           {"PAID","Paid"},
        };
    }

    public static class CustomerSortColumn
    {
        public const string OrderByColumnDefault = "CompanySID";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"ID","CompanySID"},
           {"NAME","CompanyName"},
           {"TAX","TaxCode"},
           {"ADDRESS","Address"},
           {"DELEGATE","Delegate"},
           {"TEL","Tel"},
           {"FAX","Fax"},
           {"BANKACCOUNT","BankAccount"},
           {"BANKNAME","BankName"},
           {"PERSONCONTACT","PersonContact"},
           {"MOBILE","Mobile"},
           {"EMAIL","Email"},
           {"ACTIVE","Active"},
        };
    }

    public static class ActiveEmailSortColumn
    {
        public const string OrderByColumnDefault = "Id";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"TITLE","Title"},
           {"EMAILTO","EmailTo"},
           {"CONTENT","ContentEmail"},
           {"CREATEDDATE","CreatedDate"},
           {"SENDTEDDATE","SendtedDate"},
           {"STATUS","StatusSend"},
        };
    }

    public static class EmployeelSortColumn
    {
        public const string OrderByColumnDefault = "CreatedDate";
        public static Dictionary<string, string> OrderByColumn = new Dictionary<string, string>
        {
           {"NAME","UserName"},
           {"LOGINID","UserID"},
           {"PASSWORD","Password"},
           {"EMAIL","email"},
           {"CREATEDATE","CreatedDate"},
        };
    }

    public static class CharacterAction
    {
        public const char Create = 'C';
        public const char Update = 'U';
        public const char Delete = 'D';
        public const char Read = 'R';
        public const char Active = 'A';
        public const char Approve = 'P';
        public const char Sign = 'S';
        public const char Rejected = 'H';
    }

    public static class AssetSignXML
    {
        public const string Release = "Release";
        public const string SignFile = "Sign";
        public const string DeliverBHXH = "Deliver";
        public const string TempSignFile = "Temp";
        public const string TemplateInvoiceFolder = "DeclarationTemplate";
    }
    public static class ImportDeclaration
    {
        public static List<string> ColumnImport600 = new List<string>
        { 
            {InvoiceIndentity},
            //{InvoiceDetailProductName},
            {InvoiceDetailProductCode},
            {InvoiceDetailUnit},
            {InvoiceDetailQuantity},
            {InvoiceDetailPrice},
            {InvoiceDetailTotal},
            {InvoiceDetailDiscount},
            {InvoiceDetailAmountDiscount},
            {InvoiceDetailTax},
            {InvoiceAmountlTax},
            {InvoiceDetailVendor},
            {InvoiceDetailLo},
            {InvoiceDate},
            {Client},
            {CompanyName},
            {ClientTaxCode},
            {ClientAddress},
            {ClientAccount},
            {TypePayment},
            {PaymentStatus},
            {ClientEmail},
            {Phone},
            {Decription},
            {InvoiceTax},
        };

        public const string InvoiceIndentity = "STT";
        public const string LoaiToKhai = "Loại khai báo";
        public const string InvoiceDetailProductCode = "Ho";
        public const string InvoiceDetailUnit = "DonViTinh";
        public const string InvoiceDetailDiscount = "ChietKhau";
        public const string InvoiceDetailAmountDiscount = "SoTienChietKhau";
        public const string InvoiceDetailTax = "ThueSuat";
        public const string InvoiceAmountlTax = "SoTienThue";
        public const string InvoiceDetailQuantity = "SoLuong";
        public const string InvoiceDetailPrice = "DonGia";
        public const string InvoiceDetailTotal = "ThanhTien";
        public const string InvoiceDate = "NgayThangNamHD";
        public const string Client = "HoTenNguoiMua";
        public const string CompanyName = "TenDonVi";
        public const string ClientTaxCode = "MaSoThue";
        public const string ClientAddress = "DiaChi";
        public const string ClientAccount = "SoTaiKhoan";
        public const string TypePayment = "HinhThucTT";
        public const string PaymentStatus = "TrangThaiTT";
        public const string ClientEmail = "Email";
        public const string Phone = "DienThoai";
        public const string Decription = "GhiChu";
        public const string InvoiceTax = "ThueGTGT";
        public const string InvoiceAmountTax = "TienThueGTGT";
        public const string SheetName = "Sheet1";
        public const string InvoiceDetailVendor = "NhaSanXuat/NhaCungCap";
        public const string InvoiceDetailLo = "Lo";
        public static List<string> SheetNames = new List<string>
        { 
            {SheetName},
        };

        public const int MaxLength25 = 25;
        public const int MaxLength50 = 50;
        public const int MaxLength12 = 12;
    }
}
