using System.Net.Http;
using System.Text;
using System.Net;
using DAO;
using System.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System;
using System.Globalization;

namespace CalculationService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service : IService
    {
        private static readonly string mongoConnectionString = "mongodb://localhost:27017";
        private static readonly string mongoDatabaseName = "ComplexData";
        private static readonly string mongoCollectionName = ConfigurationManager.AppSettings["MongoCollectionName"];
        private static readonly string javaPlatformBaseUri = "http://192.168.192.249:8080/api-0.0.1-SNAPSHOT";
        private static readonly HttpClient client = new HttpClient();
        private static CultureInfo provider = CultureInfo.InvariantCulture;

        public string PostGlobalComplexData(ComplexDataRequest complexDataRequest)
        {
            string response = "";
            try
            {
                if (complexDataRequest == null) throw new WebFaultException<string>("", HttpStatusCode.MethodNotAllowed);
                var collection = complexDataRequest.ToCollectionName();
                if (collection == null) throw new WebFaultException<string>("", HttpStatusCode.MethodNotAllowed);
                MongoConnection mongoConnection = new MongoConnection(mongoConnectionString, mongoDatabaseName);
                Enum.TryParse(complexDataRequest.Delay, out Delay delay);
                response = mongoConnection.GetData(complexDataRequest.ToCollectionName(), DateTime.ParseExact(complexDataRequest.From, "dd/MM/yyyy HH:mm:ss:fff", provider), DateTime.ParseExact(complexDataRequest.To, "dd/MM/yyyy HH:mm:ss:fff", provider), delay);
                //throw new WebFaultException<string>(response, HttpStatusCode.OK);

            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }

        public async Task<RawDataResponse> GetDataForCalculation(RawDataRequest rawDataRequest)
        {
            HttpResponseMessage response = await client.PostAsync(javaPlatformBaseUri + "/calculation", new StringContent(JsonConvert.SerializeObject(rawDataRequest), Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();
            var rawData = JsonConvert.DeserializeObject<RawDataResponse>(contents);
            //// MODIF CA 
            return rawData;
        }
    }
}
