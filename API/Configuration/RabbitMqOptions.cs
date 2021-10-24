namespace API.Configuration
{
    public class RabbitMqOptions
    {
        public const string RabbitMq = "RabbitMQ";
        
        public string ServerAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
