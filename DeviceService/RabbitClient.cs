﻿using RabbitMQ.Client;

namespace DeviceService
{
    public class RabbitClient
    {
        public IModel Channel { get; set; }
        private IConnection Connection { get; set; }

        public RabbitClient(string hostname)
        {
            if (hostname == null) return;
            ConnectionFactory factory = new ConnectionFactory();

            factory.HostName = hostname;
            factory.UserName = "payara";
            factory.Password = "payara";

            this.Connection = factory.CreateConnection();
            this.Channel = this.Connection.CreateModel();
        }
    }
}