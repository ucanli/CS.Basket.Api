using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.Basket.Api.Infrastructure.Settings
{
    public class AppSettings
    {
        public RabbitMQSetting RabbitMQSetting { get; set; }
    }

    public class RabbitMQSetting
    {
        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
    }
}
