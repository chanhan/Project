using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WCFModel;

namespace IWCFService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        User GetUser(string nickName);
        [OperationContract]
        int AddUser(User user);
    }
}
