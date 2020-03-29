using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSNHospitalProject.Models;

namespace HSNHospitalProject.Controllers
{
    public class GalleryImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GalleryImages
        public ActionResult Index()
        {
            return View(db.GalleryImages.ToList());
        }

        // GET: GalleryImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImages galleryImages = db.GalleryImages.Find(id);
            if (galleryImages == null)
            {
                return HttpNotFound();
            }
            return View(galleryImages);
        }

        // GET: GalleryImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GalleryImages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "galleryimageid,galleryimageref,galleryimagetitle,galleryimagealt,galleryimagedate,galleryimagedescription")] GalleryImages galleryImages)
        {
            //Bind takes form field values and puts them into a GalleryImages object
            //try to implement file upload on create
            if (ModelState.IsValid)
            {
                //add the bound GalleryImages object from the method parameter and add it to the GalleryImages database
                db.GalleryImages.Add(galleryImages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(galleryImages);
        }

        // GET: GalleryImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //change to redirect to 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImages galleryImages = db.GalleryImages.Find(id);
            if (galleryImages == null)
            {
                return HttpNotFound();
            }
            return View(galleryImages);
        }

        // POST: GalleryImages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "galleryimageid,galleryimageref,galleryimagetitle,galleryimagealt,galleryimagedate,galleryimagedescription")] GalleryImages galleryImages)
        {
            //since the page will automatically redirect if not a logged in admin, only an admin will be able to submit this form.
            if (ModelState.IsValid)
            {
                db.Entry(galleryImages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(galleryImages);
        }

        // GET: GalleryImages/Delete/5
        public ActionResult Delete(int? id)
        {
            //have the delete page redirect if user is not a logged in admin
            if (id == null)
            {
                //change to redirect to list page
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImages galleryImages = db.GalleryImages.Find(id);
            if (galleryImages == null)
            {
                //this is fine to keep
                return HttpNotFound();
            }
            return View(galleryImages);
        }

        // POST: GalleryImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //since the page will automatically redirect if not a logged in admin, only an admin will be able to submit this form.
            GalleryImages galleryImages = db.GalleryImages.Find(id);
            db.GalleryImages.Remove(galleryImages);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
