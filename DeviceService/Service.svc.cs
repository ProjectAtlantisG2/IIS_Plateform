using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

namespace DeviceService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service : IService
    {
        public AppSettingsSection appSettings;
        private static HttpClient client = new HttpClient();
        private static string javaPlatformBaseUri;
        private static string gatewayPlatformBaseUri;
        private static IModel RabbitChannel;  

        public Service()
        {
            RabbitChannel = new RabbitClient("192.168.192.241").Channel;
            javaPlatformBaseUri = "http://localhost/Java/";
            gatewayPlatformBaseUri = "http://localhost/Gateway/";
        }
        
        public HttpResponseMessage PostDevice(Device device)
        {
            if (device == null) return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            client.PostAsync(javaPlatformBaseUri + "/device", new StringContent(JsonConvert.SerializeObject(device), Encoding.UTF8, "application/json"));
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage PostTelemetry(Telemetry telemetry, string deviceId)
        {
            if (telemetry==null || deviceId == null) return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);

            RabbitChannel.QueueDeclare("Metrics", false, false, false, null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new {deviceId, telemetry.metricDate, telemetry.metricValue, telemetry.deviceType }));

            RabbitChannel.BasicPublish(exchange: "MetricsExchange", routingKey: "metrics", basicProperties: null, body: body);

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
