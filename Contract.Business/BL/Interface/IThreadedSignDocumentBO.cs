using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface IThreadedSignDocumentBO
    {
        IEnumerable<ThreadedSignDocumentInfo> Filter();

        ThreadedSignDocumentInfo GetDetail(int id);

        ThreadedSignDocumentInfo Create(ThreadedSignDocumentInfo threadedSignDocument);

        ThreadedSignDocumentInfo Update(int id, ThreadedSignDocumentInfo threadedSignDocument);

        ResultCode Delete(int id);

    }
}
