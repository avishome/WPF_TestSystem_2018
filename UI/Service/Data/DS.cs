using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFServiceWebRole1.BE;

namespace UI.Service.Data
{
    public class DS
    {
        public List<Student> student;
        public List<Teacher> teacher;
        public List<MeetTest> meettest;
        public List<ListPermission> permiss;
        public List<Hash> hash;
        DS() {
            
            
        }
        public static readonly DS instance = new DS();
    }
}
