using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  public  class Student:ICloneable
    {
      public int age;
      public string Name;
      public Student student;
      public Student(int age,string Name)
      {
          this.age = age;
          this.Name = Name;
        //  this.Copy();
      }
      private void Copy()
      {
        student=  this.MemberwiseClone() as Student;
      }
      private Student(Student s)
      {
          student = s;
      }
      public override string ToString()
      {
          return this.age+"-------"+this.Name;
      }
        public object Clone()
        {
            return new Student(this.age,this.Name);
        }
    }
}
