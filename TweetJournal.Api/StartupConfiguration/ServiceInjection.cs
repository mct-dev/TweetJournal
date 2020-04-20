﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using TweetJournal.Api.Data;
using TweetJournal.Api.Services;

namespace TweetJournal.Api.StartupConfiguration
{
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
            var connectionString = configuration.GetConnectionString("EntriesDbContext");

            InstallMvc(services);
            InstallSwagger(services);
            
            services.AddDbContext<EntryContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped<EntryService, EntryServiceImp>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EntryContext>();
            
            Access.Authentication.ServiceInjection.ConfigureServices(services, configuration);
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
        
        private static void InstallMvc(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvcCore()
                .AddApiExplorer();
        }
    }
}