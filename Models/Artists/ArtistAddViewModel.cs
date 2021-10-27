using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.Artists
{
    public class ArtistAddViewModel
    {
        public ArtistAddViewModel()
        {
            BirthOrStartDate = DateTime.Now;
        }
        public int ArtistMediaItemId { get; set; }
        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }

        // For an individual, a birth name
        [Display(Name = "If applicable, artist's birth name")]
        public string BirthName { get; set; }

        // For an individual, a birth date
        // For all others, can be the date the artist started working together
        [Display(Name = "Birth date, or start date")]
        [DataType(DataType.Date)]
        public DateTime BirthOrStartDate { get; set; }

        // Get from Apple iTunes Preview, Amazon, or Wikipedia
        [Display(Name = "Artist photo")]
        public string UrlArtist { get; set; }
        [Display(Name = "Artist's primary genre")]
        public string Genre { get; set; }
        public string ArtistName { get; set; }

        // User name who looks after this artist
        public string Executive { get; set; }
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }
    }
}