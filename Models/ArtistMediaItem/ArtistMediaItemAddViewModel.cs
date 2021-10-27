using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.ArtistMediaItems
{
    public class ArtistMediaItemAddViewModel
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        // Media item
        public byte[] Content { get; set; }
        [StringLength(200)]
        public string ContentType { get; set; }
        public string Caption { get; set; }
        [Required]
        public HttpPostedFileBase MediaUpload { get; set; }
    }
}