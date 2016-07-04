using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtCircler.Models
{
    public class Attendance
    {
        public Event Evento { get; set; }
        public ApplicationUser Attendee { get; set; }

        [Key]
        [Column(Order = 1)]
        public int EventoId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }
    }
}