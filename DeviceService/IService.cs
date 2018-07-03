using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DeviceService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/{deviceId}/telemetry")]
        HttpResponseMessage PostTelemetry(Telemetry telemetry, string deviceId);

        [OperationContract]
        [WebInvoke(UriTemplate = "")]
        HttpResponseMessage PostDevice(Device device);

        void PostCommand();
    }
}
