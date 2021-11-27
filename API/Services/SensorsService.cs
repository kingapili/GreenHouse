using System;
using System.Collections.Generic;
using API.Configuration;
using Microsoft.Extensions.Options;
using Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace API.Services
{
    public class SensorsService
    {
        private readonly IMongoCollection<API.Models.SensorData> _readouts;

        public SensorsService(IOptions<MongoDbOptions> options)
        {
            var settings = options.Value;
            var credential =
                MongoCredential.CreateCredential("admin", settings.Username, settings.Password);
            var clientSettings = new MongoClientSettings
            {
                Credential = credential,
                Server = new MongoServerAddress(settings.ServerAddress)
            };

            var client = new MongoClient(clientSettings);

            var database = client.GetDatabase(settings.DatabaseName);

            _readouts = database.GetCollection<API.Models.SensorData>(settings.SensorReadoutsCollectionName);
        }

        public List<SensorData> Get()
        {
            var result = _readouts.Find(new BsonDocument()).ToList();
            List<SensorData> dtoList = new List<SensorData>();
            foreach (var data in result)
            {
                dtoList.Add(data.ToDto());
            }

            return dtoList;
        }

        public SensorData Get(int id) =>
            _readouts.Find(readout => readout.SensorId == id).FirstOrDefault().ToDto();

        public void Create(Models.SensorData readout)
        {
            _readouts.InsertOne(readout);
        }
    }
}