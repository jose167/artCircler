using ArtCircler.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace ArtCircler.Controllers.Api
{
    [Authorize]
    public class EventController : ApiController
    {
        private ApplicationDbContext _context;
            public EventController()
        {
            _context = new ApplicationDbContext();     
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var evento = _context.Events.Single(e => e.Id == id && e.ArtistId == userId);
            
            //in case the click the buttom of delete again
            if (evento.IsCanceled)
                return NotFound();

            evento.IsCanceled = true;

            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Event = evento,
                Type = NotificationType.EventoCanceled
            };
            
            var attendees = _context.Attendances
                .Where(a => a.EventoId == evento.Id)
                .Select(a => a.Attendee)
                .ToList();

            foreach (var attendee in attendees)
            {
                var userNotification = new UserNotification
                {
                    User = attendee,
                    Notification = notification
                };
                _context.UserNotifications.Add(userNotification);
            }


            _context.SaveChanges();

            return Ok();
        }
    }
    
}
