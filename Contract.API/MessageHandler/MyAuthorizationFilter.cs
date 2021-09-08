using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contract.API.MessageHandler
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private readonly string[] _roles;

        public MyAuthorizationFilter(params string[] roles)
        {
            _roles = roles;
        }

        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}