using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Common.Utils
{
    public class ListError<T> : List<T> 
    {
        public new void Add(T item)
        {
            if (item != null)
            {
                base.Add(item);
            }
        }
        public new void AddRange(IEnumerable<T> listItem)
        {
            if (listItem != null)
            {
                base.AddRange(listItem);
            }
        }

    }
}
