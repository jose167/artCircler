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
        [Authorize]
        public ActionResult MyUp()
        {
            var userId = User.Identity.GetUserId();
            var eventos = _context.Events
                .Where(e =>
                e.ArtistId == userId &&
                e.DateTime > DateTime.Now &&
                !e.IsCanceled)
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
                Genres = _context.Genres.ToList(), ///initialize
                Heading = "Add a Event"
            };

            return View("FormEvent",viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var evento = _context.Events.Single(e => e.Id == id && e.ArtistId == userId);

            var viewModel = new EventFormViewModel
            {
                Heading ="Edit a Event",
                Id = evento.Id,
                Genres = _context.Genres.ToList(), ///initialize//
                Date = evento.DateTime.ToString("d MMM yyyy"),
                Time = evento.DateTime.ToString("HH:mm"),
                Genre = evento.GenreId,
                Venue = evento.Venue
         

            };

            return View("FormEvent", viewModel);
        }

   
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();  ///Genre has to be initialize
                return View("FormEvent", viewModel);
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();  ///Genre has to be initialize
                return View("FormEvent", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var evento = _context.Events
                .Include(e => e.Attendances.Select(a => a.Attendee))
                .Single(e => e.Id == viewModel.Id && e.ArtistId == userId);

            evento.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre);

            _context.SaveChanges();
                                   

            return RedirectToAction("MyUp", "Events");

        }

    }
}