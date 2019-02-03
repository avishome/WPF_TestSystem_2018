using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WCFServiceWebRole1.BE;
using WCFServiceWebRole1.DL;

namespace WCFServiceWebRole1.BL
{
    public class Bl_imp : IBL
    {
        private Dal_imp Dl = Dal_imp.Instance;
        public static Bl_imp Instance
        {
            get {
                if (privateinstance is null)
                    privateinstance = new Bl_imp();
                return privateinstance;
            }
        }
        private static Bl_imp privateinstance = null;
        private Bl_imp() { }
        /// <summary>
        /// data to xml
        /// </summary>
        public void SaveData() {
            try
            {
                UI.Service.DL.DLXml.SaveListLinq(StudentList(), TeacherList(), MeetTestList(), GetListUsers(""));
            }
            catch {  }
        }
        public string AddToken(string id) { return Dl.AddToken(id).Token; }
        /// <summary>
        /// adding student
        /// </summary>
        /// <param name="Hash"></param>
        /// <param name="name"></param>
        /// <param name="confirmSend"></param>
        /// <returns></returns>
        public bool AddStudent(string Hash, Student name, bool confirmSend = true)
        {
            return AddEditStudent(Hash, name, confirmSend);
        }

        public bool AddTeacher(string Hash, Teacher name, bool confirmSend = true)
        {
            return AddEditTeacher(Hash, name, confirmSend);
        }

        public bool AddMeetTest(string Hash, MeetTest name, bool confirmSend = true)
        {
            return MeetTestAddEdit(Hash, name, confirmSend, null);
        }

        private bool AddEditStudent(string Hash, Student name, bool confirmSend = true, Student oldItem = null, bool SafeMode = true)
        {
            Dictionary<Exception, string> Problems = new Dictionary<Exception, string>();
            Student newObj;
            if (oldItem is null)
            {
                newObj = def.defstudent.Clone();
                foreach (string x in def.GroupDefult) { AddToGroup(newObj, x); }
                try { Security.HashToAcsess(Hash, (int)op.CreateGroupStudents, newObj.GroupName); AddToGroup(newObj, Dl.idByToken(Hash)); } catch { }
            }
            else { newObj = oldItem.Clone(); }
            AddToGroup(newObj, name.Id);
            try // for Student
            {
                if (SafeMode) Security.HashToAcsess(Hash, (int)op.insert, newObj.GroupName);
                newObj.Id = name.Id;
                newObj.CarType = name.CarType;
                newObj.FirstName = name.FirstName;
                newObj.LastName = name.LastName;
                newObj.BirthDay = name.BirthDay;
                newObj.Adress = name.Adress;
                newObj.SchoolName = name.SchoolName;
                newObj.TeacherName = name.TeacherName;
                newObj.HoursLearned = name.HoursLearned;
                newObj.PhoneNumber = name.PhoneNumber;
                newObj.Gear = name.Gear;
                newObj.gender = name.gender;
                //File.AppendAllText(def.filePath, Dl.idByToken(Hash) + "op.insert(Student) - " + newObj.ToString() + Environment.NewLine);
            }
            catch {
                if (oldItem is null) {
                    Problems.Add(new Exception("you can't create Studet for this Id"), "Id");
                    throw new UI.Service.defineds.ex(Problems);
                }
            }

            try // for premmiery acsess
            {
                if (SafeMode) Security.HashToAcsess(Hash, (int)op.editPermission, newObj.GroupName);
                foreach (string s in name.GroupName)
                {
                    AddToGroup(newObj, s);
                }
                //File.AppendAllText(def.filePath, "op.editPermisson - " + newObj.ToString() + Environment.NewLine);
            }
            catch { }



            if (newObj.Id == def.defstudent.Id || newObj.Id == "") Problems.Add(new Exception("requerd"), "Id");
            if (newObj.FirstName == def.defstudent.FirstName) Problems.Add(new Exception("requerd"), "FirstName");
            if (newObj.LastName == def.defstudent.LastName) Problems.Add(new Exception("requerd"), "LastName");
            if (newObj.PhoneNumber == def.defstudent.PhoneNumber) Problems.Add(new Exception("requerd"), "PhoneNumber");
            if (newObj.BirthDay == def.defstudent.BirthDay) Problems.Add(new Exception("requerd"), "BirthDay");
            if (newObj.Adress.city == def.defstudent.Adress.city) Problems.Add(new Exception("requerd"), "Adress.city");
            if (newObj.Adress.street == def.defstudent.Adress.street) Problems.Add(new Exception("requerd"), "Adress.street");
            if (newObj.Adress.num == def.defstudent.Adress.num) Problems.Add(new Exception("requerd"), "Adress.num");
            if (newObj.TeacherName == def.defstudent.TeacherName) Problems.Add(new Exception("requerd"), "TeacherName");
            if (newObj.SchoolName == def.defstudent.SchoolName) Problems.Add(new Exception("requerd"), "SchoolName");
            if (newObj.HoursLearned == def.defstudent.HoursLearned) Problems.Add(new Exception("requerd"), "HoursLearned");
            try { StudentTooYoung(newObj); } catch(Exception e) { Problems.Add(e, "BirthDay");}

            if (Problems.Count > 0) confirmSend = false;

            try
            {
                if (confirmSend)
                {
                    //inset (if the corent teacher is unalbe) teacher - function
                    if (!(oldItem is null))
                    {
                        if (newObj.Id != oldItem.Id) newObj.GroupName.Remove(oldItem.Id);
                        return Dl.EditStudent(oldItem.Id.ToString(), newObj);
                    }
                    return Dl.AddStudent(newObj);
                }
            }
            catch(Exception e) { Problems.Add(e, "Id"); }

            if (Problems.Count > 0) throw new UI.Service.defineds.ex(Problems);
            return false;
        }

