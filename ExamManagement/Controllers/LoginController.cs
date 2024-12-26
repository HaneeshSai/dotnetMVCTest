using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamManagement.Models;



namespace ExamManagement.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Nav() { return View(); }
        public ActionResult Navadmin()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Navstudent()
        {
            return RedirectToAction("StudentLogin");
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Adminlogin Objuser)
        {
            if (ModelState.IsValid)
            {
                using (ExammanagefinalEntities db = new ExammanagefinalEntities())
                {
                    var obj = db.Adminlogins.Where(a => a.AdminId == Objuser.AdminId && a.Password == Objuser.Password).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["AdminId"] = obj.AdminId.ToString();
                        TempData["SuccessMessage"] = "Login Successful!";
                        // Instead of redirecting immediately, return the view to show the message
                        return View();
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Id or Password is Incorrect";
                        return View(Objuser);
                    }
                }
            }
            return View(Objuser);
        }
        public ActionResult StudentLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentLogin(Studentdetail Objuser)
        {
            if (ModelState.IsValid)
            {
                using (ExammanagefinalEntities db = new ExammanagefinalEntities())
                {
                    var obj = db.Studentdetails.Where(a => a.username == Objuser.username && a.password == Objuser.password).FirstOrDefault();
                    if (obj != null)
                    {
                        ViewBag.studentid = obj.studentid.ToString();
                        Session["Studentname"] = obj.fullname? .ToString();
                        Session["Studentid"]=obj.studentid.ToString();
                        return RedirectToAction("Index","StudentDashboard");
                    }
                    else
                    {
                        TempData["Success"] = "Id or Password is Incorrect";
                        return View();
                    }
                }
            }
            return View(Objuser);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Studentdetail model)
        {

            if (ModelState.IsValid)
            {
                using (ExammanagefinalEntities db = new ExammanagefinalEntities())
                {
                    var checkUser = db.Studentdetails.Where(x => x.username == model.username).FirstOrDefault();
                    if (checkUser == null)
                    {
                        // Add new user to the database
                        db.Studentdetails.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("StudentLogin");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "User already exists";
                        return View();
                    }
                }
            }
            return View(model);
        }

    }
}