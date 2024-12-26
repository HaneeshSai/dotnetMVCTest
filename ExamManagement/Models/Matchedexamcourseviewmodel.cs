using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagement.Models
{
    public class Matchedexamcourseviewmodel
    {
        public int Courseid { get; set; }
        public string Coursename { get; set; }
        public int Examid { get; set; }

        public int Duration { get; set; }
    }
}