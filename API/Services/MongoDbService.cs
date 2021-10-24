using System.Net.Http;
using API.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class MongoDbService : ServiceCollection, IMongoDbService
    {
        private readonly IOptions<MongoDbOptions> _options;

        public MongoDbService(IOptions<MongoDbOptions> options)
        {
            _options = options;
        }
    }
}