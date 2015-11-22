using Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    fun();
                }
                catch (OutOfMemoryException)
                {
                    fun();
                    GC.Collect();
                    
                }
            }
         

         //   Fun3();
          //  Console.ReadKey();
        }
        private static void fun()
        {
            ThreadsCount tc = new ThreadsCount(100000);
            tc.ThreadStart();
        }
        private static void Fun3()
        {

            new MathOperations().Math();
        }
        private static void Fun2() {
            int i = 100;
            object o = i;
            Console.WriteLine(o);
            Console.WriteLine(o.GetType());
            o = null;
            Console.WriteLine(i);
            int k = 1000;
            object obj = k;
            if (obj.GetType().FullName=="System.Int32")
            {
                int kk = (int)obj;
                k = 100000;
                Console.WriteLine(kk);
                Console.WriteLine(k);

            }
        }
        private void Fun1() {
            Class1 c1 = new Class1(5);
            c1.ok();
            Console.WriteLine(c1.K3);
            Struct2 s2 = new Struct2(6);
            s2.ok();
            //  Console.WriteLine(s2.K3);
            Team team = new Team();
            team.AddMember(new Employee("Anders", "Developer", 26));
            team.AddMember(new Employee("Bill", "Developer", 46));
            team.AddMember(new Employee("Steve", "CEO", 36));

            Team clone = team.Clone() as Team;

            // Display the original team.
            Console.WriteLine("Original Team:");
            Console.WriteLine(team);

            // Display the cloned team.
            Console.WriteLine("Clone Team:");
            Console.WriteLine(clone);

            // Make changes.
            Console.WriteLine("*** Make a change to original team ***");
            Console.WriteLine(Environment.NewLine);
            team.TeamMembers[0].Title = "PM";
            team.TeamMembers[0].Age = 30;
            Console.WriteLine(team);

            Console.WriteLine("*** Student ***");
            Student student = new Student(15, "Tim");
            Student cloneStudent = student.Clone() as Student;
            Console.WriteLine(student);
            Console.WriteLine(cloneStudent);
            Console.WriteLine("*** Change Student ***");
            cloneStudent.age = 16;
            cloneStudent.Name = "Jacy";
            Console.WriteLine(student);
            Console.WriteLine(cloneStudent);
        }
    }
}
