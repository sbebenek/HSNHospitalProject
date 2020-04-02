using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HSNHospitalProject.Models
{
    public class Article
    {

        [Key]
        public int articleId { get; set; }

        [Required]
        public string title { get; set; }


        public DateTime dateCreated { get; set; }

        //date the article was last edited, would get updated when the update method in the controller is called
        public DateTime dateLastEdit { get; set; }

        [Required]
        //a short view of the article, might be usefull for mobile views 
        public string condensed { get; set; }

        [Required]
        //the actual content
        public string content { get; set; }

        //the author who wrote it
        public string authorId { get; set; }

        [ForeignKey("authorId")]
        public virtual ApplicationUser authordId { get; set; }
    }
}