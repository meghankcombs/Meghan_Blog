using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Meghan_Blog.Models
{
    public class Comment
    {
        //Properties
        public int Id { get; set; }
        public int BlogPostId { get; set; }
        public string AuthorId { get; set; } //points to ApplicationUser() action in IdentityModels
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }// ? makes it nullable
        public string UpdateReason { get; set; }

        //Navigation Properties
        public virtual ApplicationUser Author { get; set; }
        public virtual BlogPost BlogPost { get; set; }
    }
}