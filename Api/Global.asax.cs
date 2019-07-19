using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Api {
    public class WebApiApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.EnsureInitialized();

            var iocContainerBuilder = new IocContainerBuilder();
            iocContainerBuilder.BuildContainer();
        }
    }
}
