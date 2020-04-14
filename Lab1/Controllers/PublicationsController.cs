using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1;
using Microsoft.AspNetCore.Authorization;

namespace Lab1.Controllers
{
    public class PublicationsController : Controller
    {
        private readonly Professor_PublicationContext _context;

        public PublicationsController(Professor_PublicationContext context)
        {
            _context = context;
        }

        public IActionResult CheckVersion(int version)
        {
            var collection = _context.Publication.ToArray();
            var reg = (from u in collection
                       where u.Version == version
                       select new { version }).FirstOrDefault();

            if (reg != null) return Json(false);
            
            return Json(true);
        }
        public IActionResult CheckNumber(string namePublication)
        {
            var collection = _context.Publication.ToArray();
            var reg = (from u in collection
                       where u.NamePublication.ToUpper() == namePublication.ToUpper()
                       select new { namePublication }).FirstOrDefault();

            if (reg != null) return Json(false);

            return Json(true);
        }
        // GET: Publications
        public async Task<IActionResult> Index()
        {
            
            var professor_PublicationContext = _context.Publication.Include(p => p.Publishing);
            return View(await professor_PublicationContext.ToListAsync());
        }
            
        // GET: Publications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication
                .Include(p => p.Publishing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // GET: Publications/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["PublishingId"] = new SelectList(_context.PublishingCollection, "Id", "PublishingName");
            return View();
        }

        // POST: Publications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Id,PublishingId,Version,NamePublication,PageAmount")] Publication publication)
        {
       
            if (ModelState.IsValid && ModelState.Count < 2)
            {
                _context.Add(publication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PublishingId"] = new SelectList(_context.PublishingCollection, "Id", "PublishingName", publication.PublishingId);
            return View(publication);
        }

        // GET: Publications/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }
            ViewData["PublishingId"] = new SelectList(_context.PublishingCollection, "Id", "PublishingName", publication.PublishingId);
            return View(publication);
        }

        // POST: Publications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PublishingId,Version,NamePublication,PageAmount")] Publication publication)
        {
            if (id != publication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicationExists(publication.Id))
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
            ViewData["PublishingId"] = new SelectList(_context.PublishingCollection, "Id", "PublishingName", publication.PublishingId);
            return View(publication);
        }

        // GET: Publications/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication
                .Include(p => p.Publishing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // POST: Publications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publication = await _context.Publication.FindAsync(id);
            _context.Publication.Remove(publication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicationExists(int id)
        {
            return _context.Publication.Any(e => e.Id == id);
        }
    }
}
