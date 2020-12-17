using System;
using System.ComponentModel.DataAnnotations;

namespace LogManager.Business.Models
{
    public class RequestLog : Entity
    {
        [MaxLength(40)]
        public string Ip { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        [MaxLength(6)]
        public string Method { get; set; }
        [Required]
        [MaxLength(500)]
        public string Adress { get; set; }
        [Required]
        [MaxLength(20)]
        public string UserAgent { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public long ContentLength { get; set; }
    }
}