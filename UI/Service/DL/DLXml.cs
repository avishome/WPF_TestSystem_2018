using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WCFServiceWebRole1.BE;

namespace UI.Service.DL
{
    public class DLXml
    {
        public DLXml() { }
        public static XElement Root;
        public static void SaveListLinq(List<Student> studentList, List<Teacher> teacherList, List<MeetTest> meetTestsList, List<ListPermission> permissionList, List<Hash> hashList = null)
        {
            XElement studentRoot = new XElement("students",
                   from p in studentList
                   select new
                       XElement("student",
                       new XElement("id", p.Id),
                       new XElement("firstname", p.FirstName),
                       new XElement("lastname", p.LastName),
                       new XElement("phonenumber", p.PhoneNumber),
                       new XElement("gender", p.gender),
                       new XElement("schoolname", p.SchoolName),
                       new XElement("teachername", p.TeacherName),
                       new XElement("hourslearned", p.HoursLearned),
                       new XElement("gruops", from q in p.GroupName select new XElement("group", q)),
                       new XElement("cartype", p.CarType),
                       new XElement("gear", p.Gear),
                       new XElement("birthday", p.BirthDay),
                       new XElement("address",
                            new XElement("city", p.Adress.city),
                            new XElement("street", p.Adress.street),
                            new XElement("number", p.Adress.num)
                            )
                       )
                   );
            XElement TeacherRoot = new XElement("teachers",
                   from t in teacherList
                   select new
                       XElement("teacher",
                       new XElement("id", t.Id),
                       new XElement("firstname", t.FirstName),
                       new XElement("lastname", t.LastName),
                       new XElement("birthday", t.BirthDay),
                       new XElement("phonenumber", t.PhoneNumber),
                       new XElement("maxHoursPerWeek", t.maxHoursPerWeek),
                       new XElement("maxKMFromHome", t.maxKMFromHome),
                       new XElement("carTypeSpecialization", t.carTypeSpecialization),
                       new XElement("Gear", t.Gear),
                       new XElement("gruops", from q in t.GroupName select new XElement("group", q)),
                       new XElement("SeniorityYears", t.SeniorityYears),
                       new XElement("Hours", from q in t.Hours.D2toLinar() select new XElement("hour", q)),
                       new XElement("Address",
                            new XElement("city", t.Adress.city),
                            new XElement("street", t.Adress.street),
                            new XElement("number", t.Adress.num)
                                    )
                                 )
                            );

            XElement meetTestRoot = new XElement("meettests",
                  from m in meetTestsList
                  select new
                      XElement("meettest",
                      new XElement("TestId", m.TestId),
                      new XElement("TeacherId", m.TeacherId),
                      new XElement("StudentId", m.StudentId),
                      new XElement("Time", m.Time),
                      new XElement("DistanceKeeping", m.DistanceKeeping),
                      new XElement("ReverseParking", m.ReverseParking),
                      new XElement("LookingAtMirrors", m.LookingAtMirrors),
                      new XElement("Signaling", m.Signaling),
                      new XElement("TestGrade", m.TestGrade),
                      new XElement("gruops", from q in m.GroupName select new XElement("group", q)),
                      new XElement("TestersNote", m.TestersNote),
                      new XElement("TestAddress",
                           new XElement("city", m.TestAddress.city),
                           new XElement("street", m.TestAddress.street),
                           new XElement("number", m.TestAddress.num)
                                   )
                                )
                           );
            XElement permissionRoot = new XElement("permissions",
                   from r in permissionList
                   select new
                       XElement("permission",
                       new XElement("id", r.Id),
                       new XElement("permissies",
                       from q in r.permisses
                       select new
                            XElement("Biggroup",
                                new XElement("name", q.Name),
                                new XElement("types", from ty in q.types select new XElement("group", ty))
                                )
                            )
                       )
                   );

            /*XElement hashRoot = new XElement("hash",
       from h in hashList
       select new
           XElement("hashes",
           new XElement("id", h.Id),
           new XElement("Token", h.Token),
           new XElement("expired", h.expired)


           )
       );*/
            XElement SerialTest = new XElement("serialTest", def.testId);
            Root = new XElement("root",
                studentRoot, TeacherRoot, meetTestRoot, permissionRoot, SerialTest);


            Root.Save(def.filePath);
        }