        private bool AddEditTeacher(string Hash, Teacher name, bool confirmSend = true, Teacher oldItem = null, bool SafeMode = true)
        {
            Dictionary<Exception, string> Problems = new Dictionary<Exception, string>();
            Teacher newObj;
            if (oldItem is null)
            {
                newObj = def.defTeacher.Clone();
                foreach (string x in def.GroupDefult) { AddToGroup(newObj, x); }
            }
            else { newObj = oldItem.Clone(); }
            try // for Insert
            {
                if (SafeMode) Security.HashToAcsess(Hash, (int)op.insert, newObj.GroupName);
                newObj.Id = name.Id;
                newObj.FirstName = name.FirstName;
                newObj.LastName = name.LastName;
                newObj.BirthDay = name.BirthDay;
                newObj.Gear = name.Gear;
                newObj.Adress = name.Adress;
                newObj.Hours = name.Hours.clone();
                newObj.PhoneNumber = name.PhoneNumber;
                newObj.gender = name.gender;
                newObj.maxHoursPerWeek = name.maxHoursPerWeek;
                newObj.maxKMFromHome = name.maxKMFromHome;
                newObj.SeniorityYears = name.SeniorityYears;
                newObj.carTypeSpecialization = name.carTypeSpecialization;
                //File.AppendAllText(def.filePath, "op.insert(Teacher) - " + newObj.ToString() + Environment.NewLine);
            }
            catch {
                if(oldItem is null)
                    Problems.Add(new Exception("you can't create Teacher for this Id"), "Id");
                    throw new UI.Service.defineds.ex(Problems);
            }
            try // for Edit
            {
                if (SafeMode) Security.HashToAcsess(Hash, (int)op.editDetils, newObj.GroupName);
                newObj.FirstName = name.FirstName;
                newObj.LastName = name.LastName;
                newObj.BirthDay = name.BirthDay;
                newObj.Adress = name.Adress;
                newObj.Gear = name.Gear;
                newObj.Hours = name.Hours.clone();
                newObj.gender = name.gender;
                newObj.maxHoursPerWeek = name.maxHoursPerWeek;
                newObj.maxKMFromHome = name.maxKMFromHome;
                newObj.SeniorityYears = name.SeniorityYears;
                newObj.PhoneNumber = name.PhoneNumber;
                //File.AppendAllText(def.filePath, "op.Edit(Teacher) - " + newObj.ToString() + Environment.NewLine);
            }
            catch { }

            if (newObj.Id == def.defstudent.Id) Problems.Add(new Exception("requerd"), "Id");
            if (newObj.Adress.city == def.defstudent.Adress.city) Problems.Add(new Exception("requerd"), "Adress.city");
            try { TeacherTooYoung(newObj); } catch (Exception e) { Problems.Add(e, "BirthDay"); }

            if (Problems.Count > 0) confirmSend = false;
            if (Problems.Count > 0) throw new UI.Service.defineds.ex(Problems);

            AddToGroup(newObj, newObj.Id);

            if (confirmSend)
            {
                //inset (if the corent teacher is unalbe) teacher - function
                if (!(oldItem is null))
                {
                    if (newObj.Id != oldItem.Id) newObj.GroupName.Remove(oldItem.Id);
                    return Dl.EditTeacher(oldItem.Id.ToString(), newObj);
                }
                return Dl.AddTeacher(newObj);
            }

            return false;
        }

