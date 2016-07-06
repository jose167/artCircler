using ArtCircler.Models;
using System.Collections.Generic;

namespace ArtCircler.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Event> UpcomingEvents { get; set; }
        public bool ShowActions { get; internal set; }
    }
}
