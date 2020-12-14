using System;

namespace LogManager.Business.Models
{
    public class RequestLog : Entity
    {
        public string Ip { get; set; }
        public DateTime DateTime { get; set; } 
        public string Method { get; set; }
        public string Adress { get; set; }
        public string Client { get; set; }
        public int Status { get; set; }
        public long ContentLength { get; set; }
    }
}