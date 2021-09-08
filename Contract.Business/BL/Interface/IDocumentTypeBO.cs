using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface IDocumentTypeBO
    {
        IEnumerable<DocumentTypeInfo> Filter();

        DocumentTypeInfo GetDetail(int id);

        ResultCode Create(DocumentTypeInfo documentType);

        ResultCode Update(int id, DocumentTypeInfo documentType);

        ResultCode Delete(int id);

    }
}
