using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using HSNHospitalProject.Helpers;
using HSNHospitalProject.Models;
using HSNHospitalProject.Models.ViewModels;

namespace HSNHospitalProject.Controllers
{
    public class ArticleController:Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        /// <summary>
        /// List Method for Article's returns a list of articles depending on the search string and page number
        /// </summary>
        /// <param name="articleSearchKey">The Search string entered in hte search input box in the view</param>
        /// <param name="pagenum"> the page which has to be displayed</param>
        /// <returns>Returns a List of Articles to the View.</returns>
        public ActionResult Index(string articleSearchKey,int pagenum=0)
        {
            
            //pass a value to the view without using a view model
            //here we can send a boolean which is used in the view to display create/update buttons and links if the user is an admin
            TempData["isAdmin"] = LoggedInChecker.isAdmin();

            //using Christine Bittle's method for pagination from PetGroomingMVC
            //https://github.com/christinebittle/PetGroomingMVC/blob/master/PetGrooming/Controllers/GroomServiceController.cs
            List<Article> articles = db.Articles.Where(a => (articleSearchKey != null) ? a.articleTitle.Contains(articleSearchKey) : true).ToList();
            int perpage = 3;
            int articleCount = articles.Count();
            int maxpage = (int)Math.Ceiling((decimal)articleCount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";
            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                articles = db.Articles
                    .Where(a => (articleSearchKey != null) ? a.articleTitle.Contains(articleSearchKey) : true)
                    .OrderBy(a => a.articleId)
                    .Skip(start)
                    .Take(perpage)
                    .ToList();
            }

            

            return View(articles);
        }


        /// <summary>
        /// Create method for Articles, checks if the current user is an admin and allows access to ../Article/Create otherwise returns users to the List page
        /// </summary>
        /// <returns>Returns the Create View if the current user is an admin</returns>
        public ActionResult Create()
        {
            //using the helper method  to check if the current user is an admin
            if (LoggedInChecker.isAdmin())
            {
                return View();
            }
            else
            {//if they are not they are sent back to the list of articles
                return RedirectToAction("Index");
            }
            
        }


        /// <summary>
        /// Update method for articles, checks if the current user is an admin and allows access to ../Article/Update/id otherwise returns users to the List page 
        /// </summary>
        /// <param name="id">The is the id of the article which is to be updated</param>
        /// <returns>Returns the view containing the article who matches the id param</returns>
        public ActionResult Update(int? id)
        {
            //using the helper method  to check if the current user is an admin
            if (LoggedInChecker.isAdmin())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Article article = db.Articles.SqlQuery("select * from articles where articleId=@articleId", new SqlParameter("@articleId", id)).FirstOrDefault();

                if (article == null)
                {
                    return HttpNotFound();

                }
                return View(article);
            }
            else
            {
                //if they are not they are sent back to the list of articles
                return RedirectToAction("Index");
            }
        }
            
        
        /// <summary>
        /// The POST update method for articles, grabs the variables from the view and uses parametezied queries to add the changes to the database
        /// </summary>
        /// <param name="article">The article returned from the view</param>
        /// <param name="id">The id of the article which is going to be updated. </param>
        /// <returns></returns>
        [HttpPost]
        //since the non-POST method checks if the user is an admin there is no need to check again. 
        public ActionResult Update(Article article, int id)
        {
            string query = "update articles set articleTitle = @articleTitle, articleBody = @articleBody,articleDateLastEdit = @articleDateLastEdit where articleId = @id";

            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@articleTitle", article.articleTitle);
            sqlparams[1] = new SqlParameter("@articleBody", article.articleBody);
            sqlparams[2] = new SqlParameter("@articleDateLastEdit", DateTime.Now);
            sqlparams[3] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("Index");

        }

        /// <summary>
        /// The POST Create method for articles 
        /// </summary>
        /// <param name="article">contains the article values from the view</param>
        /// <returns></returns>
        [HttpPost]
        //since the non-POST method checks if the user is an admin there is no need to check again. 
        public ActionResult Create(Article article)
        {
            
            string query = "insert into articles (articleTitle,articleBody,articleDatePosted)" +
                "values (@articleTitle,@articleBody,@articleDatePosted)";
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@articleTitle", article.articleTitle);
            sqlParameters[1] = new SqlParameter("@articleBody", article.articleBody);
            sqlParameters[2] = new SqlParameter("@articleDatePosted", DateTime.Now);
            

            db.Database.ExecuteSqlCommand(query, sqlParameters);

            return RedirectToAction("Index");
            
        }


        /// <summary>
        /// Delete Method for article, currently only used in the Update view.
        /// </summary>
        /// <param name="id">The id of the article to be deleted</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            //Check if the current user is an admin. 
            if (LoggedInChecker.isAdmin())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Article article = db.Articles.Find(id);
                db.Articles.Remove(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {//if they are not return them to the list of articles
                return RedirectToAction("Index");
            }
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