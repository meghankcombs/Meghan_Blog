using Meghan_Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Meghan_Blog.Helpers
{
    public class UserHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static string AuthorName()
        {
            var user = db.Users.FirstOrDefault(u => u.Email == "meghankcombs@gmail.com");
            var authorName = user.FirstName + " " + user.LastName;
            return authorName;
        }
    }
}