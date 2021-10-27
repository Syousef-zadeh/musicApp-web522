using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.Albums
{
    public class AlbumBaseViewModel : AlbumAddViewModel
    {
        public int Id { get; set; }
    }
}