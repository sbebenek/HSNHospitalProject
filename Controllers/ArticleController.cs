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

        public ActionResult Index()
        {
            List<Article> articles = db.Articles.ToList();

            return View(articles);
        }

        public ActionResult Create()
        {
            return View();
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