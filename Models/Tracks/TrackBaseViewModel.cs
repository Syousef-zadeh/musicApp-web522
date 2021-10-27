using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.Tracks
{
    public class TrackBaseViewModel : TrackAddViewModel
    {
      

        public int Id { get; set; }
        [Display(Name = "Sample clip")]
        public string ClipUrl
        {
            get
            {
                return $"/clip/{Id}";
            }
        }

        public string ClipContentType { get; set; }
        public byte[] Clip { get; set; }

        

    }
}