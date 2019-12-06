using Alexinea.Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Infra.Data.Repository.Contracts;
using ProgramAcad.Infra.Data.Workers;
using ProgramAcad.Services.Modules.Compiling;
using System;

namespace ProgramAcad.Infra.IoC
{
    public static class NativeInjector
    {
        public static ContainerBuilder RegisterDependencies(this ContainerBuilder builder)
        {

            builder
               .RegisterType<DomainNotificationManager>()
               .InstancePerLifetimeScope();

            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            RegisterRepository(builder);
            RegisterClients(builder);

            return builder;
        }

        private static void RegisterRepository(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(UsuarioRepository).Assembly)
               .Where(t => t.Namespace.EndsWith("Repositories"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }

        private static void RegisterClients(ContainerBuilder builder)
        {
            builder.RegisterType<CompilerApiClient>()
                .AsImplementedInterfaces();
        }
    }
}
