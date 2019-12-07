using Autofac;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Infra.Data.Repository.Contracts;
using ProgramAcad.Infra.Data.Workers;
using ProgramAcad.Services.Modules.Algoritmos.Commands;
using ProgramAcad.Services.Modules.Algoritmos.Services;
using ProgramAcad.Services.Modules.Compiling;

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
            RegisterCommands(builder);
            RegisterAppServices(builder);

            return builder;
        }

        private static void RegisterRepository(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(UsuarioRepository).Assembly)
               .Where(t => t.Namespace.EndsWith("Contracts"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }

        private static void RegisterCommands(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CriarAlgoritmoCommand).Assembly)
               .Where(t => t.Namespace.EndsWith("Commands"))
               .InstancePerLifetimeScope();
        }

        private static void RegisterAppServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AlgoritmoAppService).Assembly)
               .Where(t => t.Namespace.EndsWith("Services"))
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
