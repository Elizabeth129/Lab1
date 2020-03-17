using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1;

namespace Lab1.Controllers
{
    public class PublishingCollectionsController : Controller
    {
        private readonly Professor_PublicationContext _context;

        public PublishingCollectionsController(Professor_PublicationContext context)
        {
            _context = context;
        }

        public IActionResult CheckDegree(string publishingName)
        {
            var collection = _context.PublishingCollection.ToArray();
            var reg = (from u in collection
                       where u.PublishingName.ToUpper() == publishingName.ToUpper()
                       select new { publishingName }).FirstOrDefault();

            if (reg != null) return Json(false);
            
            return Json(true);
        }

        // GET: PublishingCollections
        public async Task<IActionResult> Index()    
        {
            return View(await _context.PublishingCollection.ToListAsync());
        }

        // GET: PublishingCollections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publishingCollection = await _context.PublishingCollection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publishingCollection == null)
            {
                return NotFound();
            }

            //return View(publishingCollection);
            return RedirectToAction("Index", "PublishingPublications", new { id = publishingCollection.Id , name = publishingCollection.PublishingName});
        }

        // GET: PublishingCollections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PublishingCollections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PublishingName")] PublishingCollection publishingCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publishingCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publishingCollection);
        }

        // GET: PublishingCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publishingCollection = await _context.PublishingCollection.FindAsync(id);
            if (publishingCollection == null)
            {
                return NotFound();
            }
            return View(publishingCollection);
        }

        // POST: PublishingCollections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PublishingName")] PublishingCollection publishingCollection)
        {
            if (id != publishingCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publishingCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublishingCollectionExists(publishingCollection.Id))
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
            return View(publishingCollection);
        }

        // GET: PublishingCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publishingCollection = await _context.PublishingCollection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publishingCollection == null)
            {
                return NotFound();
            }

            return View(publishingCollection);
        }

        // POST: PublishingCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publishingCollection = await _context.PublishingCollection.FindAsync(id);
            _context.PublishingCollection.Remove(publishingCollection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublishingCollectionExists(int id)
        {
            return _context.PublishingCollection.Any(e => e.Id == id);
        }
    }
}
