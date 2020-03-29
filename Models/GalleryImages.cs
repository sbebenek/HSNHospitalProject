using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HSNHospitalProject.Models
{
    public class GalleryImages
    {
        //primary key
        [Key]
        public int galleryimageid { get; set; }

        //extension for image file, Content/GalleryImage/galleryimageref
        public string galleryimageref { get; set; }

        //The title of the image
        public string galleryimagetitle { get; set; }

        //the alt text for the image
        public string galleryimagealt { get; set; }

        //the date the image was posted (In a DateTime object)
        public DateTime galleryimagedate { get; set; }

        //A description of the image
        public string galleryimagedescription { get; set; }
    }
}