using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ExamManagement.Models;
using System.Security.Cryptography.X509Certificates;
using ExamManagement;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var context = new ExamManagementEntities())
            {
                var admin = new Adminlogin
                {
                    AdminId = 10,
                    AdminName = "Veeresh",
                    Password = "12345",
                };

                // Act
                context.Adminlogins.Add(admin);
                context.SaveChanges(); 

                var insertedAdmin = context.Adminlogins.SingleOrDefault(a => a.AdminId == 10); 
                Assert.IsNotNull(insertedAdmin);  
                Assert.AreEqual("Veeresh", insertedAdmin.AdminName);
                Assert.AreEqual("12345", insertedAdmin.Password); 
            }
        }
    }
}