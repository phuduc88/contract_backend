using Contract.Business.Constants;
using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Common;
using Contract.Data.DBAccessor;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Business.BL
{
    public class DocumentTypeBO : IDocumentTypeBO
    {
        #region Fields, Properties

        private readonly IDocumentTypeRepository documentTypeRepository;
        #endregion

        #region Contructor

        public DocumentTypeBO(IRepositoryFactory repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.documentTypeRepository = repoFactory.GetRepository<IDocumentTypeRepository>();
        }

        #endregion
       
        #region Methods
        public IEnumerable<DocumentTypeInfo> Filter()
        {
            var documentTypies = this.documentTypeRepository.Filter().ToList();
            return documentTypies.Select(p => new DocumentTypeInfo(p));
        }
        public DocumentTypeInfo GetDetail(int id)
        {
            var fileSign = this.documentTypeRepository.GetDetail(id);
            return new DocumentTypeInfo(fileSign);
        }

        public ResultCode Create(DocumentTypeInfo documentType)
        {
            if (documentType == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!documentType.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            DocumentType documentTypeNew = new DocumentType();
            documentTypeNew.CopyData(documentType);
            bool result = this.documentTypeRepository.Insert(documentTypeNew);
            return result ? ResultCode.NoError : ResultCode.UnknownError;

        }

        public ResultCode Update(int id, DocumentTypeInfo documentType)
        {
            if (documentType == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!documentType.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            DocumentType currentDocumentType = GetDocumentType(id);
            currentDocumentType.CopyData(documentType);
            bool result = this.documentTypeRepository.Update(currentDocumentType);
            return result ? ResultCode.NoError : ResultCode.UnknownError;
        }

        public ResultCode Delete(int id)
        {
            DocumentType currentFileSign = GetDocumentType(id);
            bool result = this.documentTypeRepository.Delete(currentFileSign);
            return result ? ResultCode.NoError : ResultCode.UnknownError;
        }
        #endregion

        private DocumentType GetDocumentType(int id)
        {
            DocumentType currentFileSign = this.documentTypeRepository.GetDetail(id);
            if (currentFileSign == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Get FileSign fail width id");
            }

            return currentFileSign;
        }
         
    }
}