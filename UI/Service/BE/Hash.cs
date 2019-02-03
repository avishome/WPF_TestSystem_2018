using System;


namespace WCFServiceWebRole1.BE
{
    public class Hash
    {
        public Hash(string id, string Token, DateTime expired)
        {
            this.Id = id;
            this.Token = Token;
            this.expired = expired;
        }
        public String Id { get; set; }
        public string Token { get; set; }
        public DateTime expired { get; set; }
    }
}