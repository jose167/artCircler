using ArtCircler.Models;
using ArtCircler.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
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

        public ActionResult MyUp()
        {
            var userId = User.Identity.GetUserId();
            var eventos = _context.Events
                .Where(e => e.ArtistId == userId && e.DateTime > DateTime.Now)
                .Include(e => e.Genre)
                .ToList();

            return View(eventos);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var eventos = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Evento)
                .Include( e => e.Artist)
                .Include(e => e.Genre)
                .ToList();

            var viewModel = new HomeViewModel()
            {
                UpcomingEvents = eventos,
                ShowActions = User.Identity.IsAuthenticated
            };

            return View(viewModel);
        }
   
        
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel
            {
                Genres = _context.Genres.ToList() ///initialize
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();  ///Genre has to be initialize
                return View("Create", viewModel);
            }
            var evento = new Event
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Events.Add(evento);
            _context.SaveChanges();

            return RedirectToAction("MyUp", "Events");

        }

    }
}