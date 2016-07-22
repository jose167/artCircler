using ArtCircler.Models;
using System.Collections.Generic;
using System.Linq;

namespace ArtCircler.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Event> UpcomingEvents { get; set; }
        public bool ShowActions { get; internal set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> Attendances { get; set; }
    }
}