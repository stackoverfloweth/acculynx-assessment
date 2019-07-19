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
            //https://kevsoft.net/2016/02/24/automapper-and-autofac-revisited.html

            builder.Register(context => {
                var profiles = context.Resolve<IEnumerable<Profile>>();

                var config = new MapperConfiguration(x => {
                    foreach (var profile in profiles) {
                        x.AddProfile(profile);
                    }
                });

                return config;
            })
                .SingleInstance()
                .AutoActivate()
                .AsSelf();

            builder.Register(tempContext => {
                var ctx = tempContext.Resolve<IComponentContext>();
                var config = ctx.Resolve<MapperConfiguration>();

                return config.CreateMapper();
            }).As<IMapper>();
        }
    }
}