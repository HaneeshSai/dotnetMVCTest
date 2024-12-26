using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagement.Models
{
    public class MatchedStudentViewModel
    {
        public int Studentid { get; set; }
        public string Fullname { get; set; }
        public int Examid { get; set; }
    }
}