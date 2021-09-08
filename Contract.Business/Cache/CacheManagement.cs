using Contract.Data.DBAccessor;
using System;
using Contract.Common.Extensions;
using Contract.Business.BL;
using Contract.Business.DAO;
using Contract.Business.Models;
using Contract.Business.Constants;
using System.Linq;

namespace Contract.Business.Cache
{
    public class CacheManagement
    {
        private static CacheManagement _instance = new CacheManagement();
        private const int DefaultCacheItemTimeout = 120; // in minutes
        private TimeSpan _cacheItemTimeout = TimeSpan.FromMinutes(DefaultCacheItemTimeout);
        private IRepositoryFactory repoFactory;

        public static CacheManagement Instance
        {

            get { return _instance; }
        }

        public void Initialize(int timeout)
        {
            this._cacheItemTimeout = TimeSpan.FromMinutes(timeout);
        }
        private CacheManagement()
        {
        }

        public void InitData(string stringConnection = null)
        {
            this.repoFactory = GetRepoFactory(stringConnection);
            ResetCache();
        }

        public void ResetCache()
        {
            RemoveAllCache();
            InsertCache();
        }

        private void InsertCache()
        {
            AddSystemConfigCache();
            //AddCategorieCache();
            //AddDeclarationConfigCache();
        }

        private void RemoveAllCache()
        {
            CategoryCache.Instance.RemoveCacheCategory();

        }

        private IBOFactory GetBOFactory(string stringConnection)
        {
            if (stringConnection.IsNullOrEmpty())
            {
                return new BOFactory(DbContextManager.GetContext());
            }
            else
            {
                return new BOFactory(DbContextManager.GetContext(stringConnection));
            }
        }


        private IRepositoryFactory GetRepoFactory(string stringConnection)
        {
            if (stringConnection.IsNullOrEmpty())
            {
                return new RepositoryFactory(DbContextManager.GetContext());
            }
            else
            {
                return new RepositoryFactory(DbContextManager.GetContext(stringConnection));
            }
        }

        private void AddSystemConfigCache()
        {
            SystemConfig systemConfig = repoFactory.GetRepository<ISystemConfigRepository>().GetSystemConfig();
            CategoryCache.Instance.SetCacheCategory<SystemConfigInfo>(CacheDataType.SystemConfig.ToString(), new SystemConfigInfo(systemConfig), TimeSpan.FromDays(1));
        }

        //private void AddCategorieCache()
        //{
        //    var categories = repoFactory.GetRepository<ICategoryRepository>().Fillter(string.Empty).ToList();
        //    CacheCategoryInfo cacheCategory = new CacheCategoryInfo();
        //    cacheCategory.Categorie = categories.Select(p => new CategoryInfo(p)).ToList();
        //    CategoryCache.Instance.SetCacheCategory<CacheCategoryInfo>(CacheDataType.Category.ToString(), cacheCategory, TimeSpan.FromDays(1));
        //}

        //private void AddDeclarationConfigCache()
        //{
        //    var declarationsConfig = repoFactory.GetRepository<IDeclarationConfigRepository>().Fillter().ToList();
        //    CacheDeclarationConfig cacheDeclarationsConfig = new CacheDeclarationConfig();
        //    cacheDeclarationsConfig.DeclarationsConfig = declarationsConfig.Select(p => new DeclarationConfigInfo(p)).ToList();
        //    CategoryCache.Instance.SetCacheCategory<CacheDeclarationConfig>(CacheDataType.DeclarationConfig.ToString(), cacheDeclarationsConfig, TimeSpan.FromDays(1));
        //}

    }
}
