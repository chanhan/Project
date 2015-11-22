using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WCFData;
namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var pro = new ChannelFactory<WCFIService.IData>("WcfDemo").CreateChannel();
           Console.WriteLine( pro.SampleLife("陈函"));
           UserInfo user= pro.GetUser();
           Console.WriteLine(user.ToString());
           ((IChannel)pro).Close();
           Console.ReadKey();
           
        }
    }
}
