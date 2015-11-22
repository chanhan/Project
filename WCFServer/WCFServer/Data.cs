using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCFServer
{
  public  class Data:IData
    {
        public string SayHello(string name)
        {
            return string.Format("Hello {0}", name);
        }
    }
}
