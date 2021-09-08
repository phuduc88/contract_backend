using Contract.API.Business;
using Contract.API.MessageHandler;
using Hangfire;
using Microsoft.Owin;
using Owin;
using System.Web;
using Contract.Business.Cache;
using Contract.API.Config;

[assembly: OwinStartup(typeof(Contract.API.Startup))]

namespace Contract.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //InitHangFire(app);
            //InitRecuringJob();
            InitializeCache();
        }

        private void InitHangFire(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("SqlHangfire");
            var optionsServer = new BackgroundJobServerOptions
            {
                Queues = new[] { "default" },
                ServerName = "SV1",
                WorkerCount = 1
            };

            app.UseHangfireServer(optionsServer);

            var author = new MyAuthorizationFilter[]
            {
                 new MyAuthorizationFilter("admin"),
            };

            var options = new DashboardOptions { AppPath = VirtualPathUtility.ToAbsolute("~"), Authorization = author };
            app.UseHangfireDashboard("/hangfire", options);
        }

        private void InitRecuringJob()
        {
            ScheduleJobBusiness.Instance.ClearRecuringJob();
            ScheduleJobBusiness.Instance.RecuringExportInvoicePdf();
            
        }

        private static void InitializeCache()
        {
            CacheManagement.Instance.Initialize(SystemConfig.Instance.CacheResponseTimeout);
            CacheManagement.Instance.InitData();
        }

    }
}
