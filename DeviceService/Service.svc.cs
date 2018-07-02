using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DeviceService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service : IService
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string javaPlatformBaseUri = ConfigurationManager.AppSettings["JavaPlatformBaseUri"];
        private static readonly string gatewayPlatformBaseUri = ConfigurationManager.AppSettings["GatewayPlatformBaseUri"];
        private static readonly IModel RabbitChannel = new RabbitClient().Channel; 
        
        public HttpResponseMessage PostDevice(Device device)
        {
            if (device == null) return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            client.PostAsync(javaPlatformBaseUri + "/device", new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(device), Encoding.UTF8, "application/json"));
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage PostTelemetry(string metricDate, string deviceType, string metricValue, string deviceId)
        {
            if (metricDate == null || deviceType==null || metricValue==null || deviceId == null) return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            RabbitChannel.QueueDeclare("Test", false, false, false, null);

            string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            RabbitChannel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);

            //// NEED TO SEND DATA TO JMS ////

            //client.PostAsync(javaPlatformBaseUri + "/device/" + deviceId + "/telemetry", new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(telemetry), Encoding.UTF8, "application/json"));
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        //// NEED TO RECEIVE COMMAND FROM JMS ////
        public void PostCommand()
        {
            RabbitChannel.QueueDeclare("Commands", false, false, false, null);
            var consumer = new EventingBasicConsumer(RabbitChannel);
            consumer.Received += (ch, ea) =>
            {
                string data = Encoding.UTF8.GetString(ea.Body);

                RabbitChannel.BasicAck(ea.DeliveryTag, false);
            };
        }

        public HttpResponseMessage PostCommand(string command, string deviceId)
        {
            if (command == null || deviceId == null) return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            client.PostAsync(gatewayPlatformBaseUri + "/device/" + deviceId + "/command", new StringContent(command, Encoding.UTF8, "application/json"));
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
