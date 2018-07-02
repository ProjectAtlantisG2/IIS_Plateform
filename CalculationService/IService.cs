using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using DAO;

namespace CalculationService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService
    {


        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "")]
        HttpResponseMessage PostGlobalComplexData(ComplexDataRequest complexDataRequest);

        Task<RawDataResponse> GetDataForCalculation(RawDataRequest rawDataRequest);
        // TODO: ajoutez vos opérations de service ici
    }
}
