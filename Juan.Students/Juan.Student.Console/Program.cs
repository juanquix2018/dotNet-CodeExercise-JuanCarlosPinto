
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juan.Students.Entities;
using Juan.Students.Biz;
using Juan.Students.Context;
using System;
using System.IO;

namespace Juan.Students.Consol
{
    public class ReadFile
    {
        public Student[] LoadHighScores(string fileName)
        {
            string[] studentScoresText = File.ReadAllLines(fileName);
            Student[] studentScores = new Student[studentScoresText.Length];
            for (int index = 0; index < studentScoresText.Length; index++)
            {
                string[] tokens = studentScoresText[index].Split(',');

                string type = tokens[0];
                string name = tokens[1];
                string gender = tokens[2];
                string timestamp = tokens[3];

                Student st = new Student() { Type = type, Name = name, Gender = gender, Timestamp = timestamp };

                studentScores[index] = st;
            }
            return studentScores;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            ReadFile rdFile = new ReadFile();
            var allStudents = rdFile.LoadHighScores(args[0]);
            StudentManager StudentManager = new StudentManager(new StudentDao());

            foreach (var item in allStudents)
            {
                StudentManager.StoreStudent(item);
            }

            int size = args.Length;
            string[] separated;
            bool name = false;
            bool type = false;
            bool gender = false;
            if (size > 1)
            {
                for (int i = 1; i < size; i++)
                {
                    separated = args[i].Split('=');
                    if (separated[0].CompareTo("name") == 0)
                    {
                        name = true;
                    }
                    if (separated[0].CompareTo("type") == 0)
                    {
                        type = true;
                    }
                    if (separated[0].CompareTo("gender") == 0)
                    {
                        gender = true;
                    }
                }
            }

            ShowCommands(StudentManager, name,  type,  gender);

            bool exit;

            do
            {
                Console.WriteLine("* Main Menu *");
                Console.WriteLine("1 Create new students");
                Console.WriteLine("2 Delete a specific student");
                Console.WriteLine("3 Searches ordered ");
                Console.WriteLine("4 salir");
                int opc = int.Parse(Console.ReadLine());
                ReadStudent readStudent = new ReadStudent();
                ReadSpecificStudent readSpecificStudent = new ReadSpecificStudent();
                switch (opc)
                {
                    case 1:
                        int opc1 = 0;
                        do
                        {
                          var a =  StudentManager.StoreStudent(readStudent.ReturnStudent());
                            Console.WriteLine("do you want register other student?   Yes press -> 1       No press -> 0 ");
                            opc1 = int.Parse(Console.ReadLine());
                        } while (opc1 == 1);
                        break;
                    case 2:
                        int opc2 = 0;
                        do
                        {
                            StudentManager.DeleteStudent(readSpecificStudent.ReturnNameStudent());
                            Console.WriteLine("do you want delete other student?   Yes press -> 1       No press -> 0 ");
                            opc2 = int.Parse(Console.ReadLine());
                        } while (opc2 == 1);
                        break;
                    case 3:
                        Console.WriteLine("** Searches ordered Menu **");
                        Console.WriteLine("1 By name");//alphabetically
                        Console.WriteLine("2 By student type");//sorting by date, most recent to least recent
                        Console.WriteLine("3 By gender and type");//or viceversa, sorting by date, most recent to least recent
                        Console.WriteLine("4 Exit");
                        int opc3 = int.Parse(Console.ReadLine());
                        switch (opc3)
                        {
                            case 1:
                                var lsStudents = StudentManager.GetAllStudents().OrderBy(e => e.Name).ToList();
                                foreach (var item in lsStudents)
                                {
                                    Console.WriteLine("{0} {1} {2} {3} ", item.Type, item.Name, item.Gender, item.Timestamp);
                                }
    
                                break;
                            case 2:
                                var lsStudents2 = StudentManager.GetAllStudents().GroupBy(std => std.Type).ToList();

                                foreach (var item2 in lsStudents2)
                                {
                                    Console.WriteLine("Type Group: {0}", item2.Key);
                                    foreach (Student s in item2.OrderByDescending(o => o.Timestamp)) 
                                        Console.WriteLine("{0} {1} {2} ", s.Name, s.Gender, s.Timestamp);
                                }
                                break;
                            case 3:
                                var lsStudents3 = StudentManager.GetAllStudents();
                                
                                var resultmultiplekey = from stu in lsStudents3
                                                        group stu by new { stu.Type, stu.Gender}
                                                        into egroup
                                                        orderby egroup.Key.Type, egroup.Key.Gender
                                                        select egroup;
                                foreach (var group in resultmultiplekey)
                                {
                                    Console.WriteLine("Type  {0},  Gender {1}", group.Key.Type, group.Key.Gender);
                               
                                    foreach (var item in group.OrderByDescending(o => o.Timestamp))
                                    {
                                        Console.WriteLine("{0} {1} {2} {3} ", item.Type, item.Name, item.Gender, item.Timestamp);
                                    }
                                    Console.WriteLine("---------------------------------------------");
                                }

                                break;
                        }
                        break;
                    case 4:
                       // Console.WriteLine("** bye **");
                        exit = false;
                        break;
                }
              
                Console.WriteLine("do you want continue in system?      Yes press -> 1       No press -> 0 ");

                int option = int.Parse(Console.ReadLine());
                if (option == 1)
                    exit = true;
                else
                    exit = false;

            } while (exit);
            Console.ReadKey(); 

        }

        private static void ShowCommands(StudentManager StudentManager, bool name, bool type, bool gender)
        {
            if (name)
            {
                var lsStudents = StudentManager.GetAllStudents().OrderBy(e => e.Name).ToList();
                foreach (var item in lsStudents)
                {
                    Console.WriteLine("{0} {1} {2} {3} ", item.Type, item.Name, item.Gender, item.Timestamp);
                }
            }
         
            if (type)
            {
                var lsStudents2 = StudentManager.GetAllStudents().GroupBy(std => std.Type).ToList();

                foreach (var item2 in lsStudents2)
                {
                    Console.WriteLine("Type Group: {0}", item2.Key);
                    foreach (Student s in item2.OrderByDescending(o => o.Timestamp))
                        Console.WriteLine("{0} {1} {2} ", s.Name, s.Gender, s.Timestamp);
                }
            }
            if (type & gender)
            {
                var lsStudents3 = StudentManager.GetAllStudents();

                var resultmultiplekey = from stu in lsStudents3
                                        group stu by new { stu.Type, stu.Gender }
                                        into egroup
                                        orderby egroup.Key.Type, egroup.Key.Gender
                                        select egroup;
                foreach (var group in resultmultiplekey)
                {
                    Console.WriteLine("Type  {0},  Gender {1}", group.Key.Type, group.Key.Gender);

                    foreach (var item in group.OrderByDescending(o => o.Timestamp))
                    {
                        Console.WriteLine("{0} {1} {2} {3} ", item.Type, item.Name, item.Gender, item.Timestamp);
                    }
                    Console.WriteLine("---------------------------------------------");
                }
            }

        }
    }
}
