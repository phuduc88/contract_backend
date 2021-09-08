using Contract.Business.Models;
using Contract.Common;
using Contract.Common.Extensions;
using StackExchange.Redis.Extensions.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;

namespace Contract.Business.Cache
{
    public class UserSessionCache
    {
        private static Logger logger = new Logger();
        private static UserSessionCache instance = new UserSessionCache();
        public static UserSessionCache Instance
        {
            get { return instance; }
        }

        private readonly ICacheClient innerCache;

        private string cacheNamespace = "Contract:cache";

        public void Initialize(string cacheNamespace)
        {
            if (!string.IsNullOrWhiteSpace(cacheNamespace))
            {
                this.cacheNamespace = cacheNamespace;
            }
        }


        private UserSessionCache()
        {
            ISerializer serializer = new StackExchange.Redis.Extensions.Newtonsoft.NewtonsoftSerializer();
            innerCache = new StackExchangeRedisCacheClient(serializer);
        }

        /// <summary>
        /// Get UserSessionInfo of a login user by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UserSessionInfo GetUserSession(string token)
         {
            string cacheKey = GetCacheKey(token);
            UserSessionInfo cacheEntity = this.innerCache.Get<UserSessionInfo>(cacheKey);
            if (cacheEntity != null)
            {
                this.innerCache.UpdateTtl(cacheKey, TimeSpan.FromMinutes(15));
                return cacheEntity;
            }

            return null;
        }

        private string GetCacheKey(string key)
        {
            return cacheNamespace + ":" + key;
        }
        /// <summary>
        /// Save user session information
        /// </summary>
        /// <param name="userSessionInfo"></param>
        /// <param name="expireTimeout"></param>
        /// <returns></returns>
        public void SaveUserSession(UserSessionInfo userSessionInfo, TimeSpan expireTimeout)
        {
            if (userSessionInfo == null)
            {
                return;
            }

            userSessionInfo.SlidingExpiration = expireTimeout;
            string cacheKey = GetCacheKey(userSessionInfo.Token);
            this.innerCache.Add<UserSessionInfo>(cacheKey, userSessionInfo, expireTimeout);
            logger.Trace("[SESSION] LOGIN An user logged-in to system. SessionId: [{0}]; UserId: [{1}].",
                            userSessionInfo.SessionId, userSessionInfo.Id);
        }

        /// <summary>
        /// Remove session of a login user by token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UserSessionInfo RemoveUserSession(string token)
        {
            string cacheKey = GetCacheKey(token);
            UserSessionInfo cacheEntity = this.innerCache.Get<UserSessionInfo>(cacheKey);
            this.innerCache.Remove(cacheKey);

            logger.Trace("[SESSION] LOGOUT An user logged-out to system. Token: [{0}]; UserId: [{1}].",
                            cacheEntity.Token, cacheEntity.Id);
            return cacheEntity == null ? null : cacheEntity;
        }

        public IEnumerable<UserSessionInfo> FindUserSessions(string userName)
        {
            var cacheEntities = this.innerCache.GetAll<UserSessionInfo>(this.innerCache.SearchKeys(GetCacheKey("*")));
            if (cacheEntities == null || cacheEntities.Count == 0)
            {
                return new List<UserSessionInfo>();
            }

            List<UserSessionInfo> sessions = new List<UserSessionInfo>();
            foreach (var kvp in cacheEntities)
            {
                if (kvp.Value == null) continue;
                UserSessionInfo cacheEntity = kvp.Value;
                if (Utility.Equals(cacheEntity.UserName, userName))
                {
                    sessions.Add(cacheEntity);
                }
            }
            return sessions;
        }

         

        public void RemoveProduct(string companyId)
        {
            string keyOfComapny = "product*";
            if (companyId.IsNotNullOrEmpty()) {
                keyOfComapny = string.Format("product{0}_*", companyId);
            }
            this.innerCache.RemoveAll(this.innerCache.SearchKeys(GetCacheKey(keyOfComapny)));
        }

        public void RemoveCurentProduct(int companyId, int productId)
        {
            string keyOfComapny = string.Format("product{0}_{1}", companyId, productId);
            this.innerCache.Remove(GetCacheKey(keyOfComapny));
        }


        /// <summary>
        /// A user has been deleted.
        /// </summary>
        /// <param name="userId">The condition get session</param>
        public void RemoveSessionByUserId(int userId)
        {
            var sessionInfos = UserSessionCache.Instance.FindUserSessionsByUserId(userId).ToList();
            if (sessionInfos == null || sessionInfos.Count() == 0)
            {
                return;
            }

            sessionInfos.ForEach(p =>
                 {
                     UserSessionCache.Instance.RemoveUserSession(p.Token);
                     logger.Trace("[LOGOUT] An logged-in user has just deleted from system. SessionId: [{0}]; UserId: [{1}].", p.SessionId, p.UserName);
                 });
        }

        public IEnumerable<UserSessionInfo> FindUserSessionsByUserId(int userId)
        {
            var cacheEntities = this.innerCache.GetAll<UserSessionInfo>(this.innerCache.SearchKeys(GetCacheKey("*")));
            if (cacheEntities == null || cacheEntities.Count == 0)
            {
                return new List<UserSessionInfo>();
            }

            List<UserSessionInfo> sessions = new List<UserSessionInfo>();
            foreach (var kvp in cacheEntities)
            {
                if (kvp.Value == null) continue;
                UserSessionInfo cacheEntity = kvp.Value;
                if (Utility.Equals(cacheEntity.UserId, userId))
                {
                    sessions.Add(cacheEntity);
                }
            }
            return sessions;
        }

        
        /// <summary>
        /// Callback method which is called when a login session is removed
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="reason"></param>
        private void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            var sessionInfo = value as UserSessionInfo;
            switch (reason)
            {
                case CacheItemRemovedReason.Expired:
                    {
                        logger.Trace("[SESSION] EXPIRED An user session is timeout. SessionId: [{0}]; UserId: [{1}].",
                                    sessionInfo.SessionId, sessionInfo.UserName);
                        break;
                    }
                case CacheItemRemovedReason.Removed:
                    {
                        logger.Trace("[SESSION] LOGOUT An user session is logged out.  SessionId: [{0}]; UserId: [{1}].",
                                    sessionInfo.SessionId, sessionInfo.UserName);
                        break;
                    }
                default:
                    {
                        logger.Trace("[SESSION] END An user session is end.  SessionId: [{0}]; UserId: [{1}]; Reason: [{2}].",
                                    sessionInfo.SessionId, sessionInfo.UserName, reason);
                        break;
                    }
            }
        }
    }
}
