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
using Blog.Services;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Blog.Controllers
{
    public class CategoryPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        private readonly ISlugService _slugService;

        public CategoryPostsController(
            ApplicationDbContext context,
            ISlugService slugService,
            IImageService imageService)
        {
            _context = context;
            _slugService = slugService;
            _imageService = imageService;
        }

        // GET: CategoryPosts
        public async Task<IActionResult> Index(int? pageNumber, string searchString)
        {
            ViewData["SearchString"] = searchString;

            //Want to look at the incoming pageNumber variable and either use it or force it to be 1
            //use Null coelesing operator
            //loop++  same as loop +=1
            pageNumber = pageNumber == null || pageNumber <= 0 ? 1 : pageNumber;
            //pageNumber ??= 1;
            ViewData["PageNumber"] = pageNumber;
            //Define a page size
            int pageSize = 3;
            int totalRecords = 0;
            int totalPages = 0;

            //either use the searchString or not
            IQueryable<CategoryPost> result = null;
            if(!string.IsNullOrEmpty(searchString))
            {


               
                //I have to search my records for the presence of the search string
                result = _context.CategoryPost.AsQueryable();
                searchString = searchString.ToLower();

                result = result.Where(cp => cp.Title.ToLower().Contains(searchString) ||
                                            cp.Content.ToLower().Contains(searchString) ||
                                            cp.Abstract.ToLower().Contains(searchString) ||
                                            //cp.BlogCategory.Name.ToLower().Contains(searchString) ||
                                            cp.Comments.Any(c => c.Body.ToLower().Contains(searchString) ||
                                                         c.BlogUser.FirstName.ToLower().Contains(searchString) ||
                                                         c.BlogUser.LastName.ToLower().Contains(searchString) ||
                                                         c.BlogUser.Email.ToLower().Contains(searchString)));
                //Once this LINQ query executes, I can find out how many records and therefore how many pages
                totalRecords = (await result.ToListAsync()).Count;

            }
            else
            {
                result = _context.CategoryPost.AsQueryable();
                totalRecords = (await result.ToListAsync()).Count;
            }
            //var remainder = totalRecords % pageSize;
            if(totalRecords == 0)
            {
                totalPages = totalRecords;
            }
            else if((totalRecords % pageSize) > 0)
            {
                totalPages = Convert.ToInt32(totalRecords / pageSize) + 1;
            }
            else
            {
                totalPages = Convert.ToInt32(totalRecords / pageSize);
            }

            //totalPages = totalRecords == 0 ? 0 : Convert.ToInt32(totalRecords / pageSize) + 1;
            ViewData["TotalPages"] = totalPages;
            pageNumber = pageNumber > totalPages ? totalPages : pageNumber;

            if (totalPages > 0)
            {
                ViewData["PageXofY"] = $"You are viewing page {pageNumber} of {totalPages}";
            }
            else
            {
                ViewData["PageXofY"] = $"Your search for '{@ViewBag.SearchString}' yielded no results";
            }
            //ViewData["SearchString"] = searchString;
            //var applicationDbContext = _context.CategoryPost.Include(c => c.BlogCategory);
            var skipCount = ((int)pageNumber - 1) * pageSize;
            

            var categoryPosts = (await result.OrderByDescending(cp => cp.Created).ToListAsync()).Skip(skipCount).Take(pageSize);
            return View(categoryPosts);
        }

        //public async Task<IActionResult> CategoryIndex(int? id)
        //{
        //    return View("Index", await _context.CategoryPost.Where(cp => cp.BlogCategoryId == id).ToListAsync());
        //}

        public async Task<IActionResult> CategoryIndex(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            //Write a LINQ statement that uses the Id to get all of the Blog osts with the Category Id FK = id
            var categoryPosts = await _context.CategoryPost.Where(cp => cp.BlogCategoryId == id).ToListAsync();

            //Once I have my Blog posts I want to display them in the Index view
            return View("Index", categoryPosts);
        }

        //GET: CategoryPosts/Details/5

        //COMMENT THIS OUT TO USE SLUGS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPost = await _context.CategoryPost
                .Include(cp => cp.BlogCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryPost == null)
            {
                return NotFound();
            }

            return View(categoryPost);
        }

        //UNCOMMENT THIS TO USE SLUGS

        //public async Task<IActionResult> Details(string slug)
        //{
        //    if (string.IsNullOrEmpty(slug))
        //    {
        //        return NotFound();
        //    }

        //    var categoryPost = await _context.CategoryPost
        //        .Include(cp => cp.BlogCategory)
        //        .Include(cp => cp.Comments)
        //        .ThenInclude(c => c.BlogUser)
        //        .FirstOrDefaultAsync(m => m.Slug == slug);
        //    if (categoryPost == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(categoryPost);
        //}

        // GET: CategoryPosts/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            //I have created a BlogCategoryId KEY in the ViewData dictionary
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name");
            return View();
        }

        // POST: CategoryPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BlogCategoryId,Title,Abstract,Content,IsProductionReady,ImageData,ContentType")] CategoryPost categoryPost, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {

                var slug = _slugService.URLFriendly(categoryPost.Title);
                if (_slugService.IsUnique(_context, slug))
                {
                    categoryPost.Slug = slug;
                }
                else
                {
                    ModelState.AddModelError("Title", "This Title cannot be used as it results in a duplicate Slug!");
                    ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name");
                    return View(categoryPost);
                }
                categoryPost.Created = DateTime.Now;

                
                    categoryPost.ContentType = _imageService.RecordContentType(formFile);
                    categoryPost.ImageData = await _imageService.EncodeFileAsync(formFile);
                //This is the code for turning an image into a byte array
                using var ms = new MemoryStream();
                await formFile.CopyToAsync(ms);
                categoryPost.ImageData = ms.ToArray();



                _context.Add(categoryPost);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Home");
            }
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name", categoryPost.BlogCategoryId);
            return View(categoryPost);
        }

        // GET: CategoryPosts/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPost = await _context.CategoryPost.FindAsync(id);
            if (categoryPost == null)
            {
                return NotFound();
            }
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name", categoryPost.BlogCategoryId);
            return View(categoryPost);
        }

        // POST: CategoryPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogCategoryId,Title,Abstract,Content,IsProductionReady,Created,Slug,ImageData,ContentType")] CategoryPost categoryPost, IFormFile formFile)
        {

            if (id != categoryPost.Id)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            {
                if (formFile != null)
                {
                    categoryPost.ContentType = _imageService.RecordContentType(formFile);
                    categoryPost.ImageData = await _imageService.EncodeFileAsync(formFile);

                }
                try
                {
                 var slug = _slugService.URLFriendly(categoryPost.Title);
                  if (slug != categoryPost.Slug)
                    {
                        if (_slugService.IsUnique(_context, slug))
                        {
                            categoryPost.Slug = slug;
                        }
                        else
                        {
                            ModelState.AddModelError("Title", "This Title cannot be used as it results in a duplicate Slug!");
                            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name");
                            return View(categoryPost);
                        }
                    }
                    
                    categoryPost.Updated = DateTime.Now;
                    _context.Update(categoryPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryPostExists(categoryPost.Id))
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
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategory, "Id", "Name", categoryPost.BlogCategoryId);
            return View(categoryPost);
        }

        // GET: CategoryPosts/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryPost = await _context.CategoryPost
                .Include(c => c.BlogCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryPost == null)
            {
                return NotFound();
            }

            return View(categoryPost);
        }

        // POST: CategoryPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryPost = await _context.CategoryPost.FindAsync(id);
            _context.CategoryPost.Remove(categoryPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryPostExists(int id)
        {
            return _context.CategoryPost.Any(e => e.Id == id);
        }
    }
}
