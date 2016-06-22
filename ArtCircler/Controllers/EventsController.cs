using ArtCircler.Models;
using ArtCircler.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ArtCircler.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(EventFormViewModel viewModel)
        {
            
            var evento = new Event
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = DateTime.Parse(string.Format("{0} {1}", viewModel.Date, viewModel.Time)),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue

            };

            _context.Events.Add(evento);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

    }
}