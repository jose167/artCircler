using ArtCircler.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ArtCircler.Dtos
{
    public class NotificationDto
    {
        
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        [Required]
        public EventDto Event { get; set; }
    }
}