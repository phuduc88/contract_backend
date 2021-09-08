
using System.Collections.Generic;
namespace Contract.Business.Cache
{
    public interface IRedisCache<T>
    {        
        T Get(string key);
        void Set(string key, T data);
        void Remove(string key);
        void Clear();
        bool Contains(string key);

        IEnumerable<T> GetList(string key);
    }
}
