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
    public class PublishingPublicationsController : Controller
    {
        private readonly Professor_PublicationContext _context;

        public PublishingPublicationsController(Professor_PublicationContext context)
        {
            _context = context;
        }

        // GET: PublishingPublications
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "PublishingCollections");
            ViewBag.PublishingId = id;
            ViewBag.PublishingName = name;
            var publicationByPublishing = _context.Publication.Where(b => b.PublishingId == id).Include(p => p.Publishing);
            return View(await publicationByPublishing.ToListAsync());
        }

        // GET: PublishingPublications/Details/5
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

            //return View(publication);
            return RedirectToAction("Index", "Professor1PublicationLinkers", new {id = publication.Id, name = publication.NamePublication});
        }

        // GET: PublishingPublications/Create
        public IActionResult Create(int publishingId)
        {
            // ViewData["PublishingId"] = new SelectList(_context.PublishingCollection, "Id", "PublishingName");
            ViewBag.PublishingId = publishingId;
            ViewBag.PublishingName = _context.PublishingCollection.Where(c => c.Id == publishingId).FirstOrDefault().PublishingName;
            return View();
        }

        // POST: PublishingPublications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int publishingId, [Bind("Id,PublishingId,Version,NamePublication,PageAmount")] Publication publication)
        {
            publication.PublishingId = publishingId;
            if (ModelState.IsValid)
            {
                _context.Add(publication);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "PublishingPublications", new {id = publishingId, name = _context.PublishingCollection.Where(c => c.Id == publishingId).FirstOrDefault().PublishingName });
            }
            //ViewData["PublishingId"] = new SelectList(_context.PublishingCollection, "Id", "PublishingName", publication.PublishingId);
            //return View(publication);
            return RedirectToAction("Index", "PublishingPublications", new { id = publishingId, name = _context.PublishingCollection.Where(c => c.Id == publishingId).FirstOrDefault().PublishingName });

        }

        // GET: PublishingPublications/Edit/5
        public async Task<IActionResult> Edit(int? id, int? publishingId)
        {
            ViewBag.PublishingId = publishingId;
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

        // POST: PublishingPublications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int publishingId, [Bind("Id,Version,NamePublication,PageAmount")] Publication publication)
        {
            publication.PublishingId = publishingId;
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
                return RedirectToAction("Index", "PublishingPublications", new { id = publishingId, name = _context.PublishingCollection.Where(c => c.Id == publishingId).FirstOrDefault().PublishingName });

            }
            ViewData["PublishingId"] = new SelectList(_context.PublishingCollection, "Id", "PublishingName", publication.PublishingId);
            return RedirectToAction("Index", "PublishingPublications", new { id = publishingId, name = _context.PublishingCollection.Where(c => c.Id == publishingId).FirstOrDefault().PublishingName });

        }

        // GET: PublishingPublications/Delete/5
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

        // POST: PublishingPublications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