        private bool MeetTestAddEdit(string Hash, MeetTest name, bool confirmSend, MeetTest oldItem, bool SafeMode = true)
        {
            //oldItem is copy from DB
            //name is form from user
            //newObj is the object to add DB
            MeetTest newObj;
            if (oldItem is null)
            {
                newObj = def.defMeetTest.Clone();
                foreach (string x in def.GroupDefult) { AddToGroup(newObj, x); }
            }
            else { newObj = oldItem.Clone();  }
            AddToGroup(newObj, name.StudentId);
            
            //GroupManager(Hash, name, SafeMode, newObj);

            try // For teachers
            {
                if (SafeMode) Security.HashToAcsess(Hash, (int)op.addGrade, newObj.GroupName);
                newObj.StudentId = oldItem.StudentId;
                newObj.TestGrade = name.TestGrade;
                newObj.TestersNote = name.TestersNote;
                newObj.Signaling = name.Signaling;
                newObj.ReverseParking = name.ReverseParking;
                newObj.LookingAtMirrors = name.LookingAtMirrors;
                newObj.DistanceKeeping = name.DistanceKeeping;
                //File.AppendAllText(def.filePath, "op.AddGrade - " + newObj.ToString() + Environment.NewLine);
            }
            catch { }

            try // for Students
            {
                if (SafeMode)
                    if (oldItem is null) 
                        Security.HashToAcsess(Hash, (int)op.createMeetTest, newObj.GroupName);
                        else
                        Security.HashToAcsess(Hash, (int)op.editDetils, newObj.GroupName);
                if (!(oldItem is null) && oldItem.Time < DateTime.Now) throw new Exception("time Passed");
                newObj.StudentId = name.StudentId;
                newObj.TestAddress = name.TestAddress;
                newObj.Time = name.Time; 
            }
            catch { }


            Dictionary<Exception, string> Problems = new Dictionary<Exception, string>();
            if (!(oldItem is null) && name.Time != oldItem.Time && name.Time < DateTime.Now) Problems.Add(new Exception("can't update test that its time passed"), "Time");
            if(oldItem is null && name.Time < DateTime.Now) Problems.Add(new Exception("can't create test that its time passed"), "Time");
            //if (newObj.TeacherId == def.defMeetTest.TeacherId) { Problems.Add(new Exception("Teacher not find"), "TeacherId"); }
            if (newObj.TestAddress.city == def.defstudent.Adress.city || newObj.TestAddress.city == "") Problems.Add(new Exception("city is empety"), "TestAddress.city");
            if (newObj.TestAddress.street == def.defstudent.Adress.street || newObj.TestAddress.street == "") Problems.Add(new Exception("street is empety"), "TestAddress.street");
            //for avoid checks when the theacher only add grade
            if((!(oldItem is null) && oldItem.StudentId != newObj.StudentId) || oldItem is null)
            try
            {
                ShowStudent(Hash, newObj.StudentId);
                try
                {
                    TooLessLectures(ShowStudent(Hash, newObj.StudentId));
                }
                catch (Exception e) { Problems.Add(e, "StudentId"); }

                try
                {
                    DidPassATest(ShowStudent(Hash, newObj.StudentId));
                }
                catch (Exception e) { Problems.Add(e, "StudentId"); }

                try
                {
                    MeetingTooFrequent(newObj);
                }
                catch (Exception e) { Problems.Add(e, "Time"); }

            }
            catch { Problems.Add(new Exception("Student not exist"), "StudentId"); }

            if (!(oldItem is null) &&
                (
                newObj.TestAddress.ToString() != oldItem.TestAddress.ToString() ||
                newObj.StudentId != oldItem.StudentId ||
                newObj.Time != oldItem.Time
                )
               ) {
                newObj.GroupName.Remove(newObj.TeacherId);
                newObj.TeacherId = def.defMeetTest.TeacherId;
                }

           
            if (Problems.Count > 0) confirmSend = false;
            if (Problems.Count > 0) throw new UI.Service.defineds.ex(Problems);
            if (confirmSend)
            {
                if (newObj.TeacherId == def.defMeetTest.TeacherId)
                {
                    try { newObj.TeacherId = getTeacherForTest(newObj).Id; AddToGroup(newObj, newObj.TeacherId); }
                    catch (Exception e) { Problems.Add(e, "Time"); throw new UI.Service.defineds.ex(Problems); }
                }
                //inset (if the corent teacher is unalbe) teacher - function
                if (!(oldItem is null))
                {
                    return Dl.EditTest(oldItem.TestId.ToString(), newObj);
                }
                return Dl.AddTest(newObj);
            }

            return false;
        }

