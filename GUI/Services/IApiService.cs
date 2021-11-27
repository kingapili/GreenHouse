using System;
using System.Threading.Tasks;

namespace GUI.Services
{
    public interface IApiService
    {
        public Task<string> GetSensorData(int? sensorId, string sensorType,
            DateTime? dateTime, double? value, int? page, int? pageSize,
            string sortBy, bool? ascending);

        public Task<string> GetSensorDataInFormat(string format, int? sensorId, string sensorType,
            DateTime? dateTime, double? value, int? page, int? pageSize,
            string sortBy, bool? ascending);
    }
}