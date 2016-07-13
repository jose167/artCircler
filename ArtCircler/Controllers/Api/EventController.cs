using ArtCircler.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
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
            var evento = _context.Events
                .Include(e => e.Attendances.Select(a => a.Attendee))
                .Single(e => e.Id == id && e.ArtistId == userId);
            
            //in case the click the buttom of delete again
            if (evento.IsCanceled)
                return NotFound();

            evento.Cancel();
            _context.SaveChanges();

            return Ok();
        }
    }
    
}
