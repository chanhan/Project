using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var pro = new ChannelFactory<WCFServer.IData>("WcfDemo").CreateChannel();
           Console.WriteLine(pro.SayHello("chen han"));
           ((IChannel)pro).Close();
            Console.ReadKey();
        }
    }
}
