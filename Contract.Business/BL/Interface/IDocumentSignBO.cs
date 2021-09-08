using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface IDocumentSignBO
    {
        IEnumerable<DocumentSignInfo> Filter(ConditionSearchDocument condition,int skip, int take);
        int CountFilter(ConditionSearchDocument condition);
        DocumentSignInfo GetDetail(int id);

        DocumentSignInfo Create(DocumentSignInfo fileSign);

        DocumentSignInfo Update(int id, DocumentSignInfo fileSign);

        DocumentSignInfo UpdateStep(int id, DocumentSignInfo documentSign);

        FileExport SignDocument(SignDocumentInfo signDocument);

        ResultCode Delete(int id);

    }
}
