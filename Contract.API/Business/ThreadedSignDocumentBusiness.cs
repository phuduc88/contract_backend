using Contract.Business.BL;
using Contract.Business.Constants;
using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;

namespace Contract.API.Business
{
    public class ThreadedSignDocumentBusiness : BaseBusiness
    {
        #region Fields, Properties

        private IThreadedSignDocumentBO threadedSignDocumentBO;

        #endregion Fields, Properties

        #region Contructor

        public ThreadedSignDocumentBusiness(IBOFactory boFactory)
        {
            this.threadedSignDocumentBO = boFactory.GetBO<IThreadedSignDocumentBO>();
        }

        #endregion Contructor

        #region Methods

        public IEnumerable<ThreadedSignDocumentInfo> Filter()
        {
            return this.threadedSignDocumentBO.Filter();
        }

        public ThreadedSignDocumentInfo GetDetail(int id)
        {
            return this.threadedSignDocumentBO.GetDetail(id);
        }

        public ResultCode Create(ThreadedSignDocumentInfo threadedSignDocument)
        {
            if (threadedSignDocument == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            this.threadedSignDocumentBO.Create(threadedSignDocument);
            return ResultCode.NoError;
        }

        public ResultCode Update(int id, ThreadedSignDocumentInfo threadedSignDocument)
        {
            if (threadedSignDocument == null)
            {
                throw new BusinessLogicException(ResultCode.DataInvalid, MsgApiResponse.DataInvalid);
            }
            this.threadedSignDocumentBO.Update(id, threadedSignDocument);
            return ResultCode.NoError;
        }


        public ResultCode Delete(int id)
        {
            return this.threadedSignDocumentBO.Delete(id);
        }

        #endregion
    }
}