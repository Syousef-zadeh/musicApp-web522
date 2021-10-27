using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_6.Models.Tracks
{
    public class TrackAddFormViewModel : TrackAddViewModel
    {
        [Display(Name = "Track genre")]
        public SelectList GenreList { get; set; }
        //[Required]
        //[Display(Name = "Sample clip")]
        //[DataType(DataType.Upload)]
        //public string ClipUpload { get; set; }

        public string AlbumName { get; set; }
    }
}