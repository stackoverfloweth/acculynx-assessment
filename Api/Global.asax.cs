using System.Web.Http;
using System.Web.Mvc;
using Autofac.Integration.WebApi;

namespace Api {
    public class WebApiApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = GlobalConfiguration.Configuration;
            var iocContainerBuilder = new IocContainerBuilder();
            var iosContainer = iocContainerBuilder.BuildContainer();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(iosContainer);
        }
    }
}
