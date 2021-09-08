using Contract.Common;
using StackExchange.Redis.Extensions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Business.Cache
{
   public class CategoryCache
    {
        private static Logger logger = new Logger();
        private static CategoryCache instance = new CategoryCache();
        private string cacheNamespace = "Contract:category";
        public static CategoryCache Instance
        {
            get { return instance; }
        }

        private readonly ICacheClient innerCache;

        private CategoryCache()
        {
            ISerializer serializer = new StackExchange.Redis.Extensions.Newtonsoft.NewtonsoftSerializer();
            innerCache = new StackExchangeRedisCacheClient(serializer);
        }

        private string GetCacheKey(string key)
        {
            return cacheNamespace + ":" + key;
        }

        public void SetCacheCategory<T>(string categoryName, T t, TimeSpan expireTimeout) 
            where T: class
        {
            string cacheKey = GetCacheKey(categoryName);
            this.innerCache.Add<T>(cacheKey, t, expireTimeout);
        }

        public T GetCacheCategory<T>(string categoryName)
            where T: class
        {
            string cacheKey = GetCacheKey(categoryName);
            T t = this.innerCache.Get<T>(cacheKey);
            if(t != null) {
                this.innerCache.UpdateTtl(cacheKey, TimeSpan.FromDays(1));
                return t;
            }
            return null;
        }

        public void RemoveCacheCategory()
        {
           string keyAll = "*";
           var keys = this.innerCache.SearchKeys(GetCacheKey(keyAll));
           this.innerCache.RemoveAll(keys);
        }

    }
}
