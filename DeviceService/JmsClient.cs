using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;


namespace DeviceService
{
    public class JmsClient
    {
        private readonly string host = "192.168.192.241";
        private readonly int port = 4848;
        private readonly string CfName = "jms/AtlantisConnectionFactory";
        private readonly string QueueName = "jms/MetricsQueue";
    //    private static string USAGE =
    //"Usage: " + Environment.GetCommandLineArgs()[0] + NL +
    //"          [-host <hostname>] [-port <portnum>] " + NL +
    //"          [-cf <connection factory JNDI name>] " + NL +
    //"          [-queue <queue JNDI name>] [-topic <topic JNDI name>]";

        public JmsClient()
        {
        }

        

    }
}