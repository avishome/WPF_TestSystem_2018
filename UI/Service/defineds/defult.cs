using System;
using System.Collections.Generic;
using System.IO;
using WCFServiceWebRole1.BE;
public struct address
{
    public address(string s, int n, string city) { this.city = city; this.num = n; this.street = s; }
    public string street;
    public int num;
    public string city;
    public override String ToString()
    {
        return num + " " + street + " " + city;
    }
}
public class Schedule
{
    public bool[,] S;
    public Schedule()
    {
        S = new bool[5, 9];
        for (int i = 0; i < 5; i++)
            for (int y = 0; y < 9; y++)
                S[i, y] = true;
    }
    public void SetS(int x, int y, bool val)
    {
        if (x < 1 || x > 5 || y < 8 || y > 16) throw new Exception("out of range");
        S[x - 1, y - 8] = val;
    }
    public bool getS(int x, int y)
    {
        if (x < 1 || x > 5 || y < 8 || y > 16) return false;//throw new Exception("out of range");
        return S[x - 1, y - 8];
    }
    public Schedule clone()
    {
        Schedule x = new Schedule();
        for (int i = 0; i < 5; i++)
            for (int y = 0; y < 9; y++)
                x.S[i, y] = S[i, y];
        return x;
    }
    public void LinartoD2(bool[] value)
    {
        for (int i = 0; i < 5; i++) {
            for (int y = 0; y < 9; y++) {
                S[i, y]= value[i * 9 + y];
            }
        }
    }
    public Schedule(bool[] x) { S = new bool[5, 9];  this.LinartoD2(x); }
    public bool[] D2toLinar()
    {
        bool[] tolinar = new bool[45];
        for (int i = 0; i < 5; i++)
        {
            for (int y = 0; y < 9; y++)
            {
                tolinar[i * 9 + y] = S[i, y];
            }
        }
        return tolinar;
    }

    public override string ToString()
    {
        string temp = "";
        for (int i = 0; i < 5; i++)
        {
            for (int y = 0; y < 9; y++)
                temp += S[i, y] + ";";
            temp += "\n";
        }
        return temp;
    }
}
public enum Gender { male, female }
public enum gearbox { automat, manual }
public enum typecar { motorcycle, privateCar, truck, heavyTruck, bus}
public enum op
{
    insert = 0, delete = 1, editPermission = 2, showDetils = 3, createMeetTest = 4,
    editDetils = 5, addGrade = 6, managerInsert = 7, CreateGroupStudents = 8,
    showPremission = 9
}
public static class def
{
    public static Student defstudent = new Student("-1", typecar.bus,gearbox.automat, "", "",Gender.male, new DateTime(2000, 1, 1), new address("", -1, ""), "", "", -1, "");
    public static Teacher defTeacher = new Teacher("-1", "0", "0",Gender.female, new DateTime(2000, 1, 1), new address("", 0, ""), "0", typecar.truck, gearbox.manual);
    public static MeetTest defMeetTest = new MeetTest(-1, "", "", new DateTime(2000, 1, 1,1,1,1), new address("", 0, ""), false, false, false, false, 0, "");
    public static string projPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
    //Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
    public static string filePath = Path.Combine(projPath, "Data.xml");
    public static int testId { get; set; } = 0;
    public static bool[] onlyCreaste = { true, false, false, false, false, false, false, true, false, true };
    public static bool[] NewIdGroup = { true, false, false, true, true, true, false, false, false, true };
    public static bool[] NewIdGroupMangeGroup = { true, false, false, true, true, true, false, false, true, false };
    public static bool[] TeacherForHisTests = { false, false, false, true, false, false, true, false, false, false };
    public static bool[] ALLL = { true, true, true, true, true, true, true, true, true, true };

    public static string[] GroupDefult = { "ALL" };

        public const int maxTestsPerWeek = 8;
        public const int minYearOldTeacher = 40;
        public const int minYearOldStudent = 18;
        public const int minDaysBetweenTests = 7;
        public const int minLecturesBeforeTest = 20;
        public const int minGradeToPassTest = 60;
}