// **************************************************
// WEB524 Project Template V3 == e929885d-be93-471b-9f76-1908f53dfc59
// Do not change this header.
// **************************************************

using Assignment_6.EntityModels;
using Assignment_6.Models;
using Assignment_6.Models.Albums;
using Assignment_6.Models.ArtistMediaItems;
using Assignment_6.Models.Artists;
using Assignment_6.Models.Genres;
using Assignment_6.Models.Tracks;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Assignment_6.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Object mapper definitions

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<AlbumAddViewModel, Album>();

                cfg.CreateMap<Genre, GenreBaseViewModel>();

                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<ArtistAddViewModel, Artist>();
                cfg.CreateMap<Artist, ArtistWithMediaStringViewModel>();


                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<Track, TrackEditViewModel>();
                cfg.CreateMap<TrackBaseViewModel, TrackEditFormViewModel>();

                cfg.CreateMap<ArtistMediaItem, ArtistMediaItemBaseViewModel>();
                cfg.CreateMap<ArtistMediaItem, ArtistMediaItemAddViewModel>();
                cfg.CreateMap<ArtistMediaItem, ArtistMediaItemContentViewModel>();

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        //.............................Album..........................//
        //GetAll
        public IEnumerable<AlbumBaseViewModel> GetAllAlbums()
        {
            return mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(ds.Albums.OrderBy(al => al.Name));
        }


        //GetOne
        public AlbumBaseViewModel AlbumGetById(int id)
        {
            var album = ds.Albums.Find(id);
          
            return album == null ? null : mapper.Map<Album, AlbumBaseViewModel>(album);
        }



       // AddAlbum
        public AlbumBaseViewModel AddAlbum(AlbumAddViewModel newItem)
        {
            var artis = ds.Artists.Find(newItem.ArtistId);

            if (artis == null)
            {
                return null;
            }
            else
            {
                var addedAlbum = ds.Albums.Add(mapper.Map<AlbumAddViewModel, Album>(newItem));
                addedAlbum.Coordinator = User.Name;
                addedAlbum.Artists.Add(artis);
                ds.SaveChanges();
                return addedAlbum == null ? null : mapper.Map<Album, AlbumBaseViewModel>(addedAlbum);
            }

        }





        //.............................Artist............................//
        public IEnumerable<ArtistBaseViewModel> GetAllArtist()
        {
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(ds.Artists.OrderBy(al => al.Name));
        }


        //GetOne
        public ArtistBaseViewModel ArtistGetById(int id)
        {
            var artist = ds.Artists.Find(id);
            return artist == null ? null : mapper.Map<Artist, ArtistBaseViewModel>(artist);
        }


        //AddOne
        public ArtistBaseViewModel ArtistAdd(ArtistAddViewModel newItem)
        {
            var artist = ds.Artists.Add(mapper.Map<ArtistAddViewModel, Artist>(newItem));
            artist.Executive = User.Name;
            ds.SaveChanges();
            return artist == null ? null : mapper.Map<Artist, ArtistBaseViewModel>(artist);
        }

        //................................MediaItem..................................//
        //GetOne
        public ArtistMediaItemContentViewModel ArtistMediaItemGetById(string stringId)
        {
            var media = ds.ArtistMediaItems.SingleOrDefault(p => p.StringId == stringId);

            return (media == null) ? null : mapper.Map<ArtistMediaItem, ArtistMediaItemContentViewModel>(media);
        }

        //ArtistGetByIdWithMediaInfo
        public ArtistWithMediaStringViewModel ArtistGetByIdWithMediaInfo(int id)
        {
            var artist = ds.Artists.Include("ArtistMediaItem").SingleOrDefault(p => p.Id == id);

            return (artist == null) ? null : mapper.Map<Artist, ArtistWithMediaStringViewModel>(artist);
        }

        //MediaItem_Add
        public ArtistBaseViewModel MediaItemAdd(ArtistMediaItemAddViewModel newItem)
        {
            // Validate the associated item
            var artist = ds.Artists.Find(newItem.ArtistId);

            if (artist == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the new item
                var addedItem = new ArtistMediaItem();
                ds.ArtistMediaItems.Add(addedItem);

                addedItem.Caption = newItem.Caption;
                addedItem.Artist = artist;

                byte[] photoBytes = new byte[newItem.MediaUpload.ContentLength];
                newItem.MediaUpload.InputStream.Read(photoBytes, 0, newItem.MediaUpload.ContentLength);

                addedItem.Content = photoBytes;
                addedItem.ContentType = newItem.MediaUpload.ContentType;

                ds.SaveChanges();

                return (addedItem == null) ? null : mapper.Map<Artist, ArtistBaseViewModel>(artist);
            }
        }

        //.............................Track............................//
        //GetAll
        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            var track = ds.Tracks.Include("Albums");
            var result = mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(track.OrderBy(tr => tr.Name));

            foreach (var item in result)
            {

                item.AlbumsName = GetTrackById(item.Id).AlbumsName;
            }
            return result;
        }

        //GetOne
        public TrackBaseViewModel GetTrackById(int id)
        
        {
            var track = ds.Tracks
            .Include("Albums.Artists")
            .Include("Albums")
            .Include("ArtistMediaItem")
            .SingleOrDefault(tra => tra.Id == id);

            if (track == null)
            {
                return null;
            }
            else
            {
                var result = mapper.Map<Track, TrackBaseViewModel>(track);
                result.AlbumsName = track.Albums.Select(a => a.Name);

                return result;
            }
        }



        //AddOne
        public TrackBaseViewModel TrackAdd(TrackAddViewModel newItem)
        {
            var addedItem = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newItem));
            
            addedItem.Clerk = User.Name;
            
            byte[] clipBytes = new byte[newItem.ClipUpload.ContentLength];
            newItem.ClipUpload.InputStream.Read(clipBytes, 0, newItem.ClipUpload.ContentLength);
            addedItem.ClipContentType = newItem.ClipUpload.ContentType;
            addedItem.Clip = clipBytes;

            ds.SaveChanges();
            return addedItem == null ? null : mapper.Map<Track, TrackBaseViewModel>(addedItem);
        }


        //Edit
        public TrackBaseViewModel TrackEdit(TrackEditViewModel newItem)
        {
            var editedItem = ds.Tracks.Find(newItem.Id);

            if (editedItem == null)
            {
                return null;
            }
            else
            {

                byte[] clipBytes = new byte[newItem.ClipUpload.ContentLength];
                newItem.ClipUpload.InputStream.Read(clipBytes, 0, newItem.ClipUpload.ContentLength);
                editedItem.ClipContentType = newItem.ClipUpload.ContentType;
                editedItem.Clip = clipBytes;

              
                ds.SaveChanges();

                return mapper.Map<Track, TrackBaseViewModel>(editedItem);
            }
        }


        //Delete
        public bool TrackDelete(int id)
        {
            var itemToDelete = ds.Tracks.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                try
                {
                    // Remove the object
                    ds.Tracks.Remove(itemToDelete);
                    ds.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        //.............................Genre............................//
        public IEnumerable<GenreBaseViewModel> GetAllGenre()
        {
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres.OrderBy(al => al.Name));
        }






        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Role claims

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Clerk" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Staff" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Genre

            if (ds.Genres.Count() == 0)
            {
                // Add genres

                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Artist

            if (ds.Artists.Count() == 0)
            {
                // Add artists

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Adele_2016.jpg/800px-Adele_2016.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Album

            if (ds.Albums.Count() == 0)
            {
                // Add albums

                // For Bryan Adams
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                });

                ds.SaveChanges();
                done = true;
            }

            // ############################################################
            // Track

            if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For Reckless
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For Reckless
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "RequestUser" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it

    // How to use...

    // In the Manager class, declare a new property named User
    //public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value
    //User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }
}