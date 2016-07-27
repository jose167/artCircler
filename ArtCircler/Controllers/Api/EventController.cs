using ArtCircler.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ArtCircler.Controllers.Api
{
    [Authorize]
    public class EventController : ApiController
    {
        private HttpControllerContext @object;
        private ApplicationDbContext _context;
            public EventController()
        {
            _context = new ApplicationDbContext();     
        }

        public EventController(HttpControllerContext @object)
        {
            this.@object = @object;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var evento = _context.Events
                .Include(e => e.Attendances.Select(a => a.Attendee))
                .Single(e => e.Id == id && e.ArtistId == userId);

            
            if (evento == null)
                return NotFound();
            //in case the click the buttom of delete again
            if (evento.IsCanceled)
                return NotFound();

            evento.Cancel();
            _context.SaveChanges();

            return Ok();
        }
    }
    
}
