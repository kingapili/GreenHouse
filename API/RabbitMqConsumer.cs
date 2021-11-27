using System;
using System.Threading.Tasks;
using API.Services;
using MassTransit;
using Model;

namespace API
{
    public class RabbitMqConsumer : IConsumer<SensorData>
    {
        private SensorsService _service;

        public RabbitMqConsumer(SensorsService service)
        {
            _service = service;
        }

        public Task Consume(ConsumeContext<SensorData> context)
        {
            Console.WriteLine(
                $"Inserting: {context.Message.SensorId}, {context.Message.SensorType}, {context.Message.DateTime}, {context.Message.Value}, {context.Message.Unit}");
            _service.Create(new Models.SensorData(context.Message));
            return Task.CompletedTask;
        }
    }
}