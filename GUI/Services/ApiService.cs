using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public async Task<string> GetSensorData(int? sensorId, string sensorType,
            DateTime? startDateTime, DateTime? endDateTime, double? value, int? page, int? pageSize,
            string sortBy, bool? ascending)
        {
            string requestUri = AddParamsToUri("/api/Sensors/GetSensorData", sensorId, sensorType, startDateTime, 
                endDateTime, value,
                page, pageSize, sortBy, ascending);
            HttpResponseMessage response = await _client.GetAsync(requestUri);
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSensorDataInFormat(string format, int? sensorId, string sensorType, 
            DateTime? startDateTime, DateTime? endDateTime, double? value,
            int? page, int? pageSize, string sortBy, bool? @ascending)
        {
            string requestUri = AddParamsToUri($"/api/Sensors/GetSensorDataInFormat/{format}", 
                sensorId, sensorType, startDateTime, endDateTime, value, page, pageSize, sortBy, ascending);
            
            HttpResponseMessage response = await _client.GetAsync(requestUri);
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }

        private string AddParamsToUri(string uri, 
            int? sensorId, string sensorType, DateTime? startDateTime, DateTime? endDateTime, double? value, 
            int? page, int? pageSize,string sortBy, bool? ascending)
        {
            List<string> requestParams = new List<string>();
            if(sensorId.HasValue)
                requestParams.Add($"sensorId={sensorId.Value}");
            if(sensorType != null)
                requestParams.Add($"sensorType={sensorType}");
            if(startDateTime.HasValue)
                requestParams.Add($"startDateTime={startDateTime.Value:o}");
            if(endDateTime.HasValue)
                requestParams.Add($"endDateTime={endDateTime.Value:o}");
            if(value.HasValue)
                requestParams.Add($"value={value.Value}");
            if(page.HasValue)
                requestParams.Add($"page={page.Value}");
            if(pageSize.HasValue)
                requestParams.Add($"pageSize={pageSize.Value}");
            if(ascending.HasValue)
                requestParams.Add($"ascending={ascending.Value}");
            if(sortBy != null)
                requestParams.Add($"sortBy={sortBy}");

            if (requestParams.Count > 0)
            {
                uri +=  requestParams.Aggregate("?", (current, param) => current + "&" + param);
            }

            return uri;
        }
    }
}