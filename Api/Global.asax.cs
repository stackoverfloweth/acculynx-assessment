using System;
using System.Web.Http;
using System.Web.Mvc;

namespace Api {
    public class WebApiApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var iocContainerBuilder = new IocContainerBuilder();
            iocContainerBuilder.BuildContainer();
        }
        protected void Application_Error(object sender, EventArgs e) {
            var exception = Server.GetLastError();
        }
    }
}
