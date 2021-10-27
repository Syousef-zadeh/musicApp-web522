using Assignment_6.Models.Tracks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.ArtistMediaItems
{
    public class ArtistMediaItemBaseViewModel : ArtistMediaItemAddViewModel
    {

        public DateTime Timestamp { get; set; }

        // For the generated identifier
        [Required, StringLength(100)]
        public string StringId { get; set; }

        // Brief descriptive caption
        [Required, StringLength(100)]
        public string Caption { get; set; }
    }
}