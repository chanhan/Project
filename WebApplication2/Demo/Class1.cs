using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class Class1
    {
        private int k1;
        private int k2;
        public Class1(int k)
        {
            K3 = k;
        }
        public int K3 { get; set; }
        public void ok()
        {
            K3++;
        }
        public int ok(int i)
        {
            K3++;
            return 0;
        }
        //public void ok(int i)
        //{
        //    K3++;
        //}
    }
}
