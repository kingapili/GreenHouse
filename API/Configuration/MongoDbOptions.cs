namespace API.Configuration
{
    public class MongoDbOptions
    {
        public const string MongoDb = "MongoDB";
        
        public string DatabaseName { get; set; }
        public string ServerAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SensorReadoutsCollectionName { get; set; }
    }
}
