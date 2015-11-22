using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFData;
namespace WCFIService
{
    [ServiceContract]
    public interface IData
    {
        [OperationContract]
        string SayHello(string name);
        [OperationContract]
        string SampleLife(string name);
        [OperationContract]
        UserInfo GetUser();
    }
}
