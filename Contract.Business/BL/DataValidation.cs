using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using Contract.Common.Extensions;
using Contract.Data.DBAccessor;
using System.Text;

namespace Contract.Business.BL
{
    public static class DataValidation
    {
        private const string ErrorMsgCheckMaxLength = "Number of characters in the [{0}] exceeded {1} character";
        private const string ErrorMsgUserIdIsNull = "UserID cannot be blank";
        private const string ErrorMsgPasswordIsNull = "Password cannot be blank";
        public static StringBuilder AppendLineFormat(this StringBuilder sb, string format, params object[] args)
        {
            return sb.AppendFormat(format, args).AppendLine();
        }

        public static bool IsValid(this LoginInfo loginInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;

            if (loginInfo.UserId.IsNullOrEmpty())
            {
                errorDetail = ErrorMsgUserIdIsNull;
                errorCode = ResultCode.LoginUserIdIsEmpty;
                return false;
            }

            if (loginInfo.Password.IsNullOrEmpty())
            {
                errorDetail = ErrorMsgPasswordIsNull;
                errorCode = ResultCode.LoginPasswordIsEmpty;
                return false;
            }

            return true;
        }

        public static bool IsValid(this PasswordInfo passwordInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }

        public static bool IsValid(this TokenInfo tokenInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }

        
        public static bool IsValid(this ContractDetailInfo contractInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }


        public static bool IsValid(this LoginUser userInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;

            if (userInfo.UserID.IsNullOrEmpty())
            {
                errorDetail = LoginUserInfo.MsgUserIdIsEmpty;
                errorCode = ResultCode.UserAccountMgtUserIdIsEmpty;
                return false;
            }

            if (userInfo.UserID.IsOverLength(LoginUserInfo.UserIdMaxLength))
            {
                errorDetail = LoginUserInfo.MsgUserIdMaxLength;
                errorCode = ResultCode.UserAccountMgtUserIdExceedMaxLength;
                return false;
            }

            if (userInfo.UserName.IsOverLength(LoginUserInfo.UserNameMaxLength))
            {
                errorDetail = LoginUserInfo.MsgUserNameMaxLength;
                errorCode = ResultCode.UserAccountMgtUsernameExceedMaxLength;
                return false;
            }

            if (userInfo.Password.IsNullOrEmpty())
            {
                errorDetail = LoginUserInfo.MsgPasswordIsEmpty;
                errorCode = ResultCode.UserAccountMgtPasswordIsEmpty;
                return false;
            }

            if (userInfo.Password.IsOverLength(LoginUserInfo.PasswordMaxLength))
            {
                errorDetail = LoginUserInfo.MsgPasswordMaxLength;
                errorCode = ResultCode.UserAccountMgtPasswordExceedMaxLength;
                return false;
            }

            if (userInfo.Email.IsNullOrEmpty())
            {
                errorDetail = LoginUserInfo.MsgEmailIsEmpty;
                errorCode = ResultCode.UserAccountMgtEmailIsEmpty;
                return false;
            }

            if (userInfo.Email.IsOverLength(LoginUserInfo.EmailMaxLength))
            {
                errorDetail = LoginUserInfo.MsgEmailMaxLength;
                errorCode = ResultCode.UserAccountMgtEmailExceedMaxLength;
                return false;
            }

            if (!userInfo.Email.IsEmail())
            {
                errorDetail = LoginUserInfo.MsgEmailInvalid;
                errorCode = ResultCode.UserAccountMgtEmailInvalid;
                return false;
            }

            return true;
        }

        public static bool IsValid(this ResetPassword resetPwdInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;

            bool isSuccess = true;
            if (!resetPwdInfo.Email.IsNotNullOrEmpty())
            {
                errorDetail = LoginUserInfo.MsgEmailIsEmpty;
                errorCode = ResultCode.UserAccountMgtEmailIsEmpty;
                isSuccess = false;
            }
            else if (!resetPwdInfo.Email.IsEmail())
            {
                errorDetail = LoginUserInfo.MsgEmailInvalid;
                errorCode = ResultCode.UserAccountMgtEmailInvalid;
                isSuccess = false;
            }

            return isSuccess;
        }

        public static bool IsValid(this ChangePassword updatePwdInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;

            if (updatePwdInfo.NewPassword.IsNullOrEmpty())
            {
                errorDetail = LoginUserInfo.MsgPasswordIsEmpty;
                errorCode = ResultCode.UserAccountMgtPasswordIsEmpty;
                return false;
            }

            if (updatePwdInfo.NewPassword.IsOverLength(LoginUserInfo.PasswordMaxLength))
            {
                errorDetail = LoginUserInfo.MsgPasswordMaxLength;
                errorCode = ResultCode.UserAccountMgtPasswordExceedMaxLength;
                return false;
            }

            return true;
        }

