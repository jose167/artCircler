using System;

namespace ArtCircler.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }

        public bool IsCanceled { get; set; }

        public UserDto Artist { get; set; }
        public DateTime DateTime { get; set; }
        public String Venue { get; set; }
        public GenreDto Genre { get; set; }
        
    }

}