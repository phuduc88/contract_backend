using Contract.Business.BL;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;

namespace Contract.API.Business
{
    public class FileSignBusiness : BaseBusiness
    {
        #region Fields, Properties

        private IFileSignBO fileSignBO;

        #endregion Fields, Properties

        #region Contructor

        public FileSignBusiness(IBOFactory boFactory)
        {
            this.fileSignBO = boFactory.GetBO<IFileSignBO>();
        }

        #endregion Contructor

        #region Methods

        public IEnumerable<FileSignInfo> Filter()
        {
            return this.fileSignBO.Filter();
        }

        public FileSignInfo GetDetail(int id)
        {
            return this.fileSignBO.GetDetail(id);
        }

        public ResultCode Create(DocumentSignInfo fileSign)
        {
            if (fileSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            return ResultCode.NoError;
            //return this.fileSignBO.Create(fileSign);
        }

        public ResultCode Update(int id, FileSignInfo fileSign)
        {
            if (fileSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            this.fileSignBO.Update(id, fileSign);
            return ResultCode.NoError;
        }


        public ResultCode Delete(int id)
        {
            return this.fileSignBO.Delete(id);
        }

        #endregion
    }
}