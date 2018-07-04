using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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
            javaPlatformBaseUri = "http://192.168.192.249:8080/api-0.0.1-SNAPSHOT";
            gatewayPlatformBaseUri = "http://192.168.192.106:80/gateway/";
        }

        public HttpResponseMessage PostDevice(Device device)
        {
            if (device == null) return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            client.PostAsync(javaPlatformBaseUri + "/device", new StringContent(JsonConvert.SerializeObject(device), Encoding.UTF8, "application/json"));
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage PostTelemetry(Telemetry telemetry, string deviceId)
        {
            if (telemetry == null || deviceId == null) return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);

            RabbitChannel.QueueDeclare("Metrics", false, false, false, null);
            var response = JsonConvert.SerializeObject(new { deviceId, telemetry.metricDate, telemetry.metricValue, telemetry.deviceType });

            var body = Encoding.UTF8.GetBytes(response);

            RabbitChannel.BasicPublish(exchange: "MetricsExchange", routingKey: "metrics", mandatory: true, basicProperties: null, body: body);
            
            return ProcessResponse(response);
        }
        
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

        public HttpResponseMessage ProcessResponse(string response)
        {
            if (response == null) return null;
            Utils.createCustomMessage(client, javaPlatformBaseUri, response);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage PostCommand(string command, string deviceId)
        {
            if (command == null || deviceId == null) return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            var text = new StringContent(JsonConvert.SerializeObject(new { command = command }), Encoding.UTF8, "application/json");
            Console.WriteLine("New Command is coming throught Microsoft");
            client.PostAsync(gatewayPlatformBaseUri + deviceId + "/command", text);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

    }
}
