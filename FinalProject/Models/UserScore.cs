using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class UserScore
    {
        public int UserId { get; set; }
        public int SubId { get; set; }
        public int CntCorrectAns { get; set; }
    }
}