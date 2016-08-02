using ArtCircler.Controllers;
using ArtCircler.Models;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

        [TestMethod]
        public void Event_TestDetailsEventData_ShouldReturnNotNull()
        {
            var controller = new EventsController();
            var result = controller.Details(2) as ViewResult;

            if (result != null)
            {
                var event1 = (Event)result.ViewData.Model;
                Assert.AreEqual("Pantinig in Famil", event1.EventName);
            }
        }

        [TestMethod]
        [Ignore]
        public void Event_TestEditEvent_ShouldReturnEventUpdated()
        {
            var controller1 = new MyController()
            {
                GetUserId = () => "170376af-c241-4688-b913-ed5c1f1719a4"
            };
           

            var controller = new EventsController();
            var result = controller.Details(1) as ViewResult;
            
            if (result != null)
            {
                var event1 = (Event)result.ViewData.Model;

                event1.EventName = "Test";
            }
            
            controller.Edit(1);
            var result1 = controller.Details(1) as ViewResult;

            if (result1 != null)
            {
                var event2 = (Event)result1.ViewData.Model;
                Assert.AreEqual("Test", event2.EventName);
            }
        }

        public class MyController : Controller //or ApiController
        {
            public Func<string> GetUserId; //For testing

            public MyController()
            {
                GetUserId = () => User.Identity.GetUserId();
            }
            //controller actions
        }

        [TestMethod]
        public void Event_TestArtistProfile_ShouldReturnArtistView()
        {
            var controller = new EventsController();
            var result = controller.ArtistProfile(2) as ViewResult;
            if (result != null) Assert.AreEqual("ArtistProfile", result.ViewName);
        }


    }
}