        private void GroupManager(string Hash, MeetTest name, bool SafeMode, MeetTest newObj)
        {
            try // for premmiery acsess
            {
                if (SafeMode) Security.HashToAcsess(Hash, (int)op.editPermission, newObj.GroupName);
                foreach (string s in name.GroupName)
                {
                    AddToGroup(newObj, s);
                }
            }
            catch { }
        }

        public bool DelStudent(string Hash, string id)
        {
            try
            {
                Student x = ShowStudent(Hash, id, false);
                Security.HashToAcsess(Hash, (int)op.delete, x.GroupName);
                return Dl.DelStudent(id);
            }
            catch { }
            return false;
        }

        public bool DelTeacher(string Hash, string id)
        {
            try
            {
                Teacher x = ShowTeacher(Hash, id, false);
                Security.HashToAcsess(Hash, (int)op.delete, x.GroupName);
                return Dl.DelTeacher(id);
            }
            catch { }
            return false;
        }

        public bool DelTest(string Hash, string id)
        {
            try
            {
                MeetTest x = ShowTest(Hash, id, false);
                Security.HashToAcsess(Hash, (int)op.delete, x.GroupName);
                return Dl.DelTest(id);
            }
            catch { }
            return false;
        }


        public bool EditStudent(string Hash, string id, Student name, bool confirmSend = true)
        {
            Student x = ShowStudent(Hash, id, false); // if you can edit you get acsess to show
            try
            {
                Security.HashToAcsess(Hash, (int)op.editDetils, x.GroupName);
                return AddEditStudent(Hash, name, confirmSend, x, true);
            }
            catch (Exception e) { if (e is UI.Service.defineds.ex) throw e; }
            try
            {
                Security.HashToAcsess(Hash, (int)op.editPermission, x.GroupName);
                return AddEditStudent(Hash, name, confirmSend, x, true);
            }
            catch (Exception e) { if (e is UI.Service.defineds.ex) throw e; }
            return false;
        }

        public bool EditTeacher(string Hash, string id, Teacher name, bool confirmSend = true)
        {
            Teacher x = ShowTeacher(Hash, id, false); // if you can edit you get acsess to show
            try
            {
                Security.HashToAcsess(Hash, (int)op.editDetils, x.GroupName);
                return AddEditTeacher(Hash, name, confirmSend, x, true);
            }
            catch (Exception e) { if (e is UI.Service.defineds.ex) throw e; }
            try
            {
                Security.HashToAcsess(Hash, (int)op.editPermission, x.GroupName);
                return AddEditTeacher(Hash, name, confirmSend, x, true);
            }
            catch (Exception e) { if (e is UI.Service.defineds.ex) throw e; }
            return false;
        }

        public bool EditTest(string Hash, string id, MeetTest name, bool confirmSend = true)
        {
            MeetTest x = ShowTest(Hash, id, false); // if you can edit you get acsess to show
            try
            {
                Security.HashToAcsess(Hash, (int)op.editDetils, x.GroupName);
                return MeetTestAddEdit(Hash, name, confirmSend, x, true);
            }
            catch (Exception e) { if (e is UI.Service.defineds.ex) throw e; }
            try
            {
                Security.HashToAcsess(Hash, (int)op.addGrade, x.GroupName);
                return MeetTestAddEdit(Hash, name, confirmSend, x, true);
            }
            catch(Exception e) { if (e is UI.Service.defineds.ex) throw e; }
            return false;
        }


