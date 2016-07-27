using ArtCircler.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace ArtCircler.test.Controllers
{
    [TestClass]
    public class EventsControllerTest
    {
        [TestMethod]
        public void TestDetailsView()
        {
            var controller = new EventsController();
            var result = controller.Details(2) as ViewResult;
            if (result != null) Assert.AreEqual("Details", result.ViewName);
        }
    }
}
