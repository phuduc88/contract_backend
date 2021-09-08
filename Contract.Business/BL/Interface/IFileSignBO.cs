using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface IFileSignBO
    {
        IEnumerable<FileSignInfo> Filter();

        FileSignInfo GetDetail(int id);

        FileSignInfo Create(FileSignInfo fileSign);

        FileSignInfo Update(int id, FileSignInfo fileSign);

        ResultCode Delete(int id);

    }
}
