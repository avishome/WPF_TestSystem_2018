using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class Fillter
    {
        static int count = 0;
        public Fillter(string funcName, string prop)
        {
            this.Id = count++;
            this.FuncName = funcName;
            this.Prop = prop;
        }

        public int Id { get; set; }
        public string FuncName { get; set; }
        public string Prop { get; set; }
    }
}
