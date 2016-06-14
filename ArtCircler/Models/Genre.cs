using System;
using System.ComponentModel.DataAnnotations;

namespace ArtCircler.Models
{
    public class Genre
    {
        public Byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public String Name { get; set; }
    }
}