using ExamManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ExamManagement.Controllers
{
    public class AdminDashboardController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Layout = "~/Views/Shared/Adminsidebar.cshtml"; // Path to your layout
            base.OnActionExecuting(filterContext);
        }
        // GET: AdminDashboard
        public ActionResult Admindash()
        {
            using (ExammanagefinalEntities db = new ExammanagefinalEntities())
            {
                int totalstud = db.Studentdetails.Count();
                int totalexams = db.Exams.Count();
                int totalcourses = db.Courses.Count();
                ViewBag.TotalStudents = totalstud;
                ViewBag.TotalExams = totalexams;
                ViewBag.TotalCourses = totalcourses;
                return View();
            }
        }

        public ActionResult Examget(string searchId )
        {
            using (var db = new ExammanagefinalEntities())
            {
                var users = db.Exams.ToList();

                if (!string.IsNullOrEmpty(searchId))
                {
                    users = users.Where(u => u.Examid.ToString() == searchId).ToList();
                    return View(users);
                }

                else
                {
                    return View(users);
                }
           
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Exam examdata)
        {
            try
            {
                //Connect to Linq to entities
                using (var context = new ExammanagefinalEntities())
                {
                    //add data to particular table
                    context.Exams.Add(examdata);
                    int resultValue = context.SaveChanges();

                    if (resultValue > 0)
                    {
                        Response.Write("Exam added Succesfully");
                    }
                    else
                    {
                        Response.Write("Exam not inserted");
                    }


                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return View();
        }

        public ActionResult Editexam1(int? id)
        {
            ExammanagefinalEntities db = new ExammanagefinalEntities();
            var courses = db.Courses.ToList();
            ViewBag.CourseList = new SelectList(courses, "CourseID", "CourseName");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new ExammanagefinalEntities())
            {
                var stud = context.Exams.Find(id);
                if (stud == null)
                {
                    return HttpNotFound();
                }
                return View(stud);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editexam1([Bind(Include = "Examid,Courseid,Examdate,Duration")] Exam userdata)
        {
            if (ModelState.IsValid)
            {

                using (var context = new ExammanagefinalEntities())
                {

                    context.Entry(userdata).State = EntityState.Modified;
                    int result = context.SaveChanges();
                    if (result > 0)
                    {
                        Response.Write("Updated Succesffuly");
                        return RedirectToAction("Examget");
                    }
                    else
                    {
                        Response.Write("Not Updated");
                    }

                }

            }
            return View(userdata);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var context = new ExammanagefinalEntities())
            {
                var student = context.Exams.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
        }

        // POST: Studentsdatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var context = new ExammanagefinalEntities())
            {
                var aa = context.Exams.Find(id);
                context.Exams.Remove(aa);
                context.SaveChanges();
                return RedirectToAction("Examget");
            }
        }
    }
}