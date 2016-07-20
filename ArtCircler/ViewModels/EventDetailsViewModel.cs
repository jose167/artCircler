using ArtCircler.Models;

namespace ArtCircler.ViewModels
{
    public class EventDetailsViewModel
    {
        public Event Evento { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
    }
}