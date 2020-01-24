using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class Response
    {
        public string Status { get; set; }
        public Exception Err { get; set; }
        public dynamic Data { get; set; }
    }
}