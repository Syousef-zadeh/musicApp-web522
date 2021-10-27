using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.Tracks
{
    public class TrackEditFormViewModel 
    {
        public int Id { get; set; }
        [Display(Name = "Track name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Sample clip")]
        [DataType(DataType.Upload)]
        public string ClipUpload { get; set; }
        public string Composers { get; set; }

        public string Genre { get; set; }

        // User name who added/edited the track
        public string Clerk { get; set; }
    }
}