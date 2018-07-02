using System.Runtime.Serialization;

namespace DAO
{
    [DataContract]
    public class ComplexData : BaseData
    {
        [DataMember]
        protected CalculType CalculType { get; set; }

        public ComplexData(Delay delay, CalculType calculType, DeviceType deviceType)
            :base(delay,deviceType)
        {
            this.CalculType = calculType;
        }
    }

}
