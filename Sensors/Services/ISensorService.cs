using System.Collections.Generic;
using Model;

namespace Sensors.Services
{
    public interface ISensorService
    {
        public List<ISensor> GetSensors();
        public ISensor GetSensor(int id);
    }
}