using System;
using System.Collections.Generic;
using System.Linq;
using WCFServiceWebRole1.BE;

namespace WCFServiceWebRole1.BL
{
    interface IBL
    {
        void SaveData();
        bool AddStudent(string Hash, Student name, bool confirmSend = true);
        bool DelStudent(string Hash, string id);
        bool EditStudent(string Hash, string id, Student name, bool confirmSend = true);
        Student ShowStudent(string Hash, string id, bool SafeMode = true);

        bool AddTeacher(string Hash, Teacher name, bool confirmSend = true);
        bool DelTeacher(string Hash, string id);
        bool EditTeacher(string Hash, string id, Teacher name, bool confirmSend = true);
        Teacher ShowTeacher(string Hash, string id, bool SafeMode = true);

        bool AddMeetTest(string Hash, MeetTest name, bool confirmSend = true);
        bool DelTest(string Hash, string id);
        bool EditTest(string Hash, string id, MeetTest name, bool confirmSend = true);
        MeetTest ShowTest(string Hash, string id, bool SafeMode = true);

        List<MeetTest> GetListMeetTests(string Hash);
        List<Teacher> GetListTeachers(string Hash);
        List<Student> GetListStudents(string Hash);
        List<ListPermission> GetListUsers(string Hash);

        bool ReplacePermission(string Hash, List<permission> x, string id);
        ListPermission ShowPermission(string Hash, string id);
        List<string> FindGroups(string Hash);

        Teacher getTeacherForTest(MeetTest m);
        
        //checking

        void TeacherTooYoung(Teacher x);
        void StudentTooYoung(Student s);
        void MeetingTooFrequent(MeetTest m);
        void TooLessLectures(Student s);
        bool IsTimeOpenForTeacher(DateTime t, Teacher x);
        bool IsThereATestinTheHour(DateTime t, Teacher x,int testId);
        List<Teacher> CheckIfTimeOfTestIsOpen(DateTime t,int testId);
        bool TooMuchTestsInWeek(Teacher t);
        void DuplicateTest(MeetTest m);
        bool CheckIfSameTypeOfCar(Teacher t, Student s);
        void DidPassATest(Student stu);
        IEnumerable<IGrouping<typecar, Teacher>> TeachersAcorrdingToSpecilazation(bool sorted = false);
        IEnumerable<IGrouping<string, Student>> StudentsAcorrdingToSchool(bool sorted = false);
        IEnumerable<IGrouping<string, Student>> StudentsAcorrdingToTeacher(bool sorted = false);


    }
}
