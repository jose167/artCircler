﻿using ArtCircler.Models;
using ArtCircler.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ArtCircler.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

       
        public ActionResult Index(string query = null)
        {
            var upcomingEvents = _context.Events
                .Include(e => e.Artist)
                .Include(e => e.Genre)
                .Where(e => e.DateTime > DateTime.Now && !e.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingEvents = upcomingEvents
                    .Where(e =>
                        e.Artist.Name.Contains(query) ||
                        e.Genre.Name.Contains(query) ||
                        e.EventName.Contains(query) ||
                        e.Venue.Contains(query));
            }

            var viewModel = new HomeViewModel
            {
                UpcomingEvents = upcomingEvents,
                ShowActions = User.Identity.IsAuthenticated,
                SearchTerm = query
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

  
}