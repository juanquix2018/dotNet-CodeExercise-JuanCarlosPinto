using Juan.Students.Contracts;
using Juan.Students.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Students.Biz
{
    public class StudentManager
    {
        private IStudentDao _studentDao;

        public StudentManager(IStudentDao studentDao)
        {
            _studentDao = studentDao;
        }

        public Student StoreStudent(Student student)
        {
            if (student != null)
            {
                student.Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                _studentDao.Create(student);
            }
            return student;
        }
        
        public bool DeleteStudent(string name)
        {
            var student = _studentDao.GetForName(name);
            _studentDao.Delete(student);
            return true;
        }

        public List<Student> GetAllStudents()
        {
            var lista = _studentDao.GetAll().ToList();
            return lista;
        }

    }
}
