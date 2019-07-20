using Api.Contract;
using Api.Controllers;
using Api.Core;
using Api.Core.AutoMapper;
using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using Data;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;

namespace Api {
    public class IocContainerBuilder {
        public IContainer BuildContainer() {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var assemblies = new[] {
                typeof(QuestionController).Assembly,                // Api
                typeof(FilteredLatestQuestionsFetcher).Assembly,    // Api.Core
                typeof(QuestionDto).Assembly,                       // Api.Contract
                typeof(StackOverflowethContext).Assembly,           // Data
            };
            builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces().InstancePerRequest();

            RegisterAutoMapper(builder);

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }

        private static void RegisterAutoMapper(ContainerBuilder builder) {
            // https://github.com/AutoMapper/AutoMapper/issues/1109#issuecomment-189875202
            builder
                .RegisterAssemblyTypes(typeof(AttemptMapping).Assembly)
                .As<Profile>()
                .InstancePerRequest();

            builder.Register(context => new MapperConfiguration(cfg => {
                cfg.ConstructServicesUsing(context.Resolve);
                foreach (var profile in context.Resolve<IEnumerable<Profile>>()) {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().InstancePerRequest();

            builder.Register(c => {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerRequest();
        }
    }
}