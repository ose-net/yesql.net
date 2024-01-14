using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YeSql.Net;

namespace AspNetFramework
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static string SqlStatement {  get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ISqlCollection sqlStatements = new YeSqlLoader().LoadFromDirectories("./sql");
            SqlStatement = sqlStatements["GetProducts"];
        }
    }
}
