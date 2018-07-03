using System.Runtime.Serialization;

namespace DAO
{
    [DataContract]
    public class ComplexData : BaseData
    {
        [DataMember]
        protected string CalculType { get; set; }

        public ComplexData(Delay delay, CalculType calculType, DeviceTypes deviceType)
            :base(delay,deviceType)
        {
            this.CalculType = calculType.ToString();
        }
    }

}
