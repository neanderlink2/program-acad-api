using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ProgramAcad.API.Presentation.Helpers;
using ProgramAcad.Infra.Data.Workers;
using ProgramAcad.Infra.IoC;
using ProgramAcad.Services.Modules.Algoritmos.MappingProfile;
using System;

namespace ProgramAcad.API.Presentation
{
    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            ConfigurationRoot = builder.Build();
        }

        public IConfigurationRoot ConfigurationRoot { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AddAutoMapperSetup(services);

            services.AddCors(configs =>
            {
                configs.AddPolicy("AllowAnyOrigin", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });
            services.AddControllers(configs =>
            {
                var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                                 .RequireAuthenticatedUser()
                                 .Build();
                configs.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddDbContext<ProgramAcadDataContext>(options =>
            {
                options.UseSqlServer(ConfigurationRoot.GetConnectionString("ProgramAcadDatabase"));
            });

            services
                .AddAppSettingsConfigs(ConfigurationRoot)
                .AddSwaggerDocumentation()
                .AddApiClients(ConfigurationRoot)
                .AddFirebaseApp();

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.RequireHttpsMetadata = false;
                jwtOptions.SaveToken = true;
                jwtOptions.Authority = "https://securetoken.google.com/program-acad";
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "https://securetoken.google.com/program-acad",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = "program-acad"
                };
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAnyOrigin");
            app.UseSwagger();
            app.UseSwaggerUI(configs =>
            {
                configs.SwaggerEndpoint("/swagger/v1/swagger.json", "ProgramAcad Web API v1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddAutoMapperSetup(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(configs =>
            {
                configs.AddProfile(new AlgoritmoMappingProfile());
            }, AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}