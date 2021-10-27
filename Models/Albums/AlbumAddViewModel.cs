using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.Albums
{
    public class AlbumAddViewModel 
    {
        public AlbumAddViewModel()
        {
            ReleaseDate = DateTime.Now;
        }

        [Display(Name = "Album Name")]
        public string Name { get; set; }
        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "URL to album image (cover art)")]
        // Get from Apple iTunes Preview, Amazon, or Wikipedia
        public string UrlAlbum { get; set; }

        [Display(Name = "Album's primary genre")]
        public string Genre { get; set; }

        // User name who looks after the album
        public string Coordinator { get; set; }

        public int ArtistId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }
    }
}