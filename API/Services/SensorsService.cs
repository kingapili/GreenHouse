using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<SensorData> GetAll(int? sensorId, string sensorType, DateTime? dateTime, double? value, int? page,
            int? pageSize, string sortBy, bool? ascending)
        {
            // set default values if not provided
            page ??= 1;
            pageSize ??= 1000;
            ascending ??= false;
            sortBy ??= "DateTime";

            FilterDefinition<Models.SensorData> filter = Builders<Models.SensorData>.Filter.Empty;

            if (sensorId.HasValue)
                filter &= Builders<Models.SensorData>.Filter.Eq(x => x.SensorId, sensorId);

            if (sensorType != null)
                filter &= Builders<Models.SensorData>.Filter.Eq(x => x.SensorType, sensorType);

            if (dateTime.HasValue)
                filter &= Builders<Models.SensorData>.Filter.Eq(x => x.DateTime, dateTime);

            if (value.HasValue)
                filter &= Builders<Models.SensorData>.Filter.Eq(x => x.Value, value);

            SortDefinition<Models.SensorData> sort;
            if ((bool) ascending)
                sort = Builders<Models.SensorData>.Sort.Ascending(sortBy);
            else
                sort = Builders<Models.SensorData>.Sort.Descending(sortBy);

            var result = _readouts
                .Aggregate()
                .Match(filter)
                .Sort(sort)
                .Skip((int) ((page - 1) * pageSize))
                .Limit((int) pageSize)
                .ToList();

            return result.Select(data => data.ToDto()).ToList();
        }

        public void Create(Models.SensorData readout)
        {
            _readouts.InsertOne(readout);
        }
    }
}