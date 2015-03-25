using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AuthZ.DataLayer
{
    [DataContract]
    public sealed class AudienceDto
    {
        [DataMember]
        public string ClientId { get; set; }

        [DataMember]
        public string Secret { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Issuer { get; set; }
    }
}
