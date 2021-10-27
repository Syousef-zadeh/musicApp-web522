using Assignment_6.Models.Albums;
using Assignment_6.Models.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_6.Controllers.ModelControllers
{
    public class AlbumsController : Controller
    {
       private Manager m = new Manager();
        // GET: Albums
        public ActionResult Index()
        {
            return View(m.GetAllAlbums());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            var alb = m.AlbumGetById(id.GetValueOrDefault());
            if (alb == null)
            {
                return HttpNotFound();
            }
            return View(alb);
        }

        //Get addTrack 
        [Route("album/{id}/addtrack"), Authorize(Roles = "Clerk")]
        public ActionResult AddTrack(int? id)
        {
            var album = m.AlbumGetById(id.GetValueOrDefault());
            var form = new TrackAddFormViewModel();
            form.AlbumId = album.Id;
            form.AlbumName = album.Name;

            form.GenreList = new SelectList(m.GetAllGenre(), "Name", "Name");
            
            return View(form);
        }
        //Post

        [Route("album/{id}/addtrack"), Authorize(Roles = "Clerk")]
        [HttpPost]
        public ActionResult AddTrack(TrackAddViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            var addedItem = m.TrackAdd(newItem);
            addedItem.AlbumId = newItem.AlbumId;
            //addedItem.AlbumsName = newItem.AlbumsName;
            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {

                return RedirectToAction("Details", "Tracks", new { id = addedItem.Id });
            }
        }




        // POST: Albums/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Albums/Edit/5
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

        // GET: Albums/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Albums/Delete/5
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
