using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class FeedbackUsers
    {
        public T_Feedback feedback { get; set; }
        public T_Users user { get; set; }
    }
}