using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
   public struct Struct2:IDisposable,ICloneable
    {
      //  private int k1=0;
       // private int k2=0;
       //public int K3 { get; set; }
        public Struct2(int k)
        {
          //  k2++;
        }
        public void ok()
        {
           // K3++;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
