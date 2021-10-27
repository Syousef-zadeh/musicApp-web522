using Assignment_6.Models.ArtistMediaItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment_6.Models.Artists
{
    public class ArtistBaseViewModel : ArtistAddViewModel
    {
        public int Id { get; set; }

        public IEnumerable<ArtistMediaItemBaseViewModel> ArtistMediaItem { get; set; }
        
    }

    
}