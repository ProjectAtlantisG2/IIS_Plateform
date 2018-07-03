using RabbitMQ.Client;

namespace DeviceService
{
    public class RabbitClient
    {
        public IModel Channel { get; set; }
        private IConnection Connection { get; set; }

        public RabbitClient(string hostname)
        {
            ConnectionFactory factory = new ConnectionFactory();

            factory.HostName = hostname;
            factory.UserName = "luigi";
            factory.Password = "lama";

            this.Connection = factory.CreateConnection();
            this.Channel = this.Connection.CreateModel();
        }
    }
}