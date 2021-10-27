using Assignment_6.Models.ArtistMediaItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.Artists
{
    public class ArtistWithMediaStringViewModel : ArtistBaseViewModel
    {
        public ArtistWithMediaStringViewModel()
        {

            ArtistMediaItems = new List<ArtistMediaItemBaseViewModel>();
        }

        public IEnumerable<ArtistMediaItemBaseViewModel> ArtistMediaItems { get; set; }
    }
    
}