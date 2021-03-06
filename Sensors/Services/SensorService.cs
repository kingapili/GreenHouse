using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Model;

namespace Sensors.Services
{
    public class SensorService : ServiceCollection, ISensorService
    {
        private readonly List<ISensor> _sensors;
        private CancellationTokenSource _tokenSource;
        
        public SensorService(List<ISensor> sensors, CancellationTokenSource tokenSource)
        {
            _sensors = sensors;
            _tokenSource = tokenSource;
        }

        public List<ISensor> GetSensors()
        {
            return _sensors;
        }

        public ISensor GetSensor(int id)
        {
            return _sensors.SingleOrDefault(s => s.Id == id);
        }

        public CancellationTokenSource GetTokenSource()
        {
            return _tokenSource;
        }

        public CancellationTokenSource GetNewTokenSource()
        {
            _tokenSource = new CancellationTokenSource();
            return _tokenSource;
        }
    }
}