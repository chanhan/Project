using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFMessage;
using WCFModel;

namespace Wcf
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 UserService.svc 或 UserService.svc.cs，然后开始调试。
   
 public class UserService : IUserService
    {
        public int AddUser(WCFModel.User user)
        {
            return new DBSqlHelper().ExecuteSql("insert into [user] values('"+user.Name+"','"+user.NickName+"')");
        }
        public ResponseMessageUser GetUser(RequestMessageUser id)
        {
            DataTable dt= new DBSqlHelper().ExecuteDataTable("select * from [user] where id='"+id.ID+"'");
            return new ResponseMessageUser
            {
                Index = 0,
              user=  new User { Name = dt.Rows[0]["Name"].ToString() }
            };
        }


        public double Div(int num1, int num2)
        {
            if (num2 == 0)
            {
                CalculatorError error=new CalculatorError(100,"被除数不能为0");
                throw new FaultException<CalculatorError>(error,error.ErrorMessage);
            }
            return num1 / num2;
        }
    }
}
