using System;
using System.ComponentModel.DataAnnotations;

namespace ArtCircler.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime?  OriginalDateTime { get; set; }
        public string OriginalVenu { get; private set; }

       [Required]
        public Event Event { get; set; }

        protected Notification()
        {
        }

        private Notification(NotificationType type, Event evento)
        {
            if (evento == null)
                throw new ArgumentNullException("evento");

            Type = type;
            Event = evento;
            DateTime = DateTime.Now;
        }

        public static Notification EventCreated(Event evento)
        {
            return new Notification(NotificationType.EventoCreated, evento);
        }
        public static Notification EventUpdated(Event NewEvento, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.EventoUpdated, NewEvento);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenu = originalVenue;

            return notification;
        }

        public static Notification EventCanceled(Event evento)
        {
            return new Notification(NotificationType.EventoCanceled, evento);
        }

        

    }

      
        
}