        public static void GetSerialTEst() {
            string id = (from stu in LoadData().Descendants("serialTest")
                        select stu.Value).ToList().FirstOrDefault();
            //string id = LoadData().Element("serialTest").Value;
            def.testId = Convert.ToInt32(id);
        }
        public static List<Student> GetStudentList()
        {
            List < Student > students;
            try
            {
                students = (from stu in LoadData().Descendants("student")
                            select new Student()
                            {
                                Id = stu.Element("id").Value,
                                FirstName = stu.Element("firstname").Value,
                                LastName = stu.Element("lastname").Value,
                                PhoneNumber = stu.Element("phonenumber").Value,
                                gender = (Gender)Enum.Parse(typeof(Gender), stu.Element("gender").Value),
                                SchoolName = stu.Element("schoolname").Value,
                                TeacherName = stu.Element("teachername").Value,
                                HoursLearned = int.Parse(stu.Element("hourslearned").Value),
                                GroupName = (from q in stu.Descendants("group") select (string)q).ToList(),
                                CarType = (typecar)Enum.Parse(typeof(typecar), stu.Element("cartype").Value),
                                Gear = (gearbox)Enum.Parse(typeof(gearbox), stu.Element("gear").Value),
                                BirthDay = DateTime.Parse(stu.Element("birthday").Value),
                                Adress = new address(stu.Element("address").Element("street").Value, Convert.ToInt32(stu.Element("address").Element("number").Value), stu.Element("address").Element("city").Value)
                            }).ToList();
            }
            catch
            {
                students = new List<Student>();
            }
            return students;
        }
 

        private static XDocument LoadData()
        {

            XDocument xdoc = null;
            if (!File.Exists(def.filePath))
            {
                Root = new XElement("root");
                Root.Save(def.filePath);
            }
            else if (File.Exists(def.filePath)) {
                xdoc = XDocument.Load(def.filePath);
            }
            return xdoc;
            
        }
        
        public static List<Teacher> GetTeacherList()
        {
            List<Teacher> teachers;
            try
            {
                teachers = (from tea in LoadData().Descendants("teacher")
                            select new Teacher()
                            {
                                Id = tea.Element("id").Value,
                                FirstName = tea.Element("firstname").Value,
                                LastName = tea.Element("lastname").Value,
                                BirthDay = DateTime.Parse(tea.Element("birthday").Value),
                                PhoneNumber = tea.Element("phonenumber").Value,
                                maxHoursPerWeek = int.Parse(tea.Element("maxHoursPerWeek").Value),
                                maxKMFromHome = int.Parse(tea.Element("maxKMFromHome").Value),
                                carTypeSpecialization = (typecar)Enum.Parse(typeof(typecar), tea.Element("carTypeSpecialization").Value),
                                Gear = (gearbox)Enum.Parse(typeof(gearbox), tea.Element("Gear").Value),
                                GroupName = (from q in tea.Descendants("group") select (string)q).ToList(),
                                SeniorityYears = int.Parse(tea.Element("SeniorityYears").Value),
                                Hours = new Schedule((from q in tea.Descendants("hour") select (bool)q).ToArray()),
                                Adress = new address(tea.Element("Address").Element("street").Value, Convert.ToInt32(tea.Element("Address").Element("number").Value), tea.Element("Address").Element("city").Value)
                            }).ToList();
            }
            catch
            {
                teachers = new List<Teacher>() ;
            }
            return teachers;
        }
        public static List<MeetTest> GetMeetTestList()
        {
            List<MeetTest> meetTests;
            try
            {
                meetTests = (from meet in LoadData().Descendants("meettest")
                             select new MeetTest()
                             {
                                 TestId = int.Parse(meet.Element("TestId").Value),
                                 TeacherId = meet.Element("TeacherId").Value,
                                 StudentId = meet.Element("StudentId").Value,
                                 Time = DateTime.Parse(meet.Element("Time").Value),
                                 DistanceKeeping = bool.Parse(meet.Element("DistanceKeeping").Value),
                                 ReverseParking = bool.Parse(meet.Element("ReverseParking").Value),
                                 LookingAtMirrors = bool.Parse(meet.Element("LookingAtMirrors").Value),
                                 Signaling = bool.Parse(meet.Element("Signaling").Value),
                                 TestGrade = int.Parse(meet.Element("TestGrade").Value),
                                 GroupName = (from q in meet.Descendants("group") select (string)q).ToList(),
                                 TestersNote = meet.Element("TestersNote").Value,
                                 TestAddress = new address(meet.Element("TestAddress").Element("street").Value, Convert.ToInt32(meet.Element("TestAddress").Element("number").Value), meet.Element("TestAddress").Element("city").Value)
                             }).ToList();
            }
            catch
            {
                meetTests = new List<MeetTest>();
            }
            return meetTests;
        }
        public static List<ListPermission> GetPermissionList()
        {
            LoadData();
            List<ListPermission> permissions;
            try
            {
                permissions = 
                           (from per in LoadData().Descendants("permission") select new 
                               ListPermission(per.Element("id").Value,
                                                        (from pp in per.Descendants("Biggroup") select new 
                                                                permission(pp.Element("name").Value, 
                                                                        (from tt in pp.Descendants("group") select Convert.ToBoolean(tt.Value)).ToArray()
                                                                )
                                                        ).ToList()
                                              )
                            ).ToList();
            }
            catch
            {
                permissions = new List<ListPermission>();
            }
            return permissions;
        }
    }
}