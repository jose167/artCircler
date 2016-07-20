using ArtCircler.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ArtCircler.Models
{
    public class Event
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        [Required]
        [StringLength(255)]
        public string EventName { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public String Venue { get; set; }
                
        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Event()
        {
            Attendances = new Collection<Attendance>(); 
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.EventCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string venue, byte genre, string eventName)
        {
            var notification = Notification.EventUpdated(this, DateTime, Venue);
            
            Venue = venue;
            DateTime = dateTime;
            GenreId = genre;
            EventName = eventName;


            foreach (var attendee in Attendances.Select(a => a.Attendee))

                attendee.Notify(notification);

        }

        public static implicit operator Event(EventDto v)
        {
            throw new NotImplementedException();
        }
    }
}