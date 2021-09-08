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
    public class ThreadedSignDocumentBO : IThreadedSignDocumentBO
    {
        #region Fields, Properties

        private readonly IThreadedSignDocumentRepository threadedSignDocumentRepository;
        #endregion

        #region Contructor

        public ThreadedSignDocumentBO(IRepositoryFactory repoFactory)
        {
            Ensure.Argument.NotNull(repoFactory, "repoFactory");
            this.threadedSignDocumentRepository = repoFactory.GetRepository<IThreadedSignDocumentRepository>();
        }

        #endregion
       
        #region Methods
        public IEnumerable<ThreadedSignDocumentInfo> Filter()
        {
            var fileSigns = this.threadedSignDocumentRepository.Filter().ToList();
            return fileSigns.Select(p => new ThreadedSignDocumentInfo(p));
        }

        public ThreadedSignDocumentInfo GetDetail(int id)
        {
            var fileSign = this.threadedSignDocumentRepository.GetDetail(id);
            return new ThreadedSignDocumentInfo(fileSign);
        }

        public ThreadedSignDocumentInfo Create(ThreadedSignDocumentInfo threadedSignDocument)
        {
            if (threadedSignDocument == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }

            ResultCode errorCode;
            string errorMessage;
            if (!threadedSignDocument.IsValid(out errorCode, out errorMessage))
            {
                throw new BusinessLogicException(errorCode, errorMessage);
            }

            ThreadedSignDocument threadedSignDocumentNew = new ThreadedSignDocument();
            threadedSignDocumentNew.CopyData(threadedSignDocument);
            threadedSignDocumentNew.UserCreate = threadedSignDocument.UserActionId;
            threadedSignDocumentNew.DateCreate = DateTime.Now;
            bool result = this.threadedSignDocumentRepository.Insert(threadedSignDocumentNew);
            return new ThreadedSignDocumentInfo(threadedSignDocumentNew);

        }

        public ThreadedSignDocumentInfo Update(int id, ThreadedSignDocumentInfo fileSign)
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

            ThreadedSignDocument currentThreadedSignDocument = GetThreadedSignDocument(id);
            currentThreadedSignDocument.CopyData(fileSign);
            bool result = this.threadedSignDocumentRepository.Update(currentThreadedSignDocument);
            return new ThreadedSignDocumentInfo(currentThreadedSignDocument);
        }

        public ResultCode Delete(int id)
        {
            ThreadedSignDocument currentThreadedSignDocument = GetThreadedSignDocument(id);
            bool result = this.threadedSignDocumentRepository.Delete(currentThreadedSignDocument);
            return result ? ResultCode.NoError : ResultCode.UnknownError;
        }
        #endregion

        private ThreadedSignDocument GetThreadedSignDocument(int id)
        {
            ThreadedSignDocument currentThreadedSignDocument = this.threadedSignDocumentRepository.GetDetail(id);
            if (currentThreadedSignDocument == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, "Get ThreadedSignDocument fail width id");
            }

            return currentThreadedSignDocument;
        }
         
    }
}