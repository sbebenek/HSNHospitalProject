using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSNHospitalProject.Models;
using HSNHospitalProject.Models.ViewModels;
using System.Diagnostics;
//for datetime 
using System.Globalization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace HSNHospitalProject.Logs
{
    //Jashanpreet kaur
    //A class for check the user is logged in or not 
    //if logged in then check that is it user or admin
    public class LoggedIn
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public static bool isLoggedIn()
        {
            bool loggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if(loggedIn == true)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
        public static string UserId()
        {
            if(isLoggedIn())
            {
                string id = HttpContext.Current.User.Identity.GetUserId();
                return id;
            }
            else
            {
                return null;
            }
            
        }
        public static bool isAdmin()
        {
            bool admin = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(UserId()).is_admin;
            return admin;
        }
    }
}