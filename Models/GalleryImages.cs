using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HSNHospitalProject.Models
{
    public class GalleryImages
    {
        /// <summary>
        /// primary key
        /// </summary>
        [Key]
        public int galleryimageid { get; set; }

        /// <summary>
        /// extension for image file, Content/GalleryImage/galleryimageref
        /// </summary>
        public string galleryimageref { get; set; }

        /// <summary>
        /// The title of the image
        /// </summary>
        public string galleryimagetitle { get; set; }

        /// <summary>
        /// the alt text for the image
        /// </summary>
        public string galleryimagealt { get; set; }

        /// <summary>
        /// the date the image was posted (In a DateTime object)
        /// </summary>
        public DateTime galleryimagedate { get; set; }

        /// <summary>
        /// A description of the image
        /// </summary>
        public string galleryimagedescription { get; set; }
    }
}