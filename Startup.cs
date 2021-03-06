﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using pokemongo_api.DataAccess;
using pokemongo_api.Services;

namespace pokemongo_api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
 
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }
 
        public static IConfigurationRoot Configuration { get; private set; }
 
        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddSingleton<IPokedexService, PokedexService>();
            services.AddSingleton<ITypeDistinctionService, TypeDistinctionService>();
            services.AddSingleton<IS3DataAccess, S3DataAccess>();

            AwsConfiguration awsConfiguration = new AwsConfiguration();
            Configuration.GetSection("AwsConfiguration").Bind(awsConfiguration);
            services.AddSingleton<IAwsConfiguration>(awsConfiguration);

            // Pull in any SDK configuration from Configuration object
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
        }
 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLambdaLogger(Configuration.GetLambdaLoggerOptions());
            app.UseMvc();
        }
    }
}