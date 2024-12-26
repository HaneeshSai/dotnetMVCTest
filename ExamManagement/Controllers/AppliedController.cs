using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamManagement.Models;
using  System.Diagnostics;

namespace ExamManagement.Controllers
{
    public class AppliedController : Controller
    {
        // GET: Applied
        public ActionResult Index(string searchId)
        {
            using (var db = new ExammanagefinalEntities())
            {
                var studentDetails = db.Studentdetails.ToList();
                var examApplications = db.StudentExamApplications.ToList();

                var matchedData = (from t1 in studentDetails
                                   join t2 in examApplications on t1.studentid equals t2.studentid
                                   select new MatchedStudentViewModel
                                   {
                                       Studentid = t1.studentid,
                                       Fullname = t1.fullname,
                                       Examid = Convert.ToInt32(t2.Examid),
                                   }).ToList();

                if (!string.IsNullOrEmpty(searchId))
                {
                    var data = matchedData.Where(u => u.Studentid.ToString() == searchId).ToList();
                    return View(data);
                }

                else
                {
                    return View(matchedData);
                } // Pass ViewModel data to the view
            }
        }
        
    }
}