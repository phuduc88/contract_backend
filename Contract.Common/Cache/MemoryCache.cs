using System;
using System.Linq;
using System.Runtime.Caching;

namespace Contract.Common.Cache
{
    public class MemoryCache<T> : IMemoryCache<T>
    {
        #region Fields, Properties
        private readonly ObjectCache innerCache = MemoryCache.Default;
        private readonly CacheItemPolicy cachePolicy;
        private static object lockObject = new object();
        private static Logger logger = new Logger(LogTypes.Debug);

        private readonly string _regionName;
        public string RegionName
        {
            get { return this._regionName; }
        }

        private bool _enable;
        public bool Enabled
        {
            get { return this._enable; }
            set
            {
                lock (lockObject)
                {
                    // Clear all cached items if change from enabled(true) to disabled(false)
                    if (!value && this._enable)
                    {
                        Clear();
                    }
                    this._enable = value;
                }
            }
        } 
        #endregion

        #region Constructor, Methods
        public MemoryCache(string regionName, TimeSpan cacheTimeout, bool enabled = true)
        {
            this._regionName = regionName;
            this._enable = enabled;
            cachePolicy = new CacheItemPolicy()
            {
                SlidingExpiration = cacheTimeout,
                Priority = CacheItemPriority.Default,
                RemovedCallback = new CacheEntryRemovedCallback(RemovedCallback),
            };
        }

        public bool Contains(string key)
        {
            string cacheKey = GetCacheKey(key);
            lock (lockObject)
            {
                return this.innerCache.Contains(cacheKey);
            }
        }

        public T Get(string key)
        {
            lock (lockObject)
            {
                T result = default(T);
                string cacheKey = GetCacheKey(key);
                if (this.innerCache.Contains(cacheKey))
                {
                    try
                    {
                        result = (T)this.innerCache.Get(cacheKey);
                        WriteLog("get", cacheKey);
                    }
                    catch { }
                }

                return result;
            }
        }

        public void Set(string key, T data)
        {
            try
            {
                lock (lockObject)
                {
                    if (!this.Enabled) return;

                    string cacheKey = GetCacheKey(key);
                    if (this.innerCache.Contains(cacheKey))
                    {
                        this.innerCache.Remove(cacheKey);
                        WriteLog("remove", cacheKey);
                    }

                    this.innerCache.Set(cacheKey, data, this.cachePolicy);
                    WriteLog("add", cacheKey);
                }
            }
            catch { }
        }

        public void Remove(string key)
        {
            try
            {
                lock (lockObject)
                {
                    string cacheKey = GetCacheKey(key);
                    if (this.innerCache.Contains(cacheKey))
                    {
                        this.innerCache.Remove(cacheKey);
                        WriteLog("remove", cacheKey);
                    }
                }
            }
            catch { }
        }

        public void RemoveIfKeyStartsWith(string partOfKey)
        {
            try
            {
                lock (lockObject)
                {
                    string key = GetCacheKey(partOfKey);
                    var removeItems = this.innerCache.Where(kv => kv.Key.StartsWith(key));

                    foreach (var kvp in removeItems)
                    {
                        this.innerCache.Remove(kvp.Key);
                        WriteLog("remove", kvp.Key);
                    }
                }
            }
            catch { }
        }

        public void Clear()
        {
            try
            {
                lock (lockObject)
                {
                    var removeItems = this.innerCache.Where(kv => kv.Key.StartsWith(this.RegionName));
                    foreach (var kvp in removeItems)
                    {
                        this.innerCache.Remove(kvp.Key);
                        WriteLog("remove", kvp.Key);
                    }
                }
            }
            catch { }
        }

        private string GetCacheKey(string key)
        {
            return this.RegionName + ":" + key;
        }

        private static void RemovedCallback(CacheEntryRemovedArguments obj)
        {
            string reason = "remove";
            if (obj.RemovedReason == CacheEntryRemovedReason.Expired)
            {
                reason = "expired";
            }
            WriteLog(reason, obj.CacheItem.Key);
        }
 
        private static void WriteLog(string action, string key)
        {
            logger.Trace("[CACHED] {0} {1}", action.ToUpperInvariant(), key);
        }

        #endregion
    }
}