        public static bool IsValid(this ProductInfo productInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }

        public static bool IsValid(this FileSignInfo fileSignInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }

        public static bool IsValid(this EmployeeSignInfo fileSignInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }

        public static bool IsValid(this EmployeeSignDetailInfo fileSignInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }

        public static bool IsValid(this DocumentSignInfo documentSignInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }

        public static bool IsValid(this ThreadedSignDocumentInfo fileSignInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }

        public static bool IsValid(this DocumentTypeInfo documentTypeInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }

        public static bool IsValid(this SignOfUserInfo signOfUserInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }


        public static bool IsValid(this EmployeeInfo employeerInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }
        
        public static bool IsValid(this CompanyInfo companyInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;

            //if (companyInfo.CompanyName.IsNullOrEmpty() && companyInfo.CompanyNameLocal.IsNullOrEmpty())
            //{
            //    errorDetail = CompanyManagementInfo.MsgCompanyNameCannotBeBlank;
            //    errorCode = ResultCode.CompanyNameBlank;
            //    return false;
            //}

            //if (companyInfo.Address.IsNullOrEmpty() && companyInfo.AddressLocal.IsNullOrEmpty())
            //{
            //    errorDetail = CompanyManagementInfo.MsgAddressCannotBeBlank;
            //    errorCode = ResultCode.CompanyAddressBlank;
            //    return false;
            //}

            //if (companyInfo.CompanyName.IsOverLength(CompanyManagementInfo.EnglishCompanyNameMaxLength))
            //{
            //    errorDetail = CompanyManagementInfo.MsgEnglishCompanyNameMaxLength;
            //    errorCode = ResultCode.CompanyEnglishCompanyNameMaxLength;
            //    return false;
            //}

            //if (companyInfo.CompanyNameLocal.IsOverLength(CompanyManagementInfo.LocalCompanyNameMaxLength))
            //{
            //    errorDetail = CompanyManagementInfo.MsgLocalCompanyNameMaxLength;
            //    errorCode = ResultCode.CompanyLocalCompanyNameMaxLength;
            //    return false;
            //}

            //if (companyInfo.Address.IsOverLength(CompanyManagementInfo.EnglishAddressMaxLength))
            //{
            //    errorDetail = CompanyManagementInfo.MsgEnglishAddressMaxLength;
            //    errorCode = ResultCode.CompanyEnglishAddressMaxLength;
            //    return false;
            //}

            //if (companyInfo.AddressLocal.IsOverLength(CompanyManagementInfo.LocalAddressMaxLength))
            //{
            //    errorDetail = CompanyManagementInfo.MsgLocalAddressMaxLength;
            //    errorCode = ResultCode.CompanyLocalAddressMaxLength;
            //    return false;
            //}

            //if (companyInfo.Tel1.IsOverLength(CompanyManagementInfo.PhoneNumberMaxLength))
            //{
            //    errorDetail = CompanyManagementInfo.MsgPhoneNumberMaxLength;
            //    errorCode = ResultCode.CompanySecondPhoneNumberMaxLength;
            //    return false;
            //}

            //if (companyInfo.Tel2.IsOverLength(CompanyManagementInfo.PhoneNumberMaxLength))
            //{
            //    errorDetail = CompanyManagementInfo.MsgPhoneNumberMaxLength;
            //    errorCode = ResultCode.CompanySecondPhoneNumberMaxLength;
            //    return false;
            //}

            //if (companyInfo.Fax.IsOverLength(CompanyManagementInfo.FaxNumberMaxLength))
            //{
            //    errorDetail = CompanyManagementInfo.MsgFaxNumberMaxLength;
            //    errorCode = ResultCode.CompanyFaxNumberMaxLength;
            //    return false;
            //}

            //if (companyInfo.HomePage.IsOverLength(CompanyManagementInfo.HomePageMaxLength))
            //{
            //    errorDetail = CompanyManagementInfo.MsgHomePageMaxLength;
            //    errorCode = ResultCode.CompanyHomepageMaxLength;
            //    return false;
            //}

            //if ((companyInfo.Tax < CompanyManagementInfo.MinTax) || (companyInfo.Tax > CompanyManagementInfo.MaxTax))
            //{
            //    errorDetail = CompanyManagementInfo.TaxNotInRange;
            //    errorCode = ResultCode.CompanyTaxInvalid;
            //    return false;
            //}

            return true;
        }

        

        public static bool IsValid(this MyCompanyInfo companyInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;

            return true;
        }


        public static bool IsValid(this CompanyAccount account, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }

        public static bool IsValid(this ContractInfo contractInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }

        public static bool IsValid(this CustomerInfo customerInfo, out ResultCode errorCode, out string errorDetail)
        {
            errorDetail = "";
            errorCode = ResultCode.NoError;
            return true;
        }
    }
}
