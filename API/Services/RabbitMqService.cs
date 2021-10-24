using System.Net.Http;
using API.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace API.Services
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