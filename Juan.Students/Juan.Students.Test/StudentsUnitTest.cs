using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Juan.Students.Biz;
using Juan.Students.Context;
using Juan.Students.Entities;

namespace Juan.Students.Test
{
    [TestClass]
    public class StudentsUnitTest
    {
        StudentManager _studentManager = new StudentManager(new StudentDao());
        
        [TestMethod]
        public void CreateStudentTest()
        {
            //create
            var studentTest = new Student()
            {
                Type = "Kinder",
                Name = "Leia",
                Gender = "F"
            };

            int before = _studentManager.GetAllStudents().Count;
            _studentManager.StoreStudent(studentTest);
            int after = _studentManager.GetAllStudents().Count;

            Assert.IsTrue(after > before);
        }

        [TestMethod]
        public void DeleteStudentTest()
        {
            var studentTest = new Student()
            {
                Type = "University",
                Name = "Yoda",
                Gender = "M"
            };
            _studentManager.StoreStudent(studentTest);

            int before = _studentManager.GetAllStudents().Count;
            _studentManager.DeleteStudent("Yoda");
            int after = _studentManager.GetAllStudents().Count;
           

            Assert.IsTrue(before > after);
        }

        [TestMethod]
        public void GetAllStudentsTest()
        {
            int before = _studentManager.GetAllStudents().Count;

            var studentTestOne = new Student()
            {
                Type = "Kinder",
                Name = "Leia",
                Gender = "F"
            };
            _studentManager.StoreStudent(studentTestOne);
            var studentTestTwo = new Student()
            {
                Type = "University",
                Name = "Yoda",
                Gender = "M"
            };
            _studentManager.StoreStudent(studentTestTwo);

            int after = _studentManager.GetAllStudents().Count;

            Assert.IsTrue(before == 0);
            Assert.IsTrue(after > 0);
        }
    }
}
