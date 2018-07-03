using System.Net.Http;
using System.Text;
using System.Net;
using DAO;
using System.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System;

namespace CalculationService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service : IService
    {
        private static readonly string mongoConnectionString = ConfigurationManager.AppSettings["MongoConnectionString"];
        private static readonly string mongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];
        private static readonly string mongoCollectionName = ConfigurationManager.AppSettings["MongoCollectionName"];
        private static readonly string javaPlatformBaseUri = ConfigurationManager.AppSettings["JavaPlatformBaseUri"];
        private static readonly HttpClient client = new HttpClient();

        public void PostGlobalComplexData(ComplexDataRequest complexDataRequest)
        {
            try
            {
                if (complexDataRequest == null) throw new WebFaultException<string>("", HttpStatusCode.MethodNotAllowed);
                var collection = complexDataRequest.ToCollectionName();
                if (collection == null) throw new WebFaultException<string>("", HttpStatusCode.MethodNotAllowed);
                MongoConnection mongoConnection = new MongoConnection(mongoConnectionString, mongoDatabaseName);
                Enum.TryParse(complexDataRequest.Delay, out Delay delay);
                var response = mongoConnection.GetData(complexDataRequest.ToCollectionName(), DateTime.Parse(complexDataRequest.From), DateTime.Parse(complexDataRequest.To), delay);
                    throw new WebFaultException<string>(response, HttpStatusCode.OK);

            }
            catch { }
        }

        public async Task<RawDataResponse> GetDataForCalculation(RawDataRequest rawDataRequest)
        {
            HttpResponseMessage response = await client.PostAsync(javaPlatformBaseUri + "/calculation", new StringContent(JsonConvert.SerializeObject(rawDataRequest), Encoding.UTF8, "application/json"));
            
            //// MODIF CA 
            return null;
        }
    }
}
