using System;


namespace WCFServiceWebRole1.BE
{
    public class permission
    {
        public permission(string name, bool[] types)
        {
            this.Name = name;
            this.types = types;
        }
        public string Name { get; set; }
        public bool[] types { get; set; }
        public string tostring()
        {
            return Name;
        }
    }
}