        public Student ShowStudent(string Hash, string id, bool SafeMode = true)
        {
            Student x = Dl.ShowStudent(id);
            if (SafeMode) Security.HashToAcsess(Hash, (int)op.showDetils, x.GroupName);
            return x;
        }

        public Teacher ShowTeacher(string Hash, string id, bool SafeMode = true)
        {
            Teacher x = Dl.ShowTeacher(id);
            if (SafeMode) Security.HashToAcsess(Hash, (int)op.showDetils, x.GroupName);
            return x;
        }

        public MeetTest ShowTest(string Hash, string id, bool SafeMode = true)
        {
            MeetTest x = Dl.ShowTest(id);
            if (SafeMode) Security.HashToAcsess(Hash, (int)op.showDetils, x.GroupName);
            return x;

        }


        public List<Student> GetListStudents(string Hash)
        {
            return StudentList(Hash,true);
        }


        private List<Student> StudentList(string Hash="", bool safeMode = false)
        {
            int lenght = Dl.LenghtStudent();
            List<Student> NewList = new List<Student>();
            for (int i = 0; i < lenght; i++)
            {
                try { NewList.Add(ShowStudent(Hash, Dl.DirectAcsessStudent(i).Id, safeMode)); }
                catch { }
            }
            return NewList;
        }


        public List<Teacher> GetListTeachers(string Hash)
        {
            return TeacherList(Hash,true);
        }


        private List<Teacher> TeacherList(string Hash="", bool safeMode = false)
        {
            int lenght = Dl.LenghtTeacher();
            List<Teacher> NewList = new List<Teacher>();
            for (int i = 0; i < lenght; i++)
            {
                try { NewList.Add(ShowTeacher(Hash, Dl.DirectAcsessTeacher(i).Id, safeMode)); }
                catch { }
            }
            return NewList;
        }


        public List<MeetTest> GetListMeetTests(string Hash)
        {
            return MeetTestList(Hash,true);
        }


        private List<MeetTest> MeetTestList(string Hash="", bool safeMode = false)
        {
            int lenght = Dl.LenghtMeetTest();
            List<MeetTest> NewList = new List<MeetTest>();
            for (int i = 0; i < lenght; i++)
            {
                try { NewList.Add(ShowTest(Hash, Dl.DirectAcsessMeetTest(i).TestId.ToString(),safeMode)); }
                catch { }
            }
            return NewList;
        }


        public List<ListPermission> GetListUsers(string Hash)
        {
            int lenght = Dl.lenghtPermissions();
            List<ListPermission> NewList = new List<ListPermission>();
            for (int i = 0; i < lenght; i++)
            {
                try { NewList.Add(ShowPermission(Hash, Dl.DirectAcsessPermission(i).Id)); }
                catch { }
            }
            return NewList;
        }
        //for Permission system

        public bool ReplacePermission(string Hash, List<permission> x, string id)
        {
            Dl.ListpermissById(id).permisses = x;
            return true;
        }

        public ListPermission ShowPermission(string Hash, string id)
        {
            return Dl.ListpermissById(id);
        }
        public List<string> FindGroups(string Hash)
        {
           List<string> grop = new List<string>();
                foreach (InterfaceAcsess x in StudentList(Hash))
                    foreach (string p in x.GroupName)
                        if (!grop.Exists(t => t == p)) grop.Add(p);
                foreach (InterfaceAcsess x in TeacherList(Hash))
                    foreach (string p in x.GroupName)
                        if (!grop.Exists(t => t == p)) grop.Add(p);
                foreach (InterfaceAcsess x in MeetTestList(Hash))
                    foreach (string p in x.GroupName)
                        if (!grop.Exists(t => t == p)) grop.Add(p);
                return grop;
        }
        private bool AddToGroup(InterfaceAcsess obj, string str)
        {
            bool t = true;
            foreach (string x in obj.GroupName)
                if (x == str) t = false;
            if(t)obj.GroupName.Add(str);
            return t;
        }

