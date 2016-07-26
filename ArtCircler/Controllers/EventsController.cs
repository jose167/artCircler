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

        public FileResult Photo(int id)
        {
            var evento = _context.Events
                .Include(e => e.Artist)
                .SingleOrDefault(e => e.Id == id);
            
                if (evento.Artist.ProfilePicture != null)
                { 
                   return new FileContentResult(evento.Artist.ProfilePicture, "image/jpeg");
                }
                else
                {
                   return new FilePathResult("/images/blanckprofile.png", "image/jpeg");
                }
        }


        public ActionResult ArtistProfile(int id)
        {

            var evento = _context.Events
                .Include(e => e.Artist)
                .Include(e => e.Genre)
                .SingleOrDefault(e => e.Id == id);
            
            var viewModel = new ArtistViewModel { Evento = evento };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsFollowing = _context.Followings
                     .Any(f => f.FolloweeId == evento.ArtistId && f.FolloweeId == userId);

            }
            
            
           return View("ArtistProfile", viewModel);
        }

        public ActionResult Details(int id)
        {
            var evento = _context.Events
                .Include(e => e.Artist)
                .Include(e => e.Genre)
                .SingleOrDefault(e => e.Id == id);

            if (evento == null)
                return HttpNotFound();

            var viewModel = new EventDetailsViewModel { Evento = evento };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendances
                    .Any(a => a.EventoId == evento.Id && a.AttendeeId == userId);

                viewModel.IsFollowing = _context.Followings
                    .Any(f => f.FolloweeId == evento.ArtistId && f.FolloweeId == userId);

            }

            return View("Details", viewModel);
        }



        [System.Web.Mvc.Authorize]
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

        [System.Web.Mvc.Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var eventos = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Evento)
                .Include( e => e.Artist)
                .Include(e => e.Genre)
                .ToList();

            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Evento.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.EventoId);

            var viewModel = new HomeViewModel()
            {
                UpcomingEvents = eventos,
                ShowActions = User.Identity.IsAuthenticated,
                Attendances = attendances
            };

            return View(viewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Search(HomeViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchTerm});
        }
        
        [System.Web.Mvc.Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel
            {
                Genres = _context.Genres.ToList(), ///initialize
                Heading = "Add a Event"
            };

            return View("FormEvent",viewModel);
        }

        [System.Web.Mvc.Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var evento = _context.Events.Single(e => e.Id == id && e.ArtistId == userId);

            var viewModel = new EventFormViewModel
            {
                Heading ="Edit a Event",
                Id = evento.Id,
                EventName = evento.EventName,
                Genres = _context.Genres.ToList(), ///initialize//
                Date = evento.DateTime.ToString("d MMM yyyy"),
                Time = evento.DateTime.ToString("HH:mm"),
                Genre = evento.GenreId,
                Venue = evento.Venue
         

            };

            return View("FormEvent", viewModel);
        }

   
        [System.Web.Mvc.Authorize]
        [System.Web.Mvc.HttpPost]
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
                EventName = viewModel.EventName,
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Events.Add(evento);
            _context.SaveChanges();

            return RedirectToAction("MyUp", "Events");

        }

        [System.Web.Mvc.Authorize]
        [System.Web.Mvc.HttpPost]
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

            evento.Modify(viewModel.GetDateTime(), viewModel.Venue, viewModel.Genre, viewModel.EventName);

            _context.SaveChanges();
                                   

            return RedirectToAction("MyUp", "Events");

        }

        

    }
}