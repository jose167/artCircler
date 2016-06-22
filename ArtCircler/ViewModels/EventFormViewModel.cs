using ArtCircler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArtCircler.ViewModels
{
    public class EventFormViewModel
    {
        [Required]
        public string Venue { get; set; }

        [Required]
        [ValidateFutureDay]
        public string Date { get; set; }

        [Required]
        [ValidateTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
    
}