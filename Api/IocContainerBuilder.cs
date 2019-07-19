using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Api.Contract;
using Api.Controllers;
using Api.Core;
using Autofac;
using Autofac.Integration.WebApi;

namespace Api {
    public class IocContainerBuilder {
        public IContainer BuildContainer() {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var assemblies = new[] {
                typeof(QuestionController).Assembly,    // Api
                typeof(QuestionFetcher).Assembly,       // Api.Core
                typeof(QuestionDto).Assembly,           // Api.Contract
            };
            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().InstancePerRequest();

            return builder.Build();
        }
    }
}