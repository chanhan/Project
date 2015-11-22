using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
   public class Service2Callback:Test.Service2.IService2Callback
    {

        public void CallBack(string name)
        {
            Console.WriteLine("Hello "+name);
        }
    }
}
