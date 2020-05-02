using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

        private static void InjectServices(IServiceCollection services, IConfiguration configuration,
            bool isDevelopment)
        {
            InstallCors(services, isDevelopment);
            InstallMvc(services);
            InstallSwagger(services);
            InstallEntryContextAndIdentity(services);
            InstallAutoMapper(services, configuration);

            Access.Authentication.ServiceInjection.ConfigureServices(services, configuration);
            Access.Entries.ServiceInjection.ConfigureServices(services, configuration);
        }

        private static void InstallCors(IServiceCollection services, bool isDevelopment)
        {
            if (isDevelopment)
            {
                AllowDevelopmentOrigins(services);
                return;
            }
            
            AllowProductionOrigins(services);
        }

        private static void AllowDevelopmentOrigins(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                });
            });
        }

        private static void AllowProductionOrigins(IServiceCollection services)
        {
            // TODO: specify allowed origins for production
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                });
            });
        }

        private static void InstallMvc(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            });
        }

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            var builder = new ServiceCollection()
                .AddLogging()
                .AddMvc()
                .AddNewtonsoftJson()
                .Services.BuildServiceProvider();

            return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
        }

        private static void InstallSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "BCI SelfServe API", Version = "v1"});
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
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
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