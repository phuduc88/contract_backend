using Contract.Business.Cache;
using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using Contract.Common.Extensions;
using Contract.Data.DBAccessor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Contract.Business.BL
{
    public class MyCompanyBO : IMyCompanyBO
    {
        #region Fields, Properties

        private readonly IRepositoryFactory repoFactory;
        private readonly IMyCompanyRepository myCompanyRepository;
        private readonly ILoginUserRepository loginUserRepository;
        private readonly IUserRoleRepository roleRepository;
        private readonly IDbTransactionManager transaction;
        private readonly UpdateloadImageConfig uploadConfig;
        private readonly Permission permission;
        #endregion

        #region Contructor

        public MyCompanyBO(IRepositoryFactory repoFactory, UpdateloadImageConfig uploadImageConfig)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");

            this.repoFactory = repoFactory;
            this.myCompanyRepository = repoFactory.GetRepository<IMyCompanyRepository>();
            this.loginUserRepository = repoFactory.GetRepository<ILoginUserRepository>();
            this.roleRepository = repoFactory.GetRepository<IUserRoleRepository>();
            this.transaction = repoFactory.GetRepository<IDbTransactionManager>();
            this.uploadConfig = uploadImageConfig;
            this.permission = new Permission(repoFactory);
        }

        #endregion

        #region Methods

        public CompanyInfo GetCompanyInfo(int id, string userLevel)
        {
            var companyInfo = GetCompany(id);
            var adminOfCompany = GetAdminOfCompany(companyInfo.CompanySID, userLevel);
            var myCompanyInfo = new CompanyInfo(companyInfo, adminOfCompany);
            return myCompanyInfo;
        }

        public IEnumerable<MyCompany> GetList()
        {
            return this.myCompanyRepository.GetList();
        }

        #endregion

        public IEnumerable<MasterCompanyInfo> FilterCompany(ConditionSearchCompany condition, int skip = int.MinValue, int take = int.MaxValue)
        {
            if (condition == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            var companies = this.myCompanyRepository.FilterCompany(condition).AsQueryable()
                .OrderBy(condition.Order_By, condition.Order_Type.Equals(OrderType.Desc)).Skip(skip).Take(take).ToList();

            return companies.Select(p => new MasterCompanyInfo(p));
        }

        public int CountFillterCompany(ConditionSearchCompany condition)
        {
            if (condition == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            var listCompanies = this.myCompanyRepository.FilterCompany(condition).ToList();
            return listCompanies.Count();
        }

        public ResultCode Create(CompanyInfo companyInfo)
        {
            try
            {
                transaction.BeginTransaction();
                MyCompany currentCompany = CreateCompany(companyInfo);
                CreateUserOfCompany(companyInfo.Account, currentCompany);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return ResultCode.NoError;
        }

        private LoginUser CreateUserOfCompany(CompanyAccount accountInfo, MyCompany currentCompany)
        {
            var loginUser = CreateUser(accountInfo, currentCompany);
            var functionByRole = this.permission.FunctionByLevel(RoleInfo.SALE);
            this.permission.CreatePermission(loginUser, functionByRole);
            return loginUser;
        }

        public ResultCode Update(int companyId, CompanyInfo companyInfo)
        {
            try
            {
                transaction.BeginTransaction();
                MyCompany currentCompany = CompanyUpdate(companyId, companyInfo);
                UpdateUser(companyInfo.Account, currentCompany, companyInfo.Id);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return ResultCode.NoError;
        }

        public ResultCode UpdateToken(int id, int? companyId, TokenInfo tokenInfo)
        {
            return CompanyUpdateToken(id, companyId, tokenInfo);
        }

        public ResultCode Delete(int id, string userLevel)
        {
            try
            {
                transaction.BeginTransaction();
                DeleteCompany(id);
                DeleteAllUserOfCompany(id);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return ResultCode.NoError;
        }

        private void DeleteCompany(int id)
        {
            MyCompany currentCompany = GetCompany(id);
            currentCompany.Deleted = true;
            currentCompany.Active = false;
            myCompanyRepository.Update(currentCompany);
        }

        public MyCompanyInfo GetCompanyOfUser(int id, int? companyId)
        {
            var myCompany = GetCompany(id, companyId);
            MyCompanyInfo mycompanyInfo = new MyCompanyInfo(myCompany);
            mycompanyInfo.Logo = GetBase64StringImage(companyId.Value, mycompanyInfo.Logo);
            return mycompanyInfo;
        }

        public ResultCode UpdateMyCompany(int id, int? companyId, MyCompanyInfo companyInfo, string userLevel)
        {
            if (companyInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!companyInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }


            MyCompany currentCompny = GetCompany(id, companyId);

            if (!currentCompny.TaxCode.IsEquals(companyInfo.TaxCode)        
                 && this.myCompanyRepository.ContainTaxCode(companyInfo.TaxCode))  
            {
                throw new BusinessLogicException(ResultCode.CompanyNameIsExitsTaxCode,
                                    string.Format("Update Company failed because company with TaxCode = [{0}] is exist.", companyInfo.TaxCode));
            }

            if (companyInfo.Email.IsNotNullOrEmpty() && !currentCompny.Email.IsEquals(companyInfo.Email)        // Email is changed
               && this.myCompanyRepository.ContainEmail(companyInfo.Email)) // New Email is existed
            {
                throw new BusinessLogicException(ResultCode.CompanyNameIsExitsEmail,
                                  string.Format("Update Company failed because company with Email = [{0}] is exist.", companyInfo.Email));
            }

            currentCompny.CopyData(companyInfo);
            this.myCompanyRepository.Update(currentCompny);
            return ResultCode.NoError;
        }

        #region Private Method

        private MyCompany GetCompany(int id)
        {
            var company = this.myCompanyRepository.GetById(id);
            if (company == null)
            {
                throw new BusinessLogicException(ResultCode.NotFoundResourceId, MsgApiResponse.DataNotFound);
            }

            return company;
        }

        private MyCompany GetCompany(int id, int? companyId)
        {
            if (!companyId.HasValue)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            MyCompany company = this.myCompanyRepository.GetById(id);
            if (company == null)
            {
                throw new BusinessLogicException(ResultCode.NotFoundResourceId, MsgApiResponse.ResouceIdNotFound);
            }

            if (company.CompanySID != companyId.Value)
            {
                throw new BusinessLogicException(ResultCode.NotPermisionData, ClientManagementInfo.NotPermissionData);
            }

            return company;
        }

        /// <summary>
        /// Get the dealer admin of dealer company
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        private LoginUser GetAdminOfCompany(int companyId, string level)
        {

            var adminOfCompany = this.loginUserRepository.GetUsersByRoleName(level).FirstOrDefault(p => p.CompanySID == companyId);
            if (adminOfCompany == null)
            {
                return new LoginUser();
            }
            return adminOfCompany;
        }

        private MyCompany CreateCompany(CompanyInfo companyInfo)
        {
            if (companyInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!companyInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            bool isExitTaxCode = this.myCompanyRepository.ContainTaxCode(companyInfo.TaxCode);
            if (isExitTaxCode)
            {
                throw new BusinessLogicException(ResultCode.CompanyNameIsExitsTaxCode,
                                  string.Format("Create Company failed because company with TaxCode = [{0}] is exist.", companyInfo.TaxCode));
            }

            bool isExitEmail = this.myCompanyRepository.ContainEmail(companyInfo.Email);
            if (isExitEmail)
            {
                throw new BusinessLogicException(ResultCode.CompanyNameIsExitsEmail,
                                  string.Format("Create Company failed because company with Email = [{0}] is exist.", companyInfo.Email));
            }

            var myCompany = new MyCompany();
            myCompany.CopyData(companyInfo);
            myCompany.Level_Customer = CustomerLevel.Sellers;
            myCompany.Level_Agencies = 1;
            myCompany.Active = true;
            this.myCompanyRepository.Insert(myCompany);
            return myCompany;
        }

        private LoginUser CreateUser(CompanyAccount accoutInfo, MyCompany myCompany)
        {
            if (accoutInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!accoutInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            bool isExitUserId = this.loginUserRepository.ContainUserId(accoutInfo.UserId);
            if (isExitUserId)
            {
                throw new BusinessLogicException(ResultCode.UserAccountMgtConflictResourceUserId,
                                  string.Format("Create User failed because user with UserId = [{0}] is exist.", accoutInfo.UserId));
            }

            bool isExitEmail = this.loginUserRepository.ContainEmail(accoutInfo.Email);
            if (isExitEmail)
            {
                throw new BusinessLogicException(ResultCode.UserAccountMgtConflictResourceEmail,
                                  string.Format("Create User failed because user with Email = [{0}] is exist.", accoutInfo.Email));
            }

            UserRole userRole = this.roleRepository.FillterUserRoleByLevel(RoleInfo.SALE);
            LoginUser loginUser = new LoginUser();
            loginUser.UserID = accoutInfo.UserId;
            loginUser.Password = accoutInfo.Password;
            loginUser.UserRoleSID = userRole.UserRoleSID;
            loginUser.CompanySID = myCompany.CompanySID;
            loginUser.IsActive = accoutInfo.IsActive;
            loginUser.Email = accoutInfo.Email;
            loginUserRepository.Insert(loginUser);
            loginUser.UserRole = userRole;
            return loginUser;
        }

        private LoginUser UpdateUser(CompanyAccount accountInfo, MyCompany currentCompany, int? companyId)
        {
            if (accountInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            if (accountInfo.id == 0)
            {
                return CreateUser(accountInfo, currentCompany);
            }

            this.loginUserRepository.GetByEmail(accountInfo.Email);
            ResultCode errorCode;
            string errorMessage;
            if (!accountInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            LoginUser currentUser = this.loginUserRepository.GetByUserId(accountInfo.UserId, companyId.Value);
            if (!currentUser.Email.IsEquals(accountInfo.Email)        // Email is changed
               && this.loginUserRepository.ContainEmail(accountInfo.Email)) // New Email is existed
            {
                throw new BusinessLogicException(ResultCode.UserAccountMgtConflictResourceEmail,
                                string.Format("Update UserLogin with Email [{0}] exist", accountInfo.Email));
            }

            if (accountInfo.Password.IsNotNullOrEmpty())
            {
                currentUser.Password = accountInfo.Password;
            }

            currentUser.Email = accountInfo.Email;
            currentUser.IsActive = accountInfo.IsActive;
            currentUser.UpdatedDate = DateTime.Now;
            loginUserRepository.Update(currentUser);
            return currentUser;
        }

        private MyCompany CompanyUpdate(int id, CompanyInfo companyInfo)
        {
            if (companyInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!companyInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            MyCompany currentCompny = GetCompany(id);
            currentCompny.CopyData(companyInfo);
            this.myCompanyRepository.Update(currentCompny);
            //UserSessionCache.Instance.UpdateCompanyInfo(id, new CompanyInfo(currentCompny));
            return currentCompny;
        }

        private ResultCode CompanyUpdateToken(int id, int? companyId, TokenInfo tokenInfo)
        {
            if (tokenInfo == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            if (!companyId.HasValue || id != companyId.Value)
            {
                throw new BusinessLogicException(ResultCode.NotEnoughPermission, MsgApiResponse.HaveNotPermissionData);
            }


            ResultCode errorCode;
            string errorMessage;
            if (!tokenInfo.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            MyCompany currentCompny = GetCompany(id);
            currentCompny.CopyData(tokenInfo);
            this.myCompanyRepository.Update(currentCompny);
            return ResultCode.NoError;
        }

        private void DeleteAllUserOfCompany(int companyId)
        {
            var userOfCompany = this.loginUserRepository.GetByIdCompany(companyId).ToList();
            userOfCompany.ForEach(p =>
            {
                p.Deleted = true;
                this.loginUserRepository.Update(p);
            });
        }

        private string SaveImage(MyCompanyInfo companyInfo, FileTypeUpload fileType)
        {
            string imageData = GetImageData(companyInfo, fileType);
            if (imageData.IsNullOrEmpty()) return string.Empty;

            // Init file name, folder upload
            //var imageName = fileType + ".png";
            string imageName = string.Format("{0}_{1}.png", fileType, DateTime.Now.ToString("yyyyMMddHHmmss"));
            var folder = GetFolderUpload(companyInfo.Id);
            var filePath = string.Format("{0}\\{1}", folder, imageName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            // Validate and save image
            var bytes = Convert.FromBase64String(imageData);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                var sizeOfImage = ms.Length / 1024;
                image = Image.FromStream(ms);

                if (sizeOfImage > uploadConfig.MaxSizeImage)
                {
                    throw new BusinessLogicException(ResultCode.FileLarge, string.Format("Image {0} size too large", fileType.ToString()));
                }

                image.Save(filePath, ImageFormat.Png);
                image.Dispose();
            }

            return imageName;
        }

        private string GetImageData(MyCompanyInfo companyInfo, FileTypeUpload fileType)
        {
            string imageData;
            switch (fileType)
            {
                case FileTypeUpload.Logo:
                    imageData = companyInfo.Logo;
                    break;
                default:
                    imageData = companyInfo.Logo;
                    break;
            }
            return imageData;
        }

        private string GetBase64StringImage(int companyId, string fileName)
        {
            var folder = GetFolderUpload(companyId);
            var filePath = string.Format("{0}\\{1}", folder, fileName);

            if (!File.Exists(filePath)) return string.Empty;

            var bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }

        private string GetFolderUpload(int companyId)
        {
            return string.Format("{0}\\{1}", uploadConfig.RootFolderUpload, companyId);
        }

        #endregion

        public CompanyInfo GetCompanyInfo(int id)
        {
            MyCompany mycompany = this.myCompanyRepository.GetById(id);
            if (mycompany == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Get company fail");
            }

            return new CompanyInfo(mycompany);
        }

         
    }
}