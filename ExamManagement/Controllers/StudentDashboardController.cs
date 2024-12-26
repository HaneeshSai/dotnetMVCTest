using ExamManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagement.Controllers
{
    public class StudentDashboardController : Controller
    {
        // GET: StudentDashboard
        public ActionResult Index()
        {
            using (ExammanagefinalEntities db = new ExammanagefinalEntities())
            {
                var studentid= Convert.ToInt32(Session["Studentid"]);
                var obj = db.StudentMarks.Where(a => a.studentid == studentid ).Count();
                var obj1 = db.StudentExamApplications.Where(a => a.studentid == studentid).Count();
                var ob2 = db.Courses.Count();
                int totalcourses = db.Courses.Count();
                ViewBag.TotalStudents = obj;
                ViewBag.TotalExams = ob2;

                return View();
            }
        }
    }
}