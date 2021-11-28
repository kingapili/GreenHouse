using System.Collections.Generic;
using System.Threading;
using Model;

namespace Sensors.Services
{
    public interface ISensorService
    {
        public List<ISensor> GetSensors();
        public ISensor GetSensor(int id);

        public CancellationTokenSource GetTokenSource();

        public CancellationTokenSource GetNewTokenSource();
    }
}