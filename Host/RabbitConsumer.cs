using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Host
{
    public class RabbitConsumer
    {
        private static IModel RabbitChannel;
        public RabbitConsumer()
        {
            RabbitChannel = new RabbitClient("192.168.192.241").Channel;
            RabbitChannel.QueueDeclare("Commands", false, false, false, null);
            var consumer = new EventingBasicConsumer(RabbitChannel);
            consumer.Received += (ch, ea) =>
            {
                string data = Encoding.UTF8.GetString(ea.Body);

                Console.WriteLine(" [x] Received {0}", data);
            };
            RabbitChannel.BasicConsume(queue: "Commands",
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
