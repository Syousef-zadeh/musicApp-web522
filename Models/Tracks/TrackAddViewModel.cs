using Assignment_6.Models.Albums;
using Assignment_6.Models.ArtistMediaItems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.Tracks
{
    public class TrackAddViewModel
    {

        [Display(Name = "Track name")]
        public string Name { get; set; }

        [Display(Name = "Composer names (comma-separated)")]
        // Simple comma-separated string of all the track's composers
        public string Composers { get; set; }

        public string Genre { get; set; }

        // User name who added/edited the track
        public string Clerk { get; set; }
        
        public int AlbumId { get; set; }
        public int ArtistMediaItemId { get; set; }

        public IEnumerable<AlbumBaseViewModel> Albums { get; set; }


        [Display(Name = "Album name")]
        public IEnumerable<String> AlbumsName { get; set; }

        [Required]
        public HttpPostedFileBase ClipUpload { get; set; }


    }
}