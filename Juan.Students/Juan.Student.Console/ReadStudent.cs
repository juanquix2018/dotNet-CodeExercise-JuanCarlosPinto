using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juan.Students.Entities;


namespace Juan.Students.Consol
{
    public class ReadStudent
    {
        public Student ReturnStudent()
        {

            Student student = new Student();
            Console.Write("input [kinder/elementary/high/university] student Type /> ");
            student.Type = Console.ReadLine();
            Console.Write("input [name]  student Name /> ");
            student.Name = Console.ReadLine();
            Console.Write("input [male/female]  student Gerder /> ");
            student.Gender = Console.ReadLine();

            return student;
        }
           
    }
}
