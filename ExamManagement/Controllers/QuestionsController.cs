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
    public class QuestionsController : Controller
    {
        private  ExammanagefinalEntities db = new ExammanagefinalEntities();

        // GET: Questions
        public ActionResult Index(string searchId)
        {
            using (var db = new ExammanagefinalEntities())
            {
                var users = db.Questions.ToList();

                if (!string.IsNullOrEmpty(searchId))
                {
                    users = users.Where(u => u.QuestionID.ToString() == searchId).ToList();
                    return View(users);
                }

                else
                {
                    return View(users);
                }

            }
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        [HttpGet]
        public ActionResult Create()
        {
            using (ExammanagefinalEntities db = new ExammanagefinalEntities())
            {
                // Fetch the courses and create a list of SelectListItem
                var courseList = db.Courses.Select(c => new SelectListItem
                {
                    Value = c.courseid.ToString(),
                    Text = c.coursename.ToString()
                }).ToList();

                // Pass the list to the ViewBag
                ViewBag.CourseList = courseList;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(Exam examdata)
        {
            try
            {
                using (var context = new ExammanagefinalEntities())
                {
                    context.Exams.Add(examdata);
                    context.SaveChanges();
                    return RedirectToAction("Index");  // Redirect to some action after saving
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
            }
            return View(examdata);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            var courses = db.Courses.ToList();
            var exam = db.Exams.ToList();
            ViewBag.CourseList = new SelectList(courses, "CourseID", "CourseName");
            ViewBag.ExamList = new SelectList(exam, "Examid", "Examid");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionID,Examid,Courseid,QuestionText,OptionA,OptionB,OptionC,OptionD,CorrectOption")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
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
