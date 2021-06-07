using CS.Basket.Api.Applications.Services;
using CS.Basket.Api.Entities;
using CS.Basket.Api.Entities.Validators;
using CS.Basket.Api.Infrastructure.Filters;
using CS.Basket.Api.Infrastructure.Repositories;
using CS.Basket.Api.Infrastructure.ServiceCollectionExtensions;
using CS.Basket.Api.Infrastructure.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;

namespace CS.Basket.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
 
            AppSettings options = Configuration.GetSection("AppSettings").Get<AppSettings>();
            services.AddSingleton(options);

            IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);

            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration.GetValue<string>("AppSettings:CacheSettings:ConnectionString"), true);

                configuration.ResolveDns = true;

                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddHealthChecks()
                    .AddRedis(Configuration["AppSettings:CacheSettings:ConnectionString"], "Redis Health", HealthStatus.Degraded);

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateFilter));
                options.Filters.Add(typeof(GlobalExceptionFilter));
            }).AddFluentValidation(fv =>
            {
                fv.ImplicitlyValidateChildProperties = true;
            });
            services.AddTransient<IValidator<ShoppingCartItem>, ShoppingCartItemValidator>();
            services.AddTransient<IValidator<ShoppingCart>, ShoppingCartValidator>();

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Basket Api",
                    Version = "v1"
                });
            });

            services.AddMassTransit(cfg => cfg.AddBus(BusServices.BuildBuss));
            services.AddHostedService<MassTransitConsoleHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket Api");
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
