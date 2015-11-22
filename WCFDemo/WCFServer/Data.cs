using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFData;
using WCFIService;
namespace WCFServer
{
    public class Data : WCFIService.IData
    {
        public string SayHello(string name)
        {
            return string.Format("Hello {0}", name);
        }

        public string SampleLife(string name)
        {
            return string.Format("我们从来着急爱别人，却忘了停下来爱自己！\r\n----------致 {0}", name);
        }

        public UserInfo GetUser()
        {
            return new UserInfo
            {
                UserID = 1,
                UserName = "SL"
            };
        }
    }
}
