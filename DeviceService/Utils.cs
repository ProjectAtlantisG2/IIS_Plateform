using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace DeviceService
{
    public static class Utils
    {
        public static void createCustomMessage(HttpClient client, string uri, string response)
        {
            client.PostAsync(uri + "/rabbitMQ", new StringContent(response, Encoding.UTF8, "application/json"));
        }
    }
}