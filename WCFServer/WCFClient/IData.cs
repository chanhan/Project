using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace WCFServer
{
    [ServiceContract(Namespace = "WCFServer.IData")]
   public interface IData
    {
        [OperationContract]
        string SayHello(string name);
    }
}
