using Meghan_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meghan_Blog.ViewModels
{
    public class CustomPostDetail
    {    
        public List<BlogPost> RecentPosts { get; set; }
        public BlogPost BlogPost { get; set; }
        public Comment ChangeableComments { get; set; }

        public CustomPostDetail()
        {
            this.RecentPosts = new List<BlogPost>();
        }    
    }
}