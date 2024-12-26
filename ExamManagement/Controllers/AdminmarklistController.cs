using ExamManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamManagement.Controllers
{
    public class AdminmarklistController : Controller
    {
        // GET: Adminmarklist
        [HttpGet]
        public ActionResult Overallmark(string studentid)
        {
            using (var db = new ExammanagefinalEntities())
            {
                try
                {
                    var users = db.StudentMarks.ToList();
                    if (!string.IsNullOrEmpty(studentid))
                    {
                        users = users.Where(u => u.studentid.ToString() == studentid).ToList();
                        return View(users);
                    }

                    else
                    {
                        return View(users);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (use a logging framework in production)
                    ViewBag.ErrorMessage = "An error occurred while fetching the courses.";
                    return View("Error", new HandleErrorInfo(ex, "Course", "Index"));
                }
            }
        }
    }
}