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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;

namespace HSNHospitalProject.Controllers
{
    public class GalleryImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GalleryImages
        public ActionResult Index(int? page)
        {
            List<GalleryImages> galleryImages = db.GalleryImages.ToList();
            Debug.WriteLine("List of all gallery images: " + galleryImages.ToString());

            //check if the user is logged in (true if logged in)
            bool isLoggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            //is admin is false by default
            bool isAdmin = false;
            //if the user is logged in, isAdmin = whether or not the user is an admin
            if (isLoggedIn)
            {
                //below custom column check from https://stackoverflow.com/questions/31864400/how-get-custom-field-in-aspnetusers-table
                isAdmin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId()).is_admin;
            }

            //Pass whether or not this user is a logged in admin through the tempdata
            TempData["isAdmin"] = isAdmin;
            //the amount of gallery images per page
            int pageSize = 12;
            //set the page number to 1 if it is not already set
            int pageNumber = (page ?? 1);
            //return View(students.ToPagedList(pageNumber, pageSize));
            return View(galleryImages.ToPagedList(pageNumber, pageSize));
        }

        // GET: GalleryImages/Details/5
        public ActionResult Details(int? id)
        {
            //**THIS PAGE IS NO LONGER USED - REMOVE AT SOME POINT
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryImages galleryImages = db.GalleryImages.Find(id);
            if (galleryImages == null)
            {
                return HttpNotFound();
            }
            //since it is not used, redirect to the list
            return RedirectToAction("Index");
        }

        // GET: GalleryImages/Create
        public ActionResult Create()
        {

            //check if the user is logged in (true if logged in)
            bool isLoggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            //is admin is false by default
            bool isAdmin = false;
            //if the user is logged in, isAdmin = whether or not the user is an admin
            if (isLoggedIn)
            {
                //below custom column check from https://stackoverflow.com/questions/31864400/how-get-custom-field-in-aspnetusers-table
                isAdmin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId()).is_admin;
            }

            Debug.WriteLine("Is the user currently logged in? " + isLoggedIn);
            Debug.WriteLine("That user is " + this.User.Identity.Name);
            Debug.WriteLine("Is the user an admin? " + isAdmin);

            //if the user isn't a logged or not an admin, redirect to the Index list view
            if (!isLoggedIn || !isAdmin)
            {
                return RedirectToAction("Index");
            }

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

                                galleryImageFile.SaveAs(path); //will overwrite any existing file with this name 
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
            //check if the user is logged in (true if logged in)
            bool isLoggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            //is admin is false by default
            bool isAdmin = false;
            //if the user is logged in, isAdmin = whether or not the user is an admin
            if (isLoggedIn)
            {
                //below custom column check from https://stackoverflow.com/questions/31864400/how-get-custom-field-in-aspnetusers-table
                isAdmin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId()).is_admin;
            }

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
            //if the user isn't a logged or not an admin, redirect to the Index list view
            if (!isLoggedIn || !isAdmin)
            {
                return RedirectToAction("Index");
            }

            return View(galleryImages);
        }

        // POST: GalleryImages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int galleryImagesId, string galleryImagesTitle, HttpPostedFileBase galleryImageFile, string galleryImageAlt, string galleryImagesDescription)
        {
            GalleryImages galleryImage = new GalleryImages();

            //since the page will automatically redirect if not a logged in admin, only an admin will be able to submit this form.
            if (ModelState.IsValid)
            {
                galleryImage.galleryimagetitle = galleryImagesTitle;
                galleryImage.galleryimagealt = galleryImageAlt;
                galleryImage.galleryimagedate = DateTime.Now;
                galleryImage.galleryimagedescription = galleryImagesDescription;
                
                //Probably should put below code in a method at some point
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
                                string fileName = galleryImagesId + "." + extension;

                                //get a direct file path to ~/Content/Artists/{id}.{extension}
                                string path = Path.Combine(Server.MapPath("~/Content/GalleryImages/"), Path.GetFileName(fileName));

                                //delete the old image (to save server space if needed)
                                GalleryImages thisImage = db.GalleryImages.Find(galleryImagesId);
                                FileInfo fileInfo = new FileInfo(Server.MapPath("~/Content/GalleryImages/" + thisImage.galleryimageref));
                                fileInfo.Delete();
                                Debug.WriteLine("This file was deleted: " + thisImage.galleryimageref);


                                //save the file

                                galleryImageFile.SaveAs(path); //will overwrite any existing file with this name 
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
                else
                {
                    //check the image's current ref and set the new extension to be the old one (ie. don't set it to empty upon update)
                    GalleryImages thisImage = db.GalleryImages.Find(galleryImagesId);
                    if (thisImage == null)
                    {
                        Debug.WriteLine("Somehow GalleryImage wasn't found when checking what its old extension was?");
                    } 
                    //if there exists a current image for this entry
                    else if (thisImage.galleryimageref != null)
                    {
                        galleryImageExt = thisImage.galleryimageref;
                    }
                    //in case galleryImageExt was never set somehow
                    if (galleryImageExt == "")
                    {
                        galleryImageExt = thisImage.galleryimageref;
                    }
                }

                galleryImage.galleryimageref = galleryImageExt;
                //Below code seems to be giving me errors since I'm changing form input data before updating so I will use SQL instead
                //db.Entry(galleryImage).State = EntityState.Modified;
                //db.SaveChanges();

                string query = "update GalleryImages set galleryimageref=@imageref, galleryimagetitle=@imagetitle, galleryimagealt=@imagealt, " +
                    "galleryimagedate=@imagedate, galleryimagedescription=@imagedescription where galleryimageid=@id";
                SqlParameter[] sqlparams = new SqlParameter[6];
                sqlparams[0] = new SqlParameter("@imageref", galleryImageExt);
                sqlparams[1] = new SqlParameter("@imagetitle", galleryImage.galleryimagetitle);
                sqlparams[2] = new SqlParameter("@imagealt", galleryImage.galleryimagealt);
                sqlparams[3] = new SqlParameter("@imagedate", galleryImage.galleryimagedate);
                sqlparams[4] = new SqlParameter("@imagedescription", galleryImage.galleryimagedescription);
                sqlparams[5] = new SqlParameter("@id", galleryImagesId);

                db.Database.ExecuteSqlCommand(query, sqlparams);

                return RedirectToAction("Index", new { update = true });
            }
            return View(galleryImage);
        }


        // GET: GalleryImages/Delete/5
        public ActionResult Delete(int? id)
        {
            //check if the user is logged in (true if logged in)
            bool isLoggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            //is admin is false by default
            bool isAdmin = false;
            //if the user is logged in, isAdmin = whether or not the user is an admin
            if (isLoggedIn)
            {
                //below custom column check from https://stackoverflow.com/questions/31864400/how-get-custom-field-in-aspnetusers-table
                isAdmin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId()).is_admin;
            }

            //if the user isn't a logged or not an admin, redirect to the Index list view
            if (!isLoggedIn || !isAdmin)
            {
                return RedirectToAction("Index");
            }

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
            //remember to delete the image file from the server as well here.
            db.GalleryImages.Remove(galleryImages);
            db.SaveChanges();
            return RedirectToAction("Index", new { delete = true});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult showDetails(int galleryimagesid)
        {
            //check if the user is logged in (true if logged in)
            bool isLoggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            //is admin is false by default
            bool isAdmin = false;
            //if the user is logged in, isAdmin = whether or not the user is an admin
            if (isLoggedIn)
            {
                //below custom column check from https://stackoverflow.com/questions/31864400/how-get-custom-field-in-aspnetusers-table
                isAdmin = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId()).is_admin;
            }

            //Pass whether or not this user is a logged in admin through the tempdata
            //only admins should be able to see edit and delete buttons
            TempData["isAdmin"] = isAdmin;

            Debug.WriteLine("Receiving galleryimagesid in the ajax call as " + galleryimagesid);
            //grabbing the galleryImage object
            GalleryImages galleryImages = db.GalleryImages.Find(galleryimagesid);
            //returns a partial view with the given galleryImage and prints it back to whichever div the jquery call indicated in the view
            //(in this case, it will be inside a modal window)
            return PartialView("_showImage", galleryImages);
        }
    }
}
