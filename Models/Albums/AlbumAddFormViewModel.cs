using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_6.Models.Albums
{
    public class AlbumAddFormViewModel : AlbumAddViewModel
    {
        [Display(Name = "Albums's primary genre")]
        public SelectList GenreList { get; set; }

        public string ArtistName { get; set; }
    }
}