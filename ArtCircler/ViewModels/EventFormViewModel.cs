using ArtCircler.Controllers;
using ArtCircler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ArtCircler.ViewModels
{
    public class EventFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }

        [Required]
        [ValidateFutureDay]
        public string Date { get; set; }

        [Required]
        [ValidateTime]
        public string Time { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }
        public string Action
        { get
            {
                Expression<Func<EventsController, ActionResult>> update =
                    (c => c.Update(this));
                Expression<Func<EventsController, ActionResult>> create =
                    (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
    
}