        /// <summary>
        /// returning the distance between A and B
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        private int getdistance(string A, string B) {
            return UI.Service.Data.MenegeDistance.Distance(A, B);
        }
        /// <summary>
        /// finding a suitable teacher to the test
        /// </summary>
        /// <param name="meet test"></param>
        /// <returns></returns>
        public Teacher getTeacherForTest(MeetTest m)
        {
            if (m.Time < DateTime.Now) throw new Exception("time passed.");
            List<Teacher> SortedTeacher;
            List<Teacher> teacher = CheckIfTimeOfTestIsOpen(m.Time,m.TestId);
            if(teacher.Count()==0) throw new Exception("no teacher in this time");
            var teacher2 = from Teacher Item in teacher where CheckIfSameTypeOfCar(Item, ShowStudent("", m.StudentId,false)) select Item.Clone();
            if (teacher2.Count() == 0) throw new Exception("there are "+ teacher.Count() + " teacher in this time But your type car dont suport");
            var teacher3 = from Teacher Item in teacher2 where getdistance(Item.Adress.ToString(), m.TestAddress.ToString()) < Item.maxKMFromHome select Item.Clone();
            if (teacher3.Count() == 0) throw new Exception("there are " + teacher.Count() + " teacher but your place too far");
            if (teacher3.Count() > 1)
                SortedTeacher = teacher3.OrderBy(it => getdistance(it.Adress.ToString(), m.TestAddress.ToString())).ToList();
            else
                SortedTeacher = teacher3.ToList();
            if (SortedTeacher.Count() > 0) return SortedTeacher.FirstOrDefault();
            else throw new Exception("no teacher in according requirements");
        }

        //logic
        /// <summary>
        /// checking if the techer is too young
        /// </summary>
        /// <param name="teacher"></param>
        public void TeacherTooYoung(Teacher x)
        {
            if (x.BirthDay.AddYears(def.minYearOldTeacher) > DateTime.Now) throw new Exception ("Teacher can't be less then " + def.minYearOldTeacher + " years old.");
        }
        /// <summary>
        /// checking if the student is too young
        /// </summary>
        /// <param name="student"></param>
        public void StudentTooYoung(Student s)
        {
            if (s.BirthDay.AddYears(def.minYearOldStudent) > DateTime.Now) throw new Exception ("Student can't be less then " + def.minYearOldStudent + ".");
        }
        /// <summary>
        /// checking if the meeting is too frequent srom the last one
        /// </summary>
        /// <param name="meettest"></param>
        public void MeetingTooFrequent(MeetTest m)
        {
            var lists = from MeetTest Item in MeetTestList() where Item.Time.AddDays(def.minDaysBetweenTests) > m.Time && Item.Time.AddDays(-def.minDaysBetweenTests) < m.Time && Item.StudentId == m.StudentId select Item;
            if ((lists.Count() > 0)&&(lists.First().TestId!=m.TestId)) throw new Exception("meeting can't be less then " + def.minDaysBetweenTests+ " days from the last meeting");
        }
        /// <summary>
        /// checking if student did inaf lecturers
        /// </summary>
        /// <param name="student"></param>
        public void TooLessLectures(Student s)
        {
            if (s.HoursLearned < def.minLecturesBeforeTest) throw new Exception("can't determine a test while less then " + def.minLecturesBeforeTest + " lectures learned.");
        }
        /// <summary>
        /// checking if teacher has the time open
        /// </summary>
        /// <param name="time"></param>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public bool IsTimeOpenForTeacher(DateTime t, Teacher x)
        {
            try { return x.Hours.getS((int)(t.DayOfWeek), t.Hour); }
            catch {return false; }
        }
        /// <summary>
        /// checking if teacher has a test in the hour
        /// </summary>
        /// <param name="time"></param>
        /// <param name="teacher"></param>
        /// <returns></returns>
        public bool IsThereATestinTheHour(DateTime t, Teacher x, int testId)
        {
            MeetTest ifThere = (from MeetTest Item in MeetTestList() where Item.TeacherId == x.Id && Item.Time == t select Item).FirstOrDefault();
            //foreach (MeetTest meet in lists) if (meet.Time == t) return false;
            if (!(ifThere is null) && ifThere.TestId != testId) return false;
            return true;
        }
        /// <summary>
        /// checking if the time is open for a test
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public List<Teacher> CheckIfTimeOfTestIsOpen(DateTime t, int testId)
        {
            var lists = from Teacher Item in TeacherList() where (IsThereATestinTheHour(t, ShowTeacher("", Item.Id,false),testId) && IsTimeOpenForTeacher(t, ShowTeacher("", Item.Id,false))) select ShowTeacher("", Item.Id,false);
            return lists.ToList();
        }

