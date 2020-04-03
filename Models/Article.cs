using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HSNHospitalProject.Models
{
    public class Article
    {
        [Key]
        public int articleId { get; set; }

        [Required]
        public string articleTitle { get; set; }

        [Required]
        public string articleBody { get; set; }

        public DateTime articleDatePosted { get; set; }
        
        public DateTime? articleDateLastEdit { get; set; }

    }
}