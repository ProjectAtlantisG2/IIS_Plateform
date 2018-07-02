using RabbitMQ.Client;
using System.Configuration;

namespace DeviceService
{
    public class RabbitClient
    {
        public IModel Channel { get; set; }
        private IConnection Connection { get; set; }

        public RabbitClient()
        {
            ConnectionFactory factory = new ConnectionFactory();
            // "guest"/"guest" by default, limited to localhost connections
            factory.HostName = ConfigurationManager.AppSettings["HostName"];

            //this.Connection = factory.CreateConnection();
            //this.Channel = this.Connection.CreateModel();
        }
    }
}