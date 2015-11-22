using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class MathOperations
    {
        public delegate int DelMath(int x, int y);//声明委托
        public void Math()
        {
            DelMath del = new DelMath(Add);//定义委托，将方法名作为参数传递给委托
            //   DelMath del = Add;//简单写法
            Console.WriteLine(del(100, 2));
            del += Add2;//为委托添加多个方法
            //del += new DelMath(Add2);//为委托添加多个方法
            Func<int, int, int> fun1 = Add;
            Action<int,int> act1=Add3;
            Console.WriteLine(del(100, 2));//Add、Add2两个方法都会执行
            Console.WriteLine(TT(Add2, 100, 900));//将方法名作为参数传递给TT方法
        }
        private int Add(int x, int y)//装载在委托中的方法
        {
            return x + y;
        }
        private int Add2(int x, int y)//装载在委托中的方法
        {
            return x * y;
        }
        private void Add3(int x, int y)//装载在委托中的方法
        {
           x=  x * y;
        }

        public int TT(DelMath del, int x, int y)//其中一个参数是委托
        {
            return del(x, y);
        }

    }
}
