using ExamManagement;
using ExamManagement.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ExamManagement.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new
           HomeController();
            // Act
            ViewResult result =
           controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

    }
}
