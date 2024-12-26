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
    public class ApplycourseController : Controller
    {

        // GET: Applycourse
        public ActionResult Index()
        {
            using(var db = new ExammanagefinalEntities())
            {
                var courses = db.Courses.ToList();
                var exam = db.Exams.ToList();

                var examss = (from t1 in courses
                                   join t2 in exam on t1.courseid equals t2.courseid
                                   select new Matchedexamcourseviewmodel
                                   {
                                       Courseid = t1.courseid,
                                       Coursename= t1?.coursename,
                                       Examid = Convert.ToInt32(t2.Examid),
                                       Duration = Convert.ToInt32(t2.Duration),
                                   }).ToList();

                return View(examss);
            }
            
        }
        public ActionResult ApplyForExam(int examId, string Coursename)
        {
            // Assuming you have a way to get the logged-in student's ID
            var studentId = Convert.ToInt32(Session["StudentID"]);
            using(var db = new ExammanagefinalEntities()) {
                var studentExamApplication = new StudentExamApplication
                {
                    Examid = examId,
                    studentid = studentId,
                    coursename = Coursename,
                    applieddate = DateTime.Now
                };

                // Add the studentExamApplication object to the database
                db.StudentExamApplications.Add(studentExamApplication);
                db.SaveChanges();

                // Redirect to a confirmation page or the same page
                return RedirectToAction("Index", "StudentDashboard");
            }
            // Create a new StudentExamApplication object
           
        }
        public ActionResult Appliedexam()
        {
            using (var db = new ExammanagefinalEntities())
            {
                // Get the currently logged-in student's ID from the session
                var studentId = Convert.ToInt32(Session["StudentID"]);

                // Fetch the exams applied for by the student from the database
                var appliedExams = db.StudentExamApplications
                                     .Where(se => se.studentid == studentId)
                                     .ToList();

                // Return the data to the view
                return View(appliedExams);
            }

        }
        [HttpPost]
        public ActionResult AttendExam(int ExamID)
        {
            using (var db = new ExammanagefinalEntities())
            {
                // Fetch the questions for the given exam ID
                var questions = db.Questions
                                  .Where(q => q.Examid == ExamID)
                                  .OrderBy(q => q.QuestionID) // Assuming you have QuestionID or any order
                                  .ToList();

                // Store the ExamID in TempData to keep track of the current exam
                Session["ExamID"] = ExamID;

                // Pass the first question to the view
                return View("QuestionView", questions.FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult SubmitAnswer(int QuestionID, string SelectedAnswer)
        {
            // Check if required session variables are available
            if (Session["ExamID"] == null || Session["StudentID"] == null)
            {
                // Handle the case where session values are missing (you can redirect or show an error)
                return RedirectToAction("ErrorPage", new { message = "Session expired or invalid." });
            }

            var examId = Convert.ToInt32(Session["ExamID"]);
            var studentId = Convert.ToInt32(Session["StudentID"]);

            using (var db = new ExammanagefinalEntities())
            {
                // Save the student's answer
                var studentAnswer = new StudentAnswer
                {
                    studentid = studentId,
                    Examid = examId,
                    QuestionID = QuestionID,
                    SelectedAnswer = SelectedAnswer
                };

                db.StudentAnswers.Add(studentAnswer);
                db.SaveChanges();

                // Get all questions for the exam
                var questions = db.Questions
                                  .Where(q => q.Examid == examId)
                                  .ToList();

                // Check if the current answer is correct
                var question = questions.FirstOrDefault(q => q.QuestionID == QuestionID);
                if (question != null)
                {
                    var correctAnswer = question.CorrectOption;
                    int marksForThisQuestion = 0;
                    if (SelectedAnswer == correctAnswer)
                    {
                        marksForThisQuestion = 10; // Award 10 marks for each correct answer
                    }

                    // Ensure TotalMarks session variable is initialized
                    var totalMarks = Session["TotalMarks"] != null ? (int)Session["TotalMarks"] : 0;
                    totalMarks += marksForThisQuestion;
                    Session["TotalMarks"] = totalMarks;
                }

                // Check if this is the last question
                var currentIndex = questions.FindIndex(q => q.QuestionID == QuestionID);
                Question nextQuestion = null;

                if (currentIndex == questions.Count - 1)
                {
                    // Redirect to ExamComplete action if this was the last question
                    return RedirectToAction("ExamComplete");
                }
                else
                {
                    // If not the last question, move to the next question
                    nextQuestion = questions[currentIndex + 1];
                }

                return View("QuestionView", nextQuestion);
            }
        }

        public ActionResult ExamComplete()
        {
            // Check if required session variables are available
            if (Session["StudentID"] == null || Session["ExamID"] == null || Session["Studentname"] == null)
            {
                // Handle the case where session values are missing (you can redirect or show an error)
                return RedirectToAction("ErrorPage", new { message = "Session expired or invalid." });
            }

            var studentId = Convert.ToInt32(Session["StudentID"]);
            var examId = Convert.ToInt32(Session["ExamID"]);

            using (var db = new ExammanagefinalEntities())
            {
                // Retrieve the total marks from the session
                int totalMarks = Session["TotalMarks"] != null ? (int)Session["TotalMarks"] : 0;
                var studname = Session["Studentname"].ToString();

                // Save total marks in StudentMark table
                var studentMark = new StudentMark
                {
                    studentid = studentId,
                    studentname = studname,
                    Examid = examId,
                    marks = totalMarks
                };

                db.StudentMarks.Add(studentMark);
                db.SaveChanges();

                // Clear the session marks after saving to the database
                Session["TotalMarks"] = null;

                // Optionally, you can retrieve the student's answers or exam details here
                var studentAnswers = db.StudentAnswers
                                       .Where(a => a.studentid == studentId && a.Examid == examId)
                                       .ToList();

                // Return a view that thanks the student or summarizes the submitted exam
                return View("ExamComplete", studentAnswers);
            }
        }
        [HttpGet]
        public ActionResult Myresult()
        {
            using (var db = new ExammanagefinalEntities())
            {
                var studentId = Convert.ToInt32(Session["StudentID"]);

                // Fetch the questions for the given exam ID
                var questions = db.StudentMarks
                                  .Where(q => q.studentid == studentId)
                                  .ToList();

                return View(questions);
            }
        }

        [HttpGet]
        public ActionResult Editdetails()
        {
            var studentId = Convert.ToInt32(Session["StudentID"]);

            if (studentId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExammanagefinalEntities db = new ExammanagefinalEntities();
            Studentdetail studentdetail = db.Studentdetails.Find(studentId);
            if (studentdetail == null)
            {
                return HttpNotFound();
            }
            return View(studentdetail);
        }

        // POST: Studentdetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editdetails([Bind(Include = "studentid,fullname,username,password,location,email,mobile")] Studentdetail studentdetail)
        {
            ExammanagefinalEntities db = new ExammanagefinalEntities();

            if (ModelState.IsValid)
            {
                db.Entry(studentdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentdetail);
        }
    }
}