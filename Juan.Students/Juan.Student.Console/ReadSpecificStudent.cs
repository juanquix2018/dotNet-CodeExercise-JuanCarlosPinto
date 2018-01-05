using Juan.Students.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Students.Consol
{
    public class ReadSpecificStudent
    {
        public string ReturnNameStudent()
        {
            string studentName = string.Empty;
            Console.Write("input [Name] student Name /> ");
            studentName = Console.ReadLine();
            
            return studentName;
        }
        
    }
}
