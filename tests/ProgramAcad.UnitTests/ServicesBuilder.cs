using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProgramAcad.Common.Models;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Infra.Data.Workers;
using ProgramAcad.Infra.IoC;
using System;
using System.Threading.Tasks;

namespace ProgramAcad.UnitTests
{
    public static class ServicesBuilder
    {
        public static IServiceProvider GetServices()
        {
            var services = new ServiceCollection();
            var builder = new ContainerBuilder();

            services.AddDbContext<ProgramAcadDataContext>(options =>
            {
                //var dbName = Guid.NewGuid().ToString();
                options.UseInMemoryDatabase("db");
            }, ServiceLifetime.Singleton);

            builder.RegisterDependencies();

            builder.Populate(services);
            var provider = new AutofacServiceProvider(builder.Build());
            EnsureSeedData<ProgramAcadDataContext>(provider).Wait();
            return provider;
        }


        private static async Task EnsureSeedData<TContext>(IServiceProvider services)
             where TContext : DbContext
        {
            using var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TContext>();
            await SeedEnumeration<TContext, LinguagemProgramacao>(context);
            await SeedEnumeration<TContext, NivelDificuldade>(context);
            await SeedInstrutorAdministrador(context);
        }

        private static async Task SeedInstrutorAdministrador<TContext>(TContext context)
            where TContext : DbContext
        {
            var usuarioSet = context.Set<Usuario>();
            if (!await usuarioSet.AnyAsync(x => x.Nickname == "admin"))
            {
                var admin = new Usuario("admin", "admin@admin.com", false, "INSTRUTOR");
                usuarioSet.Add(admin);
            }
        }

        private static async Task SeedEnumeration<TContext, TEnumeration>(TContext context)
            where TContext : DbContext
            where TEnumeration : Enumeration
        {
            var linguagensSet = context.Set<TEnumeration>();
            if (!await linguagensSet.AnyAsync())
            {
                var linguagens = Enumeration.GetAll<TEnumeration>();
                await linguagensSet.AddRangeAsync(linguagens);
                await context.SaveChangesAsync();
            }
        }
    }
}
