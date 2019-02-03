using System;
using System.Collections.Generic;
using System.Linq;
using WCFServiceWebRole1.BE;

namespace WCFServiceWebRole1.DL
{
    public class Dal_imp : Idal
    {
        
        List<Student> student;
        List<Teacher> teacher;
        List<MeetTest> meettest;
        List<ListPermission> permiss;
        List<Hash> hash;
        public static Dal_imp Instance { get
            {
                if (privateinstance is null)
                {
                    privateinstance = new Dal_imp();
                }
                return privateinstance;
            }
         }
        private static Dal_imp privateinstance=null;
        private Dal_imp()
        {
            try
            {
                //try get data from xml file
                UI.Service.DL.DLXml.GetSerialTEst();
                student = UI.Service.DL.DLXml.GetStudentList();
                teacher = UI.Service.DL.DLXml.GetTeacherList();
                meettest = UI.Service.DL.DLXml.GetMeetTestList();
                permiss = UI.Service.DL.DLXml.GetPermissionList();
                hash = new List<Hash>();
            }
            catch
            {
                student = new List<Student>();
                teacher = new List<Teacher>();
                meettest = new List<MeetTest>();
                permiss = new List<ListPermission>();
                hash = new List<Hash>();
                
            }
        }
        /// <summary>
        /// return permission list of user
        /// when The System try acsess permision of the user.
        /// and if the user not exist, create defult list permitiom.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> permission list of user</returns>
        public ListPermission ListpermissById(string id)
        {
            foreach (ListPermission item in permiss)
            {
                if (item.Id == id) { return item; }
            }
            return addStandartPermission(id);
        }


        /// <summary>
        /// cerate Standart listPermition to User.
        ///     1. if exist Teacher on his id, create teacher permission.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ListPermission addStandartPermission(string id)
        {
            bool[] per;
            try
            {
                ShowTeacher(id); 
                per = def.TeacherForHisTests.Clone() as bool[];
            }
            catch { per = def.NewIdGroup.Clone() as bool[]; }

            ListPermission temp = new ListPermission(id, new permission(id, per));
            permiss.Add(temp);
            return permiss[permiss.IndexOf(temp)];
        }
        /// <summary>
        /// terminal to store enter of new user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Hash object of the user Enter Data</returns>
        public Hash AddToken(string id)
        {
            //generate random Token
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            string h = GuidString;
            DeleteHashById(id);
            Hash n = new Hash(id, h, DateTime.Now);
            hash.Add(n);
            return n;
        }

        /// <summary>
        /// for use the enetery token we need to check his accsepted.
        ///     1. if he is exist
        ///     2. if his time expired
        /// </summary>
        /// <param name="token"></param>
        /// <returns>id of the owner of token</returns>
        public string idByToken(string token)
        {
            foreach (Hash item in hash)
            {
                if (item.Token == token)
                {
                    DateTime now = DateTime.Now;
                    if (item.expired > now.AddHours(-24))
                        return item.Id;
                    else throw new Exception("The token is too old");
                }
            }
            throw new Exception("token not exist");
        }

        /// <summary>
        /// Delete old entery Data.
        /// </summary>
        /// <param name="id"></param>
        private void DeleteHashById(string id)
        {
            List<Hash> hashtemp = new List<Hash>();
            foreach (Hash item in hash)
            {
                if (item.Id != id) hashtemp.Add(item);
            }
            hash = hashtemp;
        }

        public int HashLength()
        {
            return hash.Count;
        }
        private void duplicatePrevent(string id)
        {
            try { placeByIdStudent(id); }
            catch
            {
                try { placeByIdTeacher(id); }
                catch
                {
                    return;
                }
            }
            throw new Exception("duplicate!!!");
        }

        /// <summary>
        /// adding student
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AddStudent(Student name)
        {
            duplicatePrevent(name.Id);
            student.Add(name);
            return true;
        }
        /// <summary>
        /// adding teacher
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AddTeacher(Teacher name)
        {
            duplicatePrevent(name.Id);
            teacher.Add(name);
            return true;

        }

        /// <summary>
        /// adding meet test
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool AddTest(MeetTest name)
        {
            if (name.TestId == -1)
            { name.TestId = def.testId++; }
            meettest.Add(name);
            return true;
        }

        /// <summary>
        /// deleting student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelStudent(string id)
        {
            student.Remove(ShowStudent(id));
            return true;
        }

        /// <summary>
        /// deleting teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelTeacher(string id)
        {
            teacher.Remove(ShowTeacher(id));
            return true;
        }
        /// <summary>
        /// deleting meet test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelTest(string id)
        {
            meettest.Remove(ShowTest(id));
            return true;
        }

        /// <summary>
        /// editing student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool EditStudent(string id, Student name)
        {
            //DelStudent(id);
            //AddStudent(name);
            if (id != name.Id.ToString()) duplicatePrevent(name.Id);
            student[placeByIdStudent(id)] = name;
            return true;
        }

        /// <summary>
        /// editing teacher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool EditTeacher(string id, Teacher name)
        {
            //DelTeacher(id);
            //AddTeacher(name);
            if (id != name.Id.ToString()) duplicatePrevent(name.Id);
            teacher[placeByIdTeacher(id)] = name;
            return true;
        }
        /// <summary>
        /// editing meet test
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool EditTest(string id, MeetTest name)
        {
            //DelTest(id);
            //AddTest(name);
            meettest[placeByIdMeetTest(id)] = name;
            return true;
        }

        /// <summary>
        /// returning number of teachers
        /// </summary>
        /// <returns></returns>
        public int LenghtTeacher()
        {
            return teacher.Count;
        }
        /// <summary>
        /// returning number of meet tests
        /// </summary>
        /// <returns></returns>
        public int LenghtMeetTest()
        {
            return meettest.Count;
        }
        /// <summary>
        /// returning number of students
        /// </summary>
        /// <returns></returns>
        public int LenghtStudent()
        {
            return student.Count;
        }
        /// <summary>
        /// returning the index of a student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int placeByIdStudent(string id)
        {
            var find = from x in student where x.Id == id select x;
            return student.IndexOf(find.First());
        }
        /// <summary>
        /// returning the student with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student ShowStudent(string id)
        {
            return DirectAcsessStudent(placeByIdStudent(id));
        }
        /// <summary>
        /// returning the index of a teacher
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int placeByIdTeacher(string id)
        {
            var find = from x in teacher where x.Id == id select x;
            return teacher.IndexOf(find.First());
        }
        /// <summary>
        /// returning the teacher with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Teacher ShowTeacher(string id)
        {
            return DirectAcsessTeacher(placeByIdTeacher(id));
        }
        /// <summary>
        /// returning the index of a meet test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private int placeByIdMeetTest(string id)
        {
            var find = from x in meettest where x.TestId.ToString() == id select x;
            return meettest.IndexOf(find.First());
        }
        /// <summary>
        /// returning the meet test with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MeetTest ShowTest(string id)
        {
            return DirectAcsessMeetTest(placeByIdMeetTest(id));
        }

        /// <summary>
        /// returning the student in num x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Student DirectAcsessStudent(int x)
        {
            return student[x];
        }

        /// <summary>
        ///  returning the teacher in num x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Teacher DirectAcsessTeacher(int x)
        {
            return teacher[x];
        }

        /// <summary>
        ///  returning the meet test in num x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public MeetTest DirectAcsessMeetTest(int x)
        {
            return meettest[x];
        }

        public int lenghtPermissions()
        {
            return permiss.Count();
        }

        public ListPermission DirectAcsessPermission(int i)
        {
            return permiss[i];
        }

    }
}