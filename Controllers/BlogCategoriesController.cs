using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Blog.Services;

namespace Blog.Controllers
{
    public class BlogCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        

        public BlogCategoriesController(
            ApplicationDbContext context,
            ISlugService slugService,
            IImageService imageService)
        {
            _context = context;
            _imageService = imageService;

        }

        // GET: BlogCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogCategory.ToListAsync());
        }

        public IActionResult CategoryPosts(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Now that I have an id lets use it to go grab all of the blog posts
            //that have a foreign key that equals the id (all the children)
            var categoryPosts = _context.CategoryPost.Where(cp => cp.BlogCategoryId == id).ToList();
            return View(categoryPosts);
        }


        // GET: BlogCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // GET: BlogCategories/Create
        //Nobody can just come in and create a new category, must occupy the role of Administrator
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageData,ContentType")] BlogCategory blogCategory, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                blogCategory.ContentType = _imageService.RecordContentType(formFile);
                blogCategory.ImageData = await _imageService.EncodeFileAsync(formFile);

                blogCategory.Created = DateTime.Now;
                _context.Add(blogCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogCategory);
        }

        // GET: BlogCategories/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategory.FindAsync(id);
            if (blogCategory == null)
            {
                return NotFound();
            }
            return View(blogCategory);
        }

        // POST: BlogCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Created,ImageData,ContentType")] BlogCategory blogCategory, IFormFile formFile)
        {
            if (id != blogCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (formFile != null)
                {
                    blogCategory.ContentType = _imageService.RecordContentType(formFile);
                    blogCategory.ImageData = await _imageService.EncodeFileAsync(formFile);

                }
                try
                {
                    blogCategory.Updated = DateTime.Now;
                    _context.Update(blogCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogCategoryExists(blogCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogCategory);
        }

        // GET: BlogCategories/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // POST: BlogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogCategory = await _context.BlogCategory.FindAsync(id);
            _context.BlogCategory.Remove(blogCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogCategoryExists(int id)
        {
            return _context.BlogCategory.Any(e => e.Id == id);
        }
    }
}
