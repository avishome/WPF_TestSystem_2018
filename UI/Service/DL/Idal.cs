using System;
using WCFServiceWebRole1.BE;
namespace WCFServiceWebRole1.DL
{
    interface Idal
    {
        ListPermission ListpermissById(string id);
        string idByToken(string token);
        Hash AddToken(string id);
        bool AddStudent(Student name);
        bool DelStudent(string id);
        bool EditStudent(string id, Student name);
        Student ShowStudent(string id);
        Student DirectAcsessStudent(int x);
        int LenghtStudent();

        bool AddTeacher(Teacher name);
        bool DelTeacher(string id);
        bool EditTeacher(string id, Teacher name);
        Teacher ShowTeacher(string id);
        Teacher DirectAcsessTeacher(int x);
        int LenghtTeacher();

        bool AddTest(MeetTest name);
        bool DelTest(string id);
        bool EditTest(string id, MeetTest name);
        MeetTest ShowTest(string id);
        MeetTest DirectAcsessMeetTest(int x);
        int LenghtMeetTest();
    }
}