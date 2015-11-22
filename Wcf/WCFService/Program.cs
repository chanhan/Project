using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
namespace WCFService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WCFService.Service)))
            {
                host.Open();
                Console.WriteLine("服务已启动");
                Console.ReadKey();
                host.Close();
            } 

        }
    }
}
