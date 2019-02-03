using System;
using System.Collections.Generic;

namespace WCFServiceWebRole1.BE
{
    public class Teacher : InterfaceAcsess
    {
        public Teacher() { group = new List<string>(); Hours = new Schedule(); }
        public Teacher(string id, string firstName, string lastNAme,Gender gen, DateTime birthDay, address city, string phoneNumber, typecar carSpecialization, gearbox gear)
        {
            this.Id = id;
            FirstName = firstName;
            LastName = lastNAme;
            BirthDay = birthDay;
            Adress = city;
            PhoneNumber = phoneNumber;
            Hours = Hours;
            gender = gen;
            carTypeSpecialization = carSpecialization;
            Gear = gear;
            Hours = new Schedule();
            group = new List<string>();
        }

        public Teacher Clone()
        {
            Teacher newObj = new Teacher();
            newObj.Id = Id;
            newObj.FirstName = FirstName;
            newObj.LastName = LastName;
            newObj.gender = gender;
            newObj.BirthDay = BirthDay;
            newObj.Adress = Adress;
            newObj.PhoneNumber = PhoneNumber;
            newObj.carTypeSpecialization = carTypeSpecialization;
            newObj.Hours = Hours.clone();
            newObj.Gear = Gear;
            newObj.maxHoursPerWeek = maxHoursPerWeek;
            newObj.maxKMFromHome = maxKMFromHome;
            newObj.SeniorityYears = SeniorityYears;
            if (!(group is null))
                newObj.group.AddRange(group);
            return newObj;
        }

        public Teacher(string id, string firstName, string lastNAme,Gender gen, DateTime birthDay, address city, string phoneNumber,typecar carSpecialization,gearbox gear, Schedule hours)
        {
            this.Id = id;
            FirstName = firstName;
            LastName = lastNAme;
            BirthDay = birthDay;
            Adress = city;
            PhoneNumber = phoneNumber;
            Gear = gear;
            this.Hours = hours;
            carTypeSpecialization =  carSpecialization;
        }

        public Teacher(string id, string firstName, string lastName, DateTime birthDay, address adress, string phoneNumber, int maxHoursPerWeek, int maxKMFromHome, Schedule hours, typecar carTypeSpecialization, gearbox gear, int seniorityYears, Gender gender)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDay = birthDay;
            Adress = adress;
            PhoneNumber = phoneNumber;
            this.maxHoursPerWeek = maxHoursPerWeek;
            this.maxKMFromHome = maxKMFromHome;
            Hours = hours;
            this.carTypeSpecialization = carTypeSpecialization;
            Gear = gear;
            SeniorityYears = seniorityYears;
            this.gender = gender;
            Hours = new Schedule();
            group = new List<string>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public address Adress { get; set; }
        public string PhoneNumber { get; set; }
        public int maxHoursPerWeek { get; set; }
        public int maxKMFromHome { get; set; }
        public Schedule Hours { get; set; }
        public typecar carTypeSpecialization { get; set; }
        public gearbox Gear { get; set; }
        public int SeniorityYears { get; set; }
        public Gender gender { get; set; }

        private List<string> group;
        public List<string> GroupName
        {
            get => group; set { group = value; }
        }
        public override string ToString()
        {
            return FirstName +" "+ LastName+ " " + Id + " phonenumber:"+ PhoneNumber+ " seniority "+SeniorityYears +" years";
        }
    }
}