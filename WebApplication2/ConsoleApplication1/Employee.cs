using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Employee : ICloneable
{
    public string Name;
    public string Title;
    public int Age;

    // Simple Emplyee constructor
    public Employee(string name, string title, int age)
    {
        Name = name;
        Title = title;
        Age = age;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public override string ToString()
    {
        return string.Format("{0} ({1}) - Age {2}", Name, Title, Age);
    }
}}
