using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgramAcad.Domain.Configurations;

namespace ProgramAcad.API.Presentation.Helpers
{
    public static class AppSettingsBinder
    {
        public static IServiceCollection AddAppSettingsConfigs(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddOptions();
            services.AddApiPaths(configuration)
                .AddJDoodleConfigs(configuration);
            return services;
        }

        private static IServiceCollection AddApiPaths(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<ApiPaths>(configuration.GetSection("ApiPaths"));
            return services;
        }

        private static IServiceCollection AddJDoodleConfigs(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<JDoodleConfigs>(configuration.GetSection("JDoodleConfigs"));
            return services;
        }
    }
}
