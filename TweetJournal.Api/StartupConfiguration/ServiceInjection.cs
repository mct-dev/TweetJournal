using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TweetJournal.Access.Entries;

namespace TweetJournal.Api.StartupConfiguration
{
    [ExcludeFromCodeCoverage]
    internal static class ServiceInjection
    {
        public static void InjectDevelopmentServices(IServiceCollection services, IConfiguration configuration)
        {
            InjectServices(services, configuration, true);
        }
        
        public static void InjectProductionServices(IServiceCollection services, IConfiguration configuration)
        {
            InjectServices(services, configuration, false);
        }

        private static void InjectServices(IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {

            InstallMvc(services);
            InstallSwagger(services);
            InstallEntryContextAndIdentity(services);
            InstallAutoMapper(services, configuration);
            
            Access.Authentication.ServiceInjection.ConfigureServices(services, configuration);
            Access.Entries.ServiceInjection.ConfigureServices(services, configuration);
        }

        private static void InstallMvc(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvcCore()
                .AddApiExplorer();
        }
        
        private static void InstallSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BCI SelfServe API", Version = "v1" });
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme{Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }}, new List<string>()}
                });
            });
        }

        private static void InstallEntryContextAndIdentity(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EntryContext>();
        }

        private static void InstallAutoMapper(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AutoMapperConfiguration.GetMapperAssemblies());
        }
    }
}