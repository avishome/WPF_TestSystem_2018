using System;
using System.Collections.Generic;

namespace WCFServiceWebRole1.BE
{
    public class ListPermission
    {
        public ListPermission(string id, List<permission> permisses)
        {
            this.Id = id;
            this.permisses = permisses;
        }
        public ListPermission(string id, permission permisses)
        {
            this.Id = id;
            this.permisses = new List<permission>();
            this.permisses.Add(permisses);
        }
        public string Id { get; set; }
        public List<permission> permisses { get; set; }
        public string tostring()
        {
            string x = Id + "\n";
            foreach (permission t in permisses) x += t.tostring() + ",";
            return x;

        }
        public override string ToString()
        {
            string x = Id + "\n";
            foreach (permission t in permisses) x += t.tostring() + ",";
            return x;

        }
    }
}