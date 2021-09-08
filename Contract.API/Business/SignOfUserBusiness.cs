using Contract.Business;
using Contract.Business.BL;
using Contract.Business.Config;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;
using System.IO;
using Contract.Common.Extensions;

namespace Contract.API.Business
{
    public class SignOfUserBusiness : BaseBusiness
    {
        #region Fields, Properties

        private ISignOfUserBO signOfuser;
        private PrintConfig printConfig;

        #endregion Fields, Properties

        #region Contructor

        public SignOfUserBusiness(IBOFactory boFactory)
        {
            this.signOfuser = boFactory.GetBO<ISignOfUserBO>();
            printConfig = GetPrintConfig();
        }

        #endregion Contructor

        #region Methods

        public IEnumerable<SignOfUserInfo> Filter()
        {
            var signOfUsers = this.signOfuser.Filter();
            var result = new List<SignOfUserInfo>();
            string folderSign = CreateFolderRootOfUser();
            foreach (var item in signOfUsers)
            {
                string fullPathFile = Path.Combine(folderSign, item.FileName);
                item.Data = FileProcess.GetBase64StringFile(fullPathFile);
                result.Add(item);
            }
            return result;
        }

        public SignOfUserInfo GetDetail(int id)
        {
            return this.signOfuser.GetDetail(id);
        }

        public ResultCode Create(SignOfUserInfo signOfUser)
        {
            if (signOfUser == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            string folderSign = CreateFolderRootOfUser();
            signOfUser.FileName = FileProcess.SaveFileBase64ToFile(signOfUser.Data, folderSign, signOfUser.Extension);
            signOfUser.UserId = this.CurrentUser.Id;
            signOfUser.CompanyId = GetCompanyIdOfUser();
            //string fileName = fileProd
            return this.signOfuser.Create(signOfUser);
        }

        public ResultCode Update(int id, SignOfUserInfo signOfUser)
        {
            if (signOfUser == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            if (signOfUser.Data.IsNotNullOrEmpty())
            {
                string folderSign = CreateFolderRootOfUser();
                signOfUser.FileName = FileProcess.SaveFileBase64ToFile(signOfUser.Data, folderSign, signOfUser.Extension);
            }

            signOfUser.UserId = this.CurrentUser.Id;
            signOfUser.CompanyId = GetCompanyIdOfUser();
            return this.signOfuser.Update(id, signOfUser);
        }

        public ResultCode UpdateUseDefault(int id)
        {
            int companyId = GetCompanyIdOfUser();
            return this.signOfuser.UpdateUseDefault(id, companyId);
        }


        public ResultCode Delete(int id)
        {
            return this.signOfuser.Delete(id);
        }

        private string CreateFolderRootOfUser()
        {
            string pathFileAssetOfCompany = printConfig.FullFolderAssetOfCompany;
            string folderContain = Path.Combine(pathFileAssetOfCompany, this.CurrentUser.Id.ToString());
            if (!Directory.Exists(folderContain))
            {
                Directory.CreateDirectory(folderContain);
            }

            return folderContain;
        }

        

        #endregion
    }
}