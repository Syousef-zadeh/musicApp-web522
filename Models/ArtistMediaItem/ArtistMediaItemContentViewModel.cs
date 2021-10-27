using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.ArtistMediaItems
{
    public class ArtistMediaItemContentViewModel
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        [StringLength(200)]
        public string ContentType { get; set; }
    }
}