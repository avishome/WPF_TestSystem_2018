using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Service.defineds
{
    public class ex : Exception
    {
        public Dictionary<Exception, string> Problems;
        public ex(Dictionary<Exception, string> Problem) {
            Problems = new Dictionary<Exception, string>(Problem);
        }
    }
}
