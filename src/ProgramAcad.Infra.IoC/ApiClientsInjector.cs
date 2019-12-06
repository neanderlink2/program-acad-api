using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgramAcad.Services.Modules.Compiling.RefitInterfaces;
using Refit;
using System;

namespace ProgramAcad.Infra.IoC
{
    public static class ApiClientsInjector
    {
        public static IServiceCollection AddApiClients(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddJDoodleCompiler(configuration);

            return services;
        }

        private static IServiceCollection AddJDoodleCompiler(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var urlBase = configuration.GetSection("ApiPaths").GetValue<string>("JDoodleBase");
            services.AddRefitClient<ICompilerApiCall>()
                       .ConfigureHttpClient(c =>
                       {
                           c.BaseAddress = new Uri(urlBase);
                           c.Timeout = TimeSpan.FromMinutes(5);
                           //c.DefaultRequestHeaders.Remove("charset");
                       });

            return services;
        }
    }
}
