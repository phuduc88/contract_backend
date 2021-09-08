using StackExchange.Redis.Extensions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Contract.Common;

namespace Contract.Business.Cache
{
    public class RedisCache<T> : IRedisCache<T>
    {
        private readonly ICacheClient innerCache;
        public string CacheNamespace { get; set; }
        public TimeSpan ExpireTimeout { get; set; }

        #region Constructor, Methods
        public RedisCache(string cacheNamespace, TimeSpan expireTimeout)
        {
            this.CacheNamespace = cacheNamespace;
            this.ExpireTimeout = expireTimeout;
            ISerializer serializer = new StackExchange.Redis.Extensions.Newtonsoft.NewtonsoftSerializer();
            innerCache = new StackExchangeRedisCacheClient(serializer);
        }

        public T Get(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, T data)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            this.innerCache.Remove(GetCacheKey(key));
        }

        public void Clear()
        {
            string keyAll = "*";
            this.innerCache.RemoveAll(this.innerCache.SearchKeys(GetCacheKey(keyAll)));
        }

        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

       public IEnumerable<T> GetList(string key)
        {
            var cacheEntities = this.innerCache.GetAll<T>(this.innerCache.SearchKeys(GetCacheKey("*")));
            if (cacheEntities == null || cacheEntities.Count == 0)
            {
                return new List<T>();
            }
            List<T> items = new List<T>();
            foreach (var kvp in cacheEntities)
            {
                if (kvp.Value == null) continue;
                T cacheEntity = kvp.Value;
                items.Add(cacheEntity);
                //if (Utility.Equals(cacheEntity.UserName, userName))
                //{
                //    sessions.Add(cacheEntity);
                //}
            }
            return items;
        }


       private string GetCacheKey(string key)
       {
           return CacheNamespace + ":" + key;
       }

        #endregion
    }
}
