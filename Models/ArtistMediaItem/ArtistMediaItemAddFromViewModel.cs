using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.ArtistMediaItems
{
    public class ArtistMediaItemAddFromViewModel
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        // Media item
        public byte[] Content { get; set; }
        [StringLength(200)]
        public string ContentType { get; set; }

        // For the generated identifier
        [Required, StringLength(100)]
        public string StringId { get; set; }
        public string Caption { get; set; }

        [Display(Name = "Artist name or stage name")]
        public string Name { get; set; }
        public string ArtistName { get; set; }
        [Required]
        [Display(Name = "Sample clip")]
        [DataType(DataType.Upload)]
        public string MediaUpload { get; set; }

        //[Required]
        //public HttpPostedFileBase MediaUpload { get; set; }
    }
}