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

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _context.Attendances
                .SingleOrDefault(a => a.AttendeeId == userId && a.EventoId == id);

            if (attendance == null)
                return NotFound();

            _context.Attendances.Remove(attendance);
            _context.SaveChanges();

            return Ok(id);
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
