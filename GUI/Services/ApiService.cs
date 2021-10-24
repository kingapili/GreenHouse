using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GUI.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GUI.Services
{
    public class ApiService : ServiceCollection, IApiService
    {
        private readonly HttpClient _client;
        private readonly IOptions<ApiOptions> _options;

        public ApiService(IOptions<ApiOptions> options)
        {
            _options = options;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_options.Value.ServerAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetTest()
        {
            HttpResponseMessage response = await _client.GetAsync("/WeatherForecast");
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}