        /// <summary>
        /// checking if the number of tests in the week is larger then allowed
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool TooMuchTestsInWeek (Teacher t)
        {
            var lists = from MeetTest Item in MeetTestList() where (Item.TeacherId == t.Id) && (Item.Time > (DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek))))  && (Item.Time < (DateTime.Now.AddDays(-(int)(DateTime.Now.DayOfWeek)+7))) select Item;
            return (lists.Count() > def.maxTestsPerWeek)||(lists.Count()> t.maxHoursPerWeek);
        }
        /// <summary>
        /// checking if the test is duplicate whith another one
        /// </summary>
        /// <param name="meettest"></param>
        public void DuplicateTest (MeetTest m)
        {
            var lists = from MeetTest Item in MeetTestList() where (Item.Time == m.Time) && (Item.TeacherId == m.TeacherId) select Item;
            if ((lists.Count()>0)&&(m.TestId!=lists.First().TestId)) throw new Exception("duplicate test error.");
        }
        /// <summary>
        /// checking if the student and teacher are working with the same type of car
        /// </summary>
        /// <param name="teacher"></param>
        /// <param name="stusent"></param>
        /// <returns></returns>
        public bool CheckIfSameTypeOfCar(Teacher t, Student s)
        {
            if ((t.carTypeSpecialization != s.CarType)) return false; return true;
                //throw new Exception("Sorry. The teacher has no expertise in this vehicle.");
        }
        /// <summary>
        /// checking if student already passed a test
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public void DidPassATest(Student stu)
        {
            var lists = from MeetTest Item in MeetTestList() where (Item.StudentId == stu.Id) && (Item.TestGrade>def.minGradeToPassTest) select Item;
            if (lists.Count() > 0) throw new Exception ("the student already passed a test");
        }

        /*public bool IsTestPassed(string id, string type)
        {
           if (s.TestPassed== 1) throw new Exception("Sorry. Test already passed");
        }
        public bool IsWaitToTest(string id, string type)
        {
            if (s.TestPassed == 1) throw new Exception("Sorry. Test already passed");
        }*/
        ////prototypes
        //public List<Teacher> TeachersInTheArea(address here, int XKm){}
        //public List<Teacher> TeachersUnoccupiedInTime(DateTime t){}
        //public int HowmuchTestDid(Student s){}

        /// <summary>
        /// grouping Teachers acorrding to specilazation
        /// </summary>
        /// <param name="sorted"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<typecar, Teacher>> TeachersAcorrdingToSpecilazation(bool sorted=false)
        {
            var lists = from Teacher in TeacherList() group Teacher by Teacher.carTypeSpecialization;
            if (sorted) lists.OrderBy(T => T.Key);
            return lists;
        }
        /// <summary>
        /// grouping students acorrding to school
        /// </summary>
        /// <param name="sorted"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, Student>> StudentsAcorrdingToSchool(bool sorted = false)
        {
            var lists = from Student in StudentList() group Student by Student.SchoolName;
            if (sorted) lists.OrderBy(T => T.Key);
            return lists;
        }
        /// <summary>
        /// grouping students acorrding to teacher
        /// </summary>
        /// <param name="sorted"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, Student>> StudentsAcorrdingToTeacher(bool sorted = false)
        {
            var lists = from Student in StudentList() group Student by Student.TeacherName;
            if (sorted) lists.OrderBy(T => T.Key);
            return lists;
        }
        //public IEnumerable<IGrouping<int, Student>> StudentsAcorrdingToTestNum(bool sorted = false)
        //{
        //    var lists = from Student in StudentList() group Student by ??????;
        //    return lists;
        //    if (sorted) lists.OrderBy(T => T.Key);
       //}
    }
}