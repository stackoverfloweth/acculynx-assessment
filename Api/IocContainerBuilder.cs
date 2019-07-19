using Api.Contract;
using Api.Controllers;
using Api.Core;
using Api.Core.AutoMapper;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

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
            
            var container = builder.Build();

            RegisterAutoMapper(builder, container);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }

        private static void RegisterAutoMapper(ContainerBuilder builder, IContainer container)
        {
            builder
                .RegisterAssemblyTypes(typeof(QuestionMapping).Assembly)
                .As<Profile>()
                .InstancePerRequest();

            builder.Register(context => new MapperConfiguration(config =>
            {
                config.ConstructServicesUsing(container.Resolve);
                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                {
                    config.AddProfile(profile);
                }
            })).AsSelf().InstancePerRequest();

            builder.Register(componentContext =>
            {
                var context = componentContext.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerRequest();
        }
    }
}