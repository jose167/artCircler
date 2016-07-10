using ArtCircler.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace ArtCircler.Controllers.Api
{
    [Authorize]
    public class AttendanceController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendanceController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.EventoId == dto.EventoId))
            return BadRequest("The Attendance already exists. ");

            var attendance = new Attendance
            {
                EventoId = dto.EventoId,
                AttendeeId = userId

            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        } 

    }
}
