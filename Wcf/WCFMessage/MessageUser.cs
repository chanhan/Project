using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFModel;
namespace WCFMessage
{
    [MessageContract]
    public class ResponseMessageUser
    {
        [MessageHeader]
        public int Index { get; set; }
        [MessageBodyMember]
        public User user { get; set; }
    }
    [MessageContract]
    public class RequestMessageUser
    {
        [MessageBodyMember]
        public int ID { get; set; }
    }
}
