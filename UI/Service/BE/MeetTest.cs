using System;
using System.Collections.Generic;

namespace WCFServiceWebRole1.BE
{
    public class MeetTest : InterfaceAcsess
    {
        public int TestId { get; set; }
        public string TeacherId { get; set; }
        public string StudentId { get; set; }
        public DateTime Time { get; set; }
        public address TestAddress { get; set; }
        public bool DistanceKeeping { get; set; }
        public bool ReverseParking { get; set; }
        public bool LookingAtMirrors { get; set; }
        public bool Signaling { get; set; }
        public int TestGrade { get; set; }
        public string TestersNote { get; set; }
        public override string ToString()
        {
            return "test: {" + this.TestId + "}, studentId {" + this.StudentId + "}, teacherId: {" + this.TeacherId + "},grade: {" + this.TestGrade + "}, comment: {" + this.TestersNote + "}";
        }

        private List<string> group;

        public MeetTest(int testId, string teacherId, string studentId, DateTime time, address testAddress, bool distanceKeeping, bool reverseParking, bool lookingAtMirrors, bool signaling, int testGrade, string testersNote)
        {
            this.TestId = testId;
            this.TeacherId = teacherId;
            this.StudentId = studentId;
            this.Time = time;
            this.TestAddress = testAddress;
            this.DistanceKeeping = distanceKeeping;
            this.ReverseParking = reverseParking;
            this.LookingAtMirrors = lookingAtMirrors;
            this.Signaling = signaling;
            this.TestGrade = testGrade;
            this.TestersNote = testersNote;

            group = new List<string>();
            //
        }
        public MeetTest()
        {
            group = new List<string>();
        }

        public List<string> GroupName { get => group; set { group = value; } }

        public MeetTest Clone()
        {
            MeetTest newObj = new MeetTest();
            newObj.TestId = this.TestId;
            newObj.TeacherId = TeacherId;
            newObj.StudentId = StudentId;
            newObj.Time = Time;
            newObj.TestAddress = TestAddress;
            newObj.DistanceKeeping = DistanceKeeping;
            newObj.ReverseParking = ReverseParking;
            newObj.LookingAtMirrors = LookingAtMirrors;
            newObj.Signaling = Signaling;
            newObj.TestGrade = TestGrade;
            newObj.TestersNote = TestersNote;
            if (!(group is null))
                newObj.group.AddRange(group);
            return newObj;
        }


    }
}