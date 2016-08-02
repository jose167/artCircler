using ArtCircler.Controllers.Api;
using ArtCircler.test.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Mvc;


namespace ArtCircler.test.Controllers.Api
{
    [TestClass]
    public class EventsControllerTest
    {
        private EventController _controller;

        public EventsControllerTest()
        {
           var mockRepository = new Mock<ControllerContext>();
            
            var mockUow = new Mock<HttpControllerContext>();
            
            _controller = new EventController(mockUow.Object);
            _controller.MockCurrentUser("1");


        }


        [TestMethod]
        public void Cancel_NoEventoWithGivenIdExits_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
