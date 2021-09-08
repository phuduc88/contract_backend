using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis.Extensions.Core;

namespace Contract.Business.Cache
{
    public static class ICacheClientExtensions
    {
        /// <summary>
        /// Update Time-to-Live of a cache item by its key
        /// </summary>
        /// <param name="key">The keys</param>
        public static void UpdateTtl(this ICacheClient cacheClient, IEnumerable<string> keys, TimeSpan slidingExpiration)
        {
            if (keys == null || keys.Count() == 0)
            {
                return;
            }

            var transaction = cacheClient.Database.CreateTransaction();
            foreach (string key in keys)
            {
                cacheClient.Database.KeyExpire(key, slidingExpiration);                
            }
            transaction.Execute();
        }

        /// <summary>
        /// Update Time-to-Live of a cache item by its key
        /// </summary>
        /// <param name="key">The key</param>
        public static void UpdateTtl(this ICacheClient cacheClient, string key, TimeSpan slidingExpiration)
        {
            var keys = new List<string>() { key };
            UpdateTtl(cacheClient, keys, slidingExpiration);
        }
    }
}
