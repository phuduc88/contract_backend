
namespace Contract.Common
{
    public enum ResultCode : int
    {
        #region Common error codes (000 ~ 099)

        NoError = 1,
        UnknownError,
        TokenInvalid,
        NotFoundResourceId,
        IdNotMatch,
        NotModified,
        WaitNextRequest,
        DataInvalid = 8,
        DataIsUsed = 9,
        FileLarge = 10,
        SOAPServiceNotSuccefull,

        #endregion Common error codes

        #region System error codes (100 ~ 199)

        SystemConfigNotFound = 100,
        SystemConfigInvalid,
        FileNotFound,
        ReadWriteFileError,

        #endregion System error codes

        #region Client error codes (200 ~ 199)

        RequestDataInvalid = 200,
        NotEnoughPermission,

        #endregion Client error codes

        // Login, Session error codes
        UserIsDisabled = 300,
        UserNotFound,
        LoginFailed,
        SessionAlive,
        SessionEnded,

        #region Common error codes (1000 ~ 1999)
        NotAuthorized = 1000,
        NotPermisionData,
        TimeOut,

        #endregion User operation error codes

        #region Login error codes (2000 ~ 2009)
        LoginUserIdIsEmpty = 2000,
        LoginUserIdNotExist,
        LoginEmailInvalid,
        LoginEmailNotExist,
        LoginPasswordIsEmpty,
        LoginOldPasswordIncorrect,
        #endregion

       
        #region User Account Management error codes (2020 ~ 2039)
        UserAccountMgtUsernameExceedMaxLength = 2020,
        UserAccountMgtUsernameIsEmpty,
        UserAccountMgtEmailExceedMaxLength,
        UserAccountMgtPasswordExceedMaxLength,
        UserAccountMgtEmailIsEmpty,
        UserAccountMgtEmailInvalid,
        UserAccountMgtPasswordIsEmpty,
        UserAccountMgtConflictResourceUserId = 2027,
        UserAccountMgtUserIdIsEmpty = 2028,
        UserAccountMgtUserIdExceedMaxLength,
        UserAccountMgtConflictResourceEmail,
        UserAccountMgtCompanyIdNotFound = 2031,
        UserAccountMgtNotPermissionDelete,
        UserAccountMgtConflictResourceTaxCode = 2033,
        UserAccountMgtConflictResourceClientCode = 2034,
        UserAccountMgtIsDefault = 2035,

        #endregion Project management error codes

        #region Employee error code (2040 ~ 2060)
		EmployeeContantsInDeclatation = 2040,
	    #endregion
         
        #region Company error Code (3000 ~ 3029)

        CompanyNameBlank = 3000,
        CompanyAddressBlank,
        CompanyAdminNameBlank,
        CompanyAdminEmailBlank,
        CompanyTaxInvalid,
        CompanyEnglishCompanyNameMaxLength,
        CompanyLocalCompanyNameMaxLength,
        CompanyEnglishAddressMaxLength,
        CompanyLocalAddressMaxLength,
        CompanyFirstPhoneNumberMaxLength,
        CompanyPhoneNumberMaxLength =3010,
        CompanyFaxNumberMaxLength =3011,
        CompanyHomepageMaxLength,
        CompanyAdminNameMaxLength,
        CompanyAdminEmailMaxLength,
        CompanyAdminEmailInvalid,
        CompanyNameMaxLength = 3016,
        CompanyNameIsExitsTaxCode = 3017,
        CompanyNameIsExitsEmail = 3018,
        CompanyNameCannotCreated= 3019,
        CompanyNameIsExitIsuranceCode = 3020,
        CompanyNameNotYetRegisterIVAN = 3021,

        #endregion
        
        #region ImportData
        ImportDataSizeOfFileTooLarge = 5050,
            ImportFileFormatInvalid = 5051,
            ImportColumnIsNotExist,
            ImportDataIsEmpty = 5053,
            ImportDataExceedMaxLength,
            ImportDataFormatInvalid,
            ImportDataNotSuccess,
            ImportDataIsExisted =5057,
            ImportDataIsNotNumberic =5058,
            ImportDataIsNotDateTime,
            ImportDataDaeteOfInvoiceInvalid = 5060,
            ImportDataNotFound= 5061,
        #endregion

        #region Contract
        ContractSended = 8000, // Hợp đồng đã được lập tờ khai
        ContractRollBack, // Khôi phục lại hợp đồng
        ContractConnectionContract,
        ContractNotExisted,
        ContractCustomerNotYetUse,
        ContractLoginUserIsEmpty = 8004,
        ContractDownloaded = 8005,
        ContractCustomerUsedInvoice,
        #endregion

        #region Agencies
        AgenciesHasContract = 9000,
        #endregion

        #region Customer
        CustomerHasContract = 9050,
        #endregion
         
        #region SOAP Error Code

        SOAPTimeOut = 9100,
        SOAPErrorSystem,
        SOAPInvalidImputData,
        SOAPInvalidOutputData,
        SOAPErrorBackEnd,
        SOAPNotConnectionBackend,
        SOAPTimeout,
        SOPAObjectAlreadyExist,
        SOAPObjectNotFound,
        SOAPRequire,
        SOAPUnknownError = 9200,
        #endregion

    }
}
