using CS.Basket.Api.Infrastructure.Settings;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace CS.Basket.Api.Infrastructure.ServiceCollectionExtensions
{
    public class BusServices
    {

        public static IBusControl BuildBuss(IServiceProvider provider)
        {

            var options = provider.GetRequiredService<IOptions<AppSettings>>();

            RabbitMQSetting rabbitMQSettings = options.Value.RabbitMQSetting;

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(rabbitMQSettings.Hostname, rabbitMQSettings.VirtualHost, h =>
                {
                    h.Username(rabbitMQSettings.Username);
                    h.Password(rabbitMQSettings.Password);
                });

                sbc.ConfigureJsonSerializer(settings => { settings.DefaultValueHandling = DefaultValueHandling.Include; return settings; });

                sbc.UseRetry(r => r.Incremental(5, TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(10)));

                sbc.UseCircuitBreaker(cb =>
                {
                    cb.TripThreshold = 15;
                    cb.ActiveThreshold = 10;
                    cb.ResetInterval = TimeSpan.FromMinutes(5);
                });
            });

            return bus;
        }
    }
}

