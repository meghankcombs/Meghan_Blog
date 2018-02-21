using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Meghan_Blog.Models;
using System.IO;
using Meghan_Blog.ViewModels;
using PagedList;
using PagedList.Mvc;

namespace Meghan_Blog.Controllers
{
    [RequireHttps]
    public class BlogPostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogPosts
        public ActionResult Index(int? page, string searchStr)
        {
            ViewBag.Search = searchStr;
            var blogList = IndexSearch(searchStr);

            int pageSize = 4;
            int pageNumber = (page ?? 1);

           // var listPosts = db.BlogPosts; need this but how to implement...?

            return View(blogList.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UnpublishedIndex(int? page, string searchStr)
        {
            ViewBag.Search = searchStr;
            var blogList = IndexSearch(searchStr);

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            
            return View("Index", blogList.ToPagedList(pageNumber, pageSize));
        }

        public IQueryable<BlogPost> IndexSearch(string searchStr)
        {
            var result = db.BlogPosts.AsQueryable();
            if(!User.IsInRole("Admin"))
            {
                result = result.Where(p => p.Published == true);
            }

            if(searchStr != null)
            {
                result = result.Where(p =>
                    p.Title.Contains(searchStr)
                    || p.Body.Contains(searchStr)
                    || p.Abstract.Contains(searchStr)
                    || p.Comments.Any(c =>
                        c.Body.Contains(searchStr)
                        || c.Author.FirstName.Contains(searchStr)
                        || c.Author.LastName.Contains(searchStr)
                        || c.Author.DisplayName.Contains(searchStr)
                        || c.Author.Email.Contains(searchStr)
                        )
                    );
            }
            return result.OrderByDescending(p => p.Created);
        }

        // GET: BlogPosts/Details/5
        public ActionResult Details(string slug)
        {
            if (String.IsNullOrWhiteSpace(slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customPostDetails = new CustomPostDetail();

            var oldestDate = DateTime.Now.AddDays(-10);
            var blogId = db.BlogPosts.FirstOrDefault(b => b.Slug == slug).Id;
            customPostDetails.ChangeableComment = db.Comments.FirstOrDefault(c => c.BlogPostId == blogId);
            customPostDetails.RecentPosts = db.BlogPosts.Where(b => b.Created >= oldestDate && b.Published).ToList();
            customPostDetails.BlogPost = db.BlogPosts.FirstOrDefault(p => p.Slug == slug);
            if (customPostDetails == null)
            {
                return HttpNotFound();
            }
            return View(customPostDetails);
        }

        // GET: BlogPosts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogPosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Body,Abstract,Published")] BlogPost blogPost, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var Slug = StringUtilities.URLFriendly(blogPost.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid title");
                    return View(blogPost);
                }
                if (db.BlogPosts.Any(p => p.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique");
                    return View(blogPost);
                }

                blogPost.Slug = Slug;

                blogPost.Created = DateTime.Now;
                
                if (ImageUploadValidator.IsWebFriendlyImage(file))
                {
                    var fileName = Path.GetFileName(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/MyImages/"), fileName));
                    blogPost.MediaUrl = "~/MyImages/" + fileName;
                }

                db.BlogPosts.Add(blogPost);
                db.SaveChanges();
                return RedirectToAction("Index", "BlogPosts", new { id = blogPost.Id });//goes to blog post that was just created
            }

            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogPost blogPost = db.BlogPosts.Find(id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }
            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Created,Title,Body,Abstract,MediaUrl,Published")] BlogPost blogPost, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var slug = StringUtilities.URLFriendly(blogPost.Title);
                if (String.IsNullOrWhiteSpace(slug))
                {
                    ModelState.AddModelError("Title", "Invalid title");
                    return View(blogPost);
                }
                if (db.BlogPosts.Any(p => p.Slug == slug && p.Id != blogPost.Id))
                {
                    ModelState.AddModelError("Title", "The title must be unique");
                    return View(blogPost);
                }

                blogPost.Slug = slug;
                
                if (ImageUploadValidator.IsWebFriendlyImage(file))
                {
                    var fileName = Path.GetFileName(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/MyImages/"), fileName));
                    blogPost.MediaUrl = "~/MyImages/" + fileName;
                }

                blogPost.Updated = DateTime.Now;
                db.Entry(blogPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "BlogPosts");
            }
            return View(blogPost);
        }

        #region Old GET Delete code
        //// GET: BlogPosts/Delete/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BlogPost blogPost = db.BlogPosts.Find(id);
        //    if (blogPost == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(blogPost);
        //}
        #endregion

        // POST & GET: BlogPosts/Delete/5
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogPost blogPost = db.BlogPosts.Find(id);
            db.BlogPosts.Remove(blogPost);
            db.SaveChanges();
            return RedirectToAction("Index", "BlogPosts");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
