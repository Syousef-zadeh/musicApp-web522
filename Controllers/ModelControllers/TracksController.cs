using Assignment_6.Models.Tracks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_6.Controllers.ModelControllers
{
    public class TracksController : Controller
    {
        private Manager m = new Manager();
        // GET: Tracks
        public ActionResult Index()
        {
            return View(m.TrackGetAll());
        }

        // GET: Tracks/Details/5
        public ActionResult Details(int? id)
        {
            var track = m.GetTrackById(id.GetValueOrDefault());
            
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        [Route("clip/{id}")]
        public ActionResult DetailsWithClip(int? id)
        {
            var item = m.GetTrackById(id.GetValueOrDefault());

            if (item == null)
            {
                return HttpNotFound();
            }
            else if (item.Clip == null || item.ClipContentType == null)
            {
                return null;
            }
            else
            {
                return File(item.Clip, item.ClipContentType); ;
            }

        }




        // GET: Tracks/Edit/5
        [Authorize(Roles = "Clerk")]
        public ActionResult Edit(int? id)
        {
            var track = m.GetTrackById(id.GetValueOrDefault());

            if (track == null)
            {
                return HttpNotFound();
            }
            else
            {
                var form = m.mapper.Map<TrackEditFormViewModel>(track);
                
                return View(form);
            }
        }

        // POST: Tracks/Edit/5
        [HttpPost, Authorize(Roles = "Clerk")]
        public ActionResult Edit(int? id, TrackEditViewModel newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = newItem.Id });
            }

            var editedItem = m.TrackEdit(newItem);

            if (editedItem == null)
            {
                return RedirectToAction("Edit", new { id = newItem.Id });
            }
            else
            {
                return RedirectToAction("Details", new { id = newItem.Id });
            }
        }

        // GET: Tracks/Delete/5
        [Authorize(Roles = "Clerk")]
        public ActionResult Delete(int? id)
        {
            var itemDelete = m.GetTrackById(id.GetValueOrDefault());
            if (itemDelete == null)
            {
                return Redirect("Index");
            }
            else
            {
                return View(itemDelete);
            }
        }

        // POST: Tracks/Delete/5
        [HttpPost, Authorize(Roles = "Clerk")]
        public ActionResult Delete(int? id, FormCollection collection)
        {
          
                var wasDeleted = m.TrackDelete(id.GetValueOrDefault());
                return RedirectToAction("Index");
        
        }
    }
}
