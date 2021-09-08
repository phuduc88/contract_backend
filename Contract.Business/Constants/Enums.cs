using System.ComponentModel;
namespace Contract.Business.Constants
{

    public enum RecordStatus
     {
        Created =1,
        Approved,
        Cancelled,
    }

    public enum GroupType
    {
        /// <summary>
        /// Cá nhân
        /// </summary>
        Personal = 0,
        /// <summary>
        /// Tổ chức
        /// </summary>
        Organize,
   }

    public enum ContractStatus
    {
        /// <summary>
        /// Hợp đồng tạo mới
        /// </summary>
        New = 1,
        /// <summary>
        /// Đã duyệt
        /// </summary>
        Approved,
        /// <summary>
        /// Đã kích hoạt bảo hiểm xã hội
        /// </summary>
        Sended,
        /// <summary>
        /// Hợp đồng đã bị hủy
        /// </summary>
        Cancel 
    }

    public enum DocumentStatus
    {
        /// <summary>
        /// Hợp đồng tạo mới
        /// </summary>
        New = 1,
        /// <summary>
        /// Đã duyệt
        /// </summary>
        Approved,
        /// <summary>
        /// Đã kích hoạt bảo hiểm xã hội
        /// </summary>
        Sended,
        /// <summary>
        /// Hợp đồng đã bị hủy
        /// </summary>
        Cancel
    }

    public enum DocumentStep
    {
        /// <summary>
        /// Hợp đồng tạo mới
        /// </summary>
        New = 0,
        /// <summary>
        /// Bước 2
        /// </summary>
        AddCustomer = 1,
        /// <summary>
        /// Đã hoàn thành chưa gửi email
        /// </summary>
        Finish,
         
    }

    public enum EmployeeSearch_Type
    {
        All = 0,
        ContractCode = 1,
        IdentityCar,
        Code,
    }

    public enum ContractType
    {
        Customer = 1,
        Agencies,
        Client,
    }

    public enum StatuRegister
    {
        Create = 1,
        Adjust
    }
   

    public enum Email_Type
    {
        NoticeAdjustSubstitute = 1,
        NoticeAdjustUp,
        NoticeAdjustDown,
        NoticeAdjustInfomation,
        NoticeAccountSeller,
        NoticeAccountCustomer,
        SendVerificationCode,
    }
    public enum StatusSendEmail
    {
        New = 0,
        Successfull,
        Error,
    }

    public enum CacheDataType
    {
        SystemConfig,
        Category,
        Bank,
        DeclarationConfig,
    }
    public enum KEYENCRYPT
    {
        MBHXH
    }

    public enum ResultSubmitDelaration
    {
        Draft = 0,
        NotSend = 1,
        SendError,
        SendSuceesfull,
    }
}