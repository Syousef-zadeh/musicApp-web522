using Assignment_6.Models.Albums;
using Assignment_6.Models.ArtistMediaItems;
using Assignment_6.Models.Artists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_6.Controllers.ModelControllers
{
    public class ArtistsController : Controller
    {
       private Manager m = new Manager();
        // GET: Artists
        public ActionResult Index()
        {
            return View(m.GetAllArtist());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            var art = m.ArtistGetByIdWithMediaInfo(id.GetValueOrDefault());
            
            if (art == null)
            {
                return HttpNotFound();
            }
            return View(art);
        }

        [Route("media/{stringid}")]
        public ActionResult DetailsWithMedia(string stringId = "")
        {
            var media = m.ArtistMediaItemGetById(stringId);

            if (media == null)
            {
                return HttpNotFound();
            }
            return File(media.Content, media.ContentType);
        }

        // GET: Artists/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
         
                var form = new ArtistAddFormViewModel();
                form.GenreList = new SelectList(m.GetAllGenre(), "Name", "Name");
                return View(form);
           

        }

        // POST: Artists/Create
        [Authorize(Roles = "Executive")]
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(ArtistAddViewModel Item)
        {
            if (!ModelState.IsValid)
            {
                return View(Item);
            }
            var addedItem = m.ArtistAdd(Item);
            if (Item == null)
            {
                return View(Item);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedItem.Id });
            }
        }
        //Get
        [Authorize(Roles = "Coordinator")]
        public ActionResult AddAlbum(int? id)
        {
            var artist = m.ArtistGetById(id.GetValueOrDefault());

            var form = new AlbumAddFormViewModel();
            form.ArtistId = artist.Id;
            form.GenreList = new SelectList(m.GetAllGenre(), "Name", "Name");
            form.ArtistName = artist.Name;
            return View(form);
        }

        //Post
        [Authorize(Roles = "Coordinator")]
        [HttpPost, ValidateInput(false)]
        public ActionResult AddAlbum(AlbumAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            var addedItem = m.AddAlbum(newItem);
            addedItem.ArtistId = newItem.ArtistId;
            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {

                return RedirectToAction("Details", "Albums", new { id = addedItem.Id });
            }
        }
        //Get-AddMedia
        public ActionResult AddMedia(int? id)
        {
            var artist = m.ArtistGetByIdWithMediaInfo(id.GetValueOrDefault());
            var form = new ArtistMediaItemAddFromViewModel();
            form.ArtistId = artist.Id;
            form.ArtistName = artist.Name;
            
            return View(form);
        }

        //Post
        [HttpPost]
        public ActionResult AddMedia(int? id, ArtistMediaItemAddViewModel newItem)
        {
            if (!ModelState.IsValid && id.GetValueOrDefault() == newItem.ArtistId)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.MediaItemAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", new { id = addedItem.Id });
            }
        }



        // GET: Artists/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Artists/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Artists/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
