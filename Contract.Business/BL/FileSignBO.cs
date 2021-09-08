using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Business.BL
{
    public class FileSignBO : IFileSignBO
    {
        #region Fields, Properties

        private readonly IFileSignRepository fileSignRepository;
        #endregion

        #region Contructor

        public FileSignBO(IRepositoryFactory repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.fileSignRepository = repoFactory.GetRepository<IFileSignRepository>();
        }

        #endregion
       
        #region Methods
        public IEnumerable<FileSignInfo> Filter()
        {
            var fileSigns = this.fileSignRepository.Filter().ToList();
            return fileSigns.Select(p => new FileSignInfo(p));
        }
        public FileSignInfo GetDetail(int id)
        {
            var fileSign = this.fileSignRepository.GetDetail(id);
            return new FileSignInfo(fileSign);
        }

        public FileSignInfo Create(FileSignInfo fileSign)
        {
            if (fileSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!fileSign.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            FileSign fileSignNew = new FileSign();
            fileSignNew.CopyData(fileSign);
            fileSignNew.UserUpload = fileSign.UserCreate;
            fileSignNew.DateCreate = DateTime.Now;
            this.fileSignRepository.Insert(fileSignNew);
            return new FileSignInfo(fileSignNew);
        }

        public FileSignInfo Update(int id, FileSignInfo fileSign)
        {
            if (fileSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!fileSign.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            FileSign currentFileSign = GetFileSign(id);
            currentFileSign.CopyData(fileSign);
            this.fileSignRepository.Update(currentFileSign);
            return new FileSignInfo(currentFileSign);
        }

        public ResultCode Delete(int id)
        {
            FileSign currentFileSign = GetFileSign(id);
            bool result = this.fileSignRepository.Delete(currentFileSign);
            return result ? ResultCode.NoError : ResultCode.UnknownError;
        }
        #endregion

        private FileSign GetFileSign(int id)
        {
            FileSign currentFileSign = this.fileSignRepository.GetDetail(id);
            if (currentFileSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Get FileSign fail width id");
            }

            return currentFileSign;
        }
         
    }
}