using Contract.Business.Models;
using Contract.Common;
using System.Collections.Generic;

namespace Contract.Business.BL
{
    public interface IProductBO
    {
        IEnumerable<ProductInfo> Filter();

        ProductInfo GetById(int id);

        ResultCode Create(ProductInfo productInfo);

        ResultCode Update(int id, ProductInfo productInfo);

        ResultCode Delete(int id);

    }
}
