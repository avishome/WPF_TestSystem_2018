using System;
using System.Collections.Generic;

namespace WCFServiceWebRole1.BE
{

    public class Student : InterfaceAcsess
    {
        public Student(typecar type, gearbox gear ,string firstName, string lastNAme, DateTime birthDay, string city, string phoneNumber, string id)
        {
            this.Id = id;
            CarType = type;
            FirstName = firstName;
            LastName = lastNAme;
            BirthDay = birthDay;
            PhoneNumber = phoneNumber;
            Gear = gear;
            group = new List<string>();
        }
        public Student()
        {
            group = new List<string>();
        }

        public Student(string id, typecar carType,gearbox gear, string firstName, string lastName,Gender gen, DateTime birthDay, address adress, string schoolName, string teacherName, int hoursLearned, string phoneNumber)
        {
            this.Id = id;
            CarType = carType;
            FirstName = firstName;
            LastName = lastName;
            BirthDay = birthDay;
            gender = gen;
            this.Adress = adress;
            SchoolName = schoolName;
            TeacherName = teacherName;
            HoursLearned = hoursLearned;
            PhoneNumber = phoneNumber;
            Gear = gear;
        }
        
        public string Id { get; set; }
        public typecar CarType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public Gender gender { get; set; }
        public address Adress { get; set; }
        public string SchoolName { get; set; }
        public string TeacherName { get; set; }
        public int HoursLearned { get; set; }
        public string PhoneNumber { get; set; }
        public gearbox Gear { get; set; }

        private List<string> group;
        public List<string> GroupName { get => group; set { group = value; } }

        public override string ToString()
        {
            String groups = "";
            foreach (String x in GroupName) { groups += x + ","; }
            return "firstName: {" + this.FirstName + "} LastName: {" + this.LastName + "} id: {" + this.Id + "} permission: {" + groups + ",}";
        }
        public Student Clone()
        {
            Student newObj = new Student();
            newObj.Id = Id;
            newObj.CarType = CarType;
            newObj.FirstName = FirstName;
            newObj.LastName = LastName;
            newObj.BirthDay = BirthDay;
            newObj.Adress = Adress;
            newObj.SchoolName = SchoolName;
            newObj.TeacherName = TeacherName;
            newObj.HoursLearned = HoursLearned;
            newObj.PhoneNumber = PhoneNumber;
            newObj.gender = gender;
            newObj.Gear = Gear;
            if (!(group is null))
                newObj.group.AddRange(group);
            return newObj;
        }

    }

}