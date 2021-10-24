using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sensors.Configuration;

namespace Sensors.Services
{
    public class RabbitMqService : ServiceCollection, IRabbitMqService
    {
        private readonly IOptions<RabbitMqOptions> _options;

        public RabbitMqService(IOptions<RabbitMqOptions> options)
        {
            _options = options;
        }
    }
}