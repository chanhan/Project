using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WCFModel;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // User user = GetUser("Fly");
            UserServiceClient client = new UserServiceClient();
            WCFUserService.UserServiceClient newClient = new WCFUserService.UserServiceClient();
            Service2Callback callback = new Service2Callback();
            InstanceContext context = new InstanceContext(callback);
            Service2.Service2Client service2 = new Service2.Service2Client(context);
            service2.DoWork();
            WCFUserService.User user = new WCFUserService.User();
            newClient.GetUser(1,out user);
            Console.WriteLine(user.Name);
            Console.WriteLine(newClient.AddUser(new WCFUserService.User { Name = "jinjin", NickName = "yezi" }));
            Console.WriteLine(client.AddUser(new User { Name = "jingjing", NickName = "yezi" }));
            var pro = new ChannelFactory<Wcf.IService1>("service1").CreateChannel();
            Console.WriteLine(pro.GetData(100));
            try
            {
                newClient.Div(10, 0);
            }
            catch (FaultException<Test.WCFUserService.CalculatorError> ex)
            {
                Console.WriteLine(ex.Detail.ErrorMessage);
                newClient.Abort();
            }
            client.Close();
            newClient.Close();
            Console.ReadKey();
        }
        public static User GetUser(string nickName)
        {
            DataTable dt = new DBSqlHelper().ExecuteDataTable("select * from [user] where nickname='" + nickName + "'");
            return new User
            {
                ID = Convert.ToInt32(dt.Rows[0]["ID"]),
                Name = dt.Rows[0]["Name"].ToString(),
                NickName = dt.Rows[0]["NickName"].ToString()
            };
        }
    }
}
