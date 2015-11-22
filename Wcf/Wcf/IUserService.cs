using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFModel;
using WCFMessage;
namespace Wcf
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUserService”。
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        int AddUser(User user);
        [OperationContract]
        ResponseMessageUser GetUser(RequestMessageUser id);
        [OperationContract]
        [FaultContract(typeof(CalculatorError))]
        double Div(int num1,int num2);
    }
}
