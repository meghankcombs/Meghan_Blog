using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Meghan_Blog.Models
{
    public class BlogPost
    {
        //Properties
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }// ? makes it nullable
        public string Title { get; set; }
        [AllowHtml] //data annotation allowing for HTML markup (tinyMCE)
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public string Abstract { get; set; }
        public string MediaUrl { get; set; }
        public string Slug { get; set; }
        public bool Published { get; set; }

        //Navigation Properties
        public virtual ICollection<Comment> Comments { get; set; }

        //Constructor
        public BlogPost()
        {
            this.Comments = new HashSet<Comment>();
        }
    }
}