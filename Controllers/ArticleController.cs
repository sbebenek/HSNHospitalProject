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
using HSNHospitalProject.Models;
using HSNHospitalProject.Models.ViewModels;

namespace HSNHospitalProject.Controllers
{
    public class ArticleController:Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string articleSearchKey,int pagenum=0)
        {
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

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Update(int id)
        {
            Article article = db.Articles.SqlQuery("select * from articles where articleId=@articleId", new SqlParameter("@articleId", id)).FirstOrDefault();

            if (article == null)
            {
                return HttpNotFound();

            }
            return View(article);
        }

        [HttpPost]
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

        [HttpPost]
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