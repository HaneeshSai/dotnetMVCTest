//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentMark
    {
        public int id { get; set; }
        public Nullable<int> studentid { get; set; }
        public Nullable<int> Examid { get; set; }
        public Nullable<int> marks { get; set; }
        public string studentname { get; set; }
    
        public virtual Exam Exam { get; set; }
        public virtual Studentdetail Studentdetail { get; set; }
    }
}
