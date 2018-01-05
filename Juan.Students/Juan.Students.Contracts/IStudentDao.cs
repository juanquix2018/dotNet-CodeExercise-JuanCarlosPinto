using Juan.Students.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Students.Contracts
{
    public interface IStudentDao
    {
        void Create(Student student);
        void Delete(Student student);
        Student GetForName(string name);
        ICollection<Student> GetAll();
    }
}
