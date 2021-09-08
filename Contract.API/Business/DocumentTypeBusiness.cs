using Contract.Business.BL;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;

namespace Contract.API.Business
{
    public class DocumentTypeBusiness : BaseBusiness
    {
        #region Fields, Properties

        private IDocumentTypeBO documentTypeBO;

        #endregion Fields, Properties

        #region Contructor

        public DocumentTypeBusiness(IBOFactory boFactory)
        {
            this.documentTypeBO = boFactory.GetBO<IDocumentTypeBO>();
        }

        #endregion Contructor

        #region Methods

        public IEnumerable<DocumentTypeInfo> Filter()
        {
            return this.documentTypeBO.Filter();
        }

        public DocumentTypeInfo GetDetail(int id)
        {
            return this.documentTypeBO.GetDetail(id);
        }

        public ResultCode Create(DocumentTypeInfo documentType)
        {
            if (documentType == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            return this.documentTypeBO.Create(documentType);
        }

        public ResultCode Update(int id, DocumentTypeInfo documentType)
        {
            if (documentType == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            return this.documentTypeBO.Update(id, documentType);
        }


        public ResultCode Delete(int id)
        {
            return this.documentTypeBO.Delete(id);
        }

        #endregion
    }
}