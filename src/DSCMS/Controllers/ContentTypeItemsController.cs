using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DSCMS.Data;
using DSCMS.Models;

namespace DSCMS.Controllers
{
    public class ContentTypeItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContentTypeItemsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ContentTypeItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ContentTypeItems.Include(c => c.ContentType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ContentTypeItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentTypeItem = await _context.ContentTypeItems.SingleOrDefaultAsync(m => m.ContentTypeItemId == id);
            if (contentTypeItem == null)
            {
                return NotFound();
            }

            return View(contentTypeItem);
        }

        // GET: ContentTypeItems/Create
        public IActionResult Create()
        {
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId");
            return View();
        }

        // POST: ContentTypeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentTypeItemId,ContentTypeId,Name")] ContentTypeItem contentTypeItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentTypeItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", contentTypeItem.ContentTypeId);
            return View(contentTypeItem);
        }

        // GET: ContentTypeItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentTypeItem = await _context.ContentTypeItems.SingleOrDefaultAsync(m => m.ContentTypeItemId == id);
            if (contentTypeItem == null)
            {
                return NotFound();
            }
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", contentTypeItem.ContentTypeId);
            return View(contentTypeItem);
        }

        // POST: ContentTypeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentTypeItemId,ContentTypeId,Name")] ContentTypeItem contentTypeItem)
        {
            if (id != contentTypeItem.ContentTypeItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentTypeItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentTypeItemExists(contentTypeItem.ContentTypeItemId))
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
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", contentTypeItem.ContentTypeId);
            return View(contentTypeItem);
        }

        // GET: ContentTypeItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentTypeItem = await _context.ContentTypeItems.SingleOrDefaultAsync(m => m.ContentTypeItemId == id);
            if (contentTypeItem == null)
            {
                return NotFound();
            }

            return View(contentTypeItem);
        }

        // POST: ContentTypeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentTypeItem = await _context.ContentTypeItems.SingleOrDefaultAsync(m => m.ContentTypeItemId == id);
            _context.ContentTypeItems.Remove(contentTypeItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ContentTypeItemExists(int id)
        {
            return _context.ContentTypeItems.Any(e => e.ContentTypeItemId == id);
        }
    }
}