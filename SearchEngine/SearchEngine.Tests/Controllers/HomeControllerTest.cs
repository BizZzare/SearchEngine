using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchEngine;
using SearchEngine.Controllers;

namespace SearchEngine.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async void WebSearch_Returns_List_of_Results()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = await controller.WebSearch("some search");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DbLookup_returns_view()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.DbLookup() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DbSearch_returns_view()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.DbSearch("some search") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
