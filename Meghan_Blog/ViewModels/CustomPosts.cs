using Meghan_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Meghan_Blog.ViewModels
{
    public class CustomPosts
    {
        public List<BlogPost> LatestPosts { get; set; }
        public List<BlogPost> RecentPosts { get; set; }

        public CustomPosts()
        {
            this.LatestPosts = new List<BlogPost>();
            this.RecentPosts = new List<BlogPost>();
        }
    }
}