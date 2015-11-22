using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace WCFServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WCFServer.Data)))
            {
                host.Open();
                Console.WriteLine("WCF服务已启动");
                Console.ReadKey();
                host.Close();
            }
        }
    }
}
