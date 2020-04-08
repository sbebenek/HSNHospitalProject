using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSNHospitalProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HSNHospitalProject.Helpers
{
    /// <summary>
    /// Sam Bebenek -
    /// Holds methods that determins whether a not a user is logged in or if that user is an admin
    /// </summary>
    public class LoggedInChecker
    {
        public LoggedInChecker()
        {

        }
        /// <summary>
        /// Determines if a user is logged in or not. Returns true if a user is logged in, and false if no user is logged in.
        /// </summary>
        /// <returns>bool</returns>
        public static bool isLoggedIn()
        {
            bool isLoggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            Debug.WriteLine("Is there a logged in user? " + isLoggedIn);
            return isLoggedIn;
        }

        /// <summary>
        /// Determines whether or not the logged in user is an admin. Returns true if an admin is logged in, false if the logged in user is not an admin or if there is no user logged in
        /// </summary>
        /// <returns></returns>
        public static bool isAdmin()
        {
            if (isLoggedIn())
            {
                //below custom column check from https://stackoverflow.com/questions/31864400/how-get-custom-field-in-aspnetusers-table
                bool isAdmin = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(loggedInUserId()).is_admin;
                Debug.WriteLine("Is logged in user admin? " + isAdmin);
                return isAdmin;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns the ID of the currently logged in user. Returns null if there is no user logged in
        /// </summary>
        /// <returns></returns>
        public static string loggedInUserId()
        {
            if (isLoggedIn())
            {
                var id = HttpContext.Current.User.Identity.GetUserId();
                Debug.WriteLine("User currently logged in with the ID " + id);
                return id;
            }
            else
                Debug.WriteLine("No user is currently logged in.");
                return null;

            
        }
    }
}