using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using WCFModel;

namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var pro = new ChannelFactory<IWCFService.IService>("WcfDemo").CreateChannel();
            User user= pro.GetUser("OK");
            Console.WriteLine(user.Name);
            ((IChannel)pro).Close();
            Console.ReadKey();
        }
    }  
}
