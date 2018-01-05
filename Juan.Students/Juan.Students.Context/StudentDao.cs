using Juan.Students.Contracts;
using Juan.Students.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Students.Context
{
    public class StudentDao: IStudentDao
    {
        private List<Student> _lsStudents = new List<Student>();
        public void Create(Student student)
        {
            _lsStudents.Add(student);
        }
        public Student GetForName(string name)//for name, all others
        {
            Student student = null;
            var studentDelete = _lsStudents.FirstOrDefault(s => s.Name.Contains(name));
            student = _lsStudents.SingleOrDefault(d => d.Name == name);

            return student;
        }
        public void Delete(Student student)
        {
            var studentDelete = _lsStudents.FirstOrDefault(s => s.Name.Contains(student.Name));
            _lsStudents.Remove(studentDelete);
        }
        public ICollection<Student> GetAll()
        {
            List<Student> Students;
                Students = _lsStudents.ToList();

            return Students;
        }
    }
}
