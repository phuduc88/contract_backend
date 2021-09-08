using Contract.API.Config;
using Contract.Business.Cache;
using Contract.Common;
using Contract.Common.Cache;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Contract.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static Logger logger = new Logger();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            logger.Trace("API server started");
        }

        protected void Application_End()
        {
            logger.Trace("API server stopped");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (SystemConfig.Instance.EnableCORS)
            {
                if (Request.Headers.AllKeys.Contains("Origin", StringComparer.OrdinalIgnoreCase)
                    && Utility.Equals(Request.HttpMethod, "OPTIONS"))
                {
                    HttpContext.Current.Response.End();
                }
            }
        }

    }
}
