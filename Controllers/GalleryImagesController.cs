using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
            List<GalleryImages> galleryImages = db.GalleryImages.ToList();
            Debug.WriteLine("List of all gallery images: " + galleryImages.ToString());
            return View(galleryImages);
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
        public ActionResult Create(string galleryImagesTitle, HttpPostedFileBase galleryImageFile, string galleryImageAlt, string galleryImagesDescription)
        {
            GalleryImages galleryImage = new GalleryImages();

            //Bind takes form field values and puts them into a GalleryImages object
            //try to implement file upload on create
            if (ModelState.IsValid)
            {
                galleryImage.galleryimagetitle = galleryImagesTitle;
                galleryImage.galleryimagealt = galleryImageAlt;
                galleryImage.galleryimagedate = DateTime.Now;
                galleryImage.galleryimagedescription = galleryImagesDescription;
                //I will upload the image at the next stage
                galleryImage.galleryimageref = "";

                //add the galleryImage object to the database and save changes
                db.GalleryImages.Add(galleryImage);
                db.SaveChanges();

                //now that its added, I can get the ID and update at the ID with the image file named after the ID
                int id = galleryImage.galleryimageid;
                Debug.WriteLine("ID of newly added image is " + id);

                //image uploading here
                /**BELOW CODE BORROWED FROM CLASS EXAMPLE - https://github.com/christinebittle/PetGroomingMVC/blob/master/PetGrooming/Controllers/PetController.cs**/
                string galleryImageExt = "";
                //checking to see if an image was uploaded
                if (galleryImageFile != null)
                {
                    Debug.WriteLine("File was uploaded...");
                    //checking to see if the file size is greater than 0 (bytes)
                    if (galleryImageFile.ContentLength > 0)
                    {
                        Debug.WriteLine("Successfully Identified Image");
                        Debug.WriteLine("Image uploaded was " + galleryImageFile.FileName);

                        //file extensioncheck taken from https://www.c-sharpcorner.com/article/file-upload-extension-validation-in-asp-net-mvc-and-javascript/
                        var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
                        var extension = Path.GetExtension(galleryImageFile.FileName).Substring(1);

                        if (valtypes.Contains(extension))
                        {
                            try
                            {
                                //file name is the id of the image
                                string fileName = id + "." + extension;

                                //get a direct file path to ~/Content/Artists/{id}.{extension}
                                string path = Path.Combine(Server.MapPath("~/Content/GalleryImages/"), Path.GetFileName(fileName));

                                //save the file

                                //****@TODO: delete the old image if it exists, currently it will save new images with different filepaths instead of overwriting


                                galleryImageFile.SaveAs(path); //will overwrite any existing file with this name (aka the old artist's image)
                                                               //if these are all successful then we can set these fields
                                galleryImageExt = fileName;
                                Debug.WriteLine("Saving gallery image at " + path);


                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine("Gallery Image was not saved successfully.");
                                Debug.WriteLine("Exception:" + ex);
                            }

                        }
                    }
                }//else, no file was uploaded. Don't save anything new.
                

                string query = "update GalleryImages set galleryimageref=@imageref where galleryimageid=@id";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@imageref", galleryImageExt);
                sqlparams[1] = new SqlParameter("@id", id);

                Debug.WriteLine("Setting galleryimageref = " + galleryImageExt + " for the image with id=" + id);

                db.Database.ExecuteSqlCommand(query, sqlparams);
                return RedirectToAction("Index", new { add = true });

            }

            return View(galleryImage);
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
