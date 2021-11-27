using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace Sensors.Services
{
    public class SensorService : ServiceCollection, ISensorService
    {
        private readonly List<ISensor> _sensors;
        
        public SensorService(List<ISensor> sensors)
        {
            _sensors = sensors;
        }

        public List<ISensor> GetSensors()
        {
            return _sensors;
        }

        public ISensor GetSensor(int id)
        {
            return _sensors.SingleOrDefault(s => s.Id == id);
        }
    }
}