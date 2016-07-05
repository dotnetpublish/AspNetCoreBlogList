using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreBlogList.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AspNetCoreBlogList.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly BlogPostContext _context;

        public async Task<ActionResult> RenderImage(int id)
        {
            BlogPost blog = await _context.BlogPosts.SingleOrDefaultAsync(m => m.BlogPostId == id);
            byte[] itemImage = blog.BlogPostImage;
            return File(itemImage, "image/png");
        }

        public BlogPostsController(BlogPostContext context)
        {
            _context = context;    
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogPosts.ToListAsync());
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.BlogPosts.SingleOrDefaultAsync(m => m.BlogPostId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogPostId,AspNetCoreVersion,BlogPostLink,BlogPostTitle,BlogAuthor,TwitterHandle")] BlogPost blog, IFormFile blogImage)
        {
            if (ModelState.IsValid)
            {
                if (blogImage != null)
                {
                    blog.BlogPostImage = await GetBytes(blogImage);
                }

                if (!string.IsNullOrEmpty(blog.BlogPostLink))
                {
                    blog.BlogPostLink = GetUri(blog.BlogPostLink);
                }

                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(blog);
        }


        public async Task<byte[]> GetBytes(IFormFile file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public string GetUri(string s)
        {
            return new UriBuilder(s).Uri.ToString();
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.BlogPosts.SingleOrDefaultAsync(m => m.BlogPostId == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogPostId,AspNetCoreVersion,BlogPostLink,BlogPostTitle,BlogAuthor,TwitterHandle")] BlogPost blog, IFormFile blogImage)
        {
            if (id != blog.BlogPostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (blogImage != null)
                    {
                        blog.BlogPostImage = await GetBytes(blogImage);
                    }

                    if (!string.IsNullOrEmpty(blog.BlogPostLink))
                    {
                        blog.BlogPostLink = GetUri(blog.BlogPostLink);
                    }

                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogPostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.BlogPosts.SingleOrDefaultAsync(m => m.BlogPostId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.BlogPosts.SingleOrDefaultAsync(m => m.BlogPostId == id);
            _context.BlogPosts.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BlogExists(int id)
        {
            return _context.BlogPosts.Any(e => e.BlogPostId == id);
        }
    }
}
