using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamManagement.Models;

namespace ExamManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly ExammanagefinalEntities db = new ExammanagefinalEntities();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Layout = "~/Views/Shared/Adminsidebar.cshtml"; // Path to your layout
            base.OnActionExecuting(filterContext);
        }
        // GET: Course
        public ActionResult Index(string searchId)
        {
            try
            {
                using (var db = new ExammanagefinalEntities())
                {
                    var users = db.Courses.ToList();

                    if (!string.IsNullOrEmpty(searchId))
                    {
                        users = users.Where(u => u.courseid.ToString() == searchId).ToList();
                        return View(users);
                    }

                    else
                    {
                        return View(users);
                    }

                }
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging framework in production)
                ViewBag.ErrorMessage = "An error occurred while fetching the courses.";
                return View("Error", new HandleErrorInfo(ex, "Course", "Index"));
            }
        }

        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Cours cours = db.Courses.Find(id);
                if (cours == null)
                {
                    return HttpNotFound();
                }
                return View(cours);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching the course details.";
                return View("Error", new HandleErrorInfo(ex, "Course", "Details"));
            }
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "courseid,coursename,Descriptions")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Courses.Add(cours);
                    int result =db.SaveChanges(); 
                    if(result > 0)
                    {
                        TempData["SuccessMessage"] = "Course Created Successfully!";
                        return View();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Course Adding Failed";
                        return View();
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error occurred while creating the course.";
                    return View("Error", new HandleErrorInfo(ex, "Course", "Create"));
                }
            }
            return View(cours);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Cours cours = db.Courses.Find(id);
                if (cours == null)
                {
                    return HttpNotFound();
                }
                return View(cours);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching the course for editing.";
                return View("Error", new HandleErrorInfo(ex, "Course", "Edit"));
            }
        }

        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "courseid,coursename,Descriptions")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(cours).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "An error occurred while updating the course.";
                    return View("Error", new HandleErrorInfo(ex, "Course", "Edit"));
                }
            }
            return View(cours);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                Cours cours = db.Courses.Find(id);
                if (cours == null)
                {
                    return HttpNotFound();
                }
                return View(cours);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while fetching the course for deletion.";
                return View("Error", new HandleErrorInfo(ex, "Course", "Delete"));
            }
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Cours cours = db.Courses.Find(id);
                db.Courses.Remove(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while deleting the course.";
                return View("Error", new HandleErrorInfo(ex, "Course", "Delete"));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}