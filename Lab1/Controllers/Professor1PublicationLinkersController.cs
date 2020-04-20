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
    public class Professor1PublicationLinkersController : Controller
    {
        private readonly Professor_PublicationContext _context;

        public Professor1PublicationLinkersController(Professor_PublicationContext context)
        {
            _context = context;
        }

        // GET: Professor1PublicationLinkers
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "PublishingPublications");
            ViewBag.PublicationnId = id;
            ViewBag.PublicationnName = name;
            var professorsByPublication = _context.ProfessorPublicationLinker.Where(c => c.PublicationId == id).Include(p => p.Professor).Include(p => p.Publication).Include(b => b.Professor.Degree).Include(b => b.Professor.PlaceOfWorking).Include(b => b.Professor.PlaceOfWorking.Cathedra).Include(b => b.Professor.PlaceOfWorking.Cathedra.Faculty); ;
            return View(await professorsByPublication.ToListAsync());
        }

        // GET: Professor1PublicationLinkers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professorPublicationLinker = await _context.ProfessorPublicationLinker
                .Include(p => p.Professor)
                .Include(p => p.Publication)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professorPublicationLinker == null)
            {
                return NotFound();
            }

            return View(professorPublicationLinker);
        }

        // GET: Professor1PublicationLinkers/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create(int publicationnId)
        {
            ViewBag.PublicationnId = publicationnId;
            ViewBag.PublicationnName = _context.Publication.Where(c => c.Id == publicationnId).FirstOrDefault().NamePublication;
            ViewData["ProfessorId"] = new SelectList(_context.Professor, "Id", "Name");
            //ViewData["PublicationId"] = new SelectList(_context.Publication, "Id", "NamePublication");
            return View();
        }

        // POST: Professor1PublicationLinkers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create( int publicationnId, [Bind("Id,ProfessorId,PublicationId")] ProfessorPublicationLinker professorPublicationLinker)
        {
            professorPublicationLinker.PublicationId = publicationnId;
            if (ModelState.IsValid)
            {
                _context.Add(professorPublicationLinker);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Professor1PublicationLinkers", new { id = publicationnId,
                                        name = _context.Publication.Where(c => c.Id == publicationnId).FirstOrDefault().NamePublication});
            }
           ViewData["ProfessorId"] = new SelectList(_context.Professor, "Id", "Name", professorPublicationLinker.ProfessorId);
           // ViewData["PublicationId"] = new SelectList(_context.Publication, "Id", "NamePublication", professorPublicationLinker.PublicationId);
            return RedirectToAction("Index", "Professor1PublicationLinkers", new
            {
                id = publicationnId,
                name = _context.Publication.Where(c => c.Id == publicationnId).FirstOrDefault().NamePublication
            });

        }

        // GET: Professor1PublicationLinkers/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id, int? publicationId)
        {
            ViewBag.PublicationnId = publicationId;
            if (id == null)
            {
                return NotFound();
            }

            var professorPublicationLinker = await _context.ProfessorPublicationLinker.FindAsync(id);
            if (professorPublicationLinker == null)
            {
                return NotFound();
            }
            ViewData["ProfessorId"] = new SelectList(_context.Professor, "Id", "Name", professorPublicationLinker.ProfessorId);
            ViewData["PublicationId"] = new SelectList(_context.Publication, "Id", "NamePublication", professorPublicationLinker.PublicationId);
            return View(professorPublicationLinker);
        }

        // POST: Professor1PublicationLinkers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id,int publicationId, [Bind("Id,ProfessorId")] ProfessorPublicationLinker professorPublicationLinker)
        {
            professorPublicationLinker.PublicationId = publicationId;
            if (id != professorPublicationLinker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professorPublicationLinker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorPublicationLinkerExists(professorPublicationLinker.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Professor1PublicationLinkers", new
                {
                    id = publicationId,
                    name = _context.Publication.Where(c => c.Id == publicationId).FirstOrDefault().NamePublication
                });
            }
            ViewData["ProfessorId"] = new SelectList(_context.Professor, "Id", "Name", professorPublicationLinker.ProfessorId);
            ViewData["PublicationId"] = new SelectList(_context.Publication, "Id", "NamePublication", professorPublicationLinker.PublicationId);
            return RedirectToAction("Index", "Professor1PublicationLinkers", new
            {
                id = publicationId,
                name = _context.Publication.Where(c => c.Id == publicationId).FirstOrDefault().NamePublication
            });
        }

        // GET: Professor1PublicationLinkers/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professorPublicationLinker = await _context.ProfessorPublicationLinker
                .Include(p => p.Professor)
                .Include(p => p.Publication)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professorPublicationLinker == null)
            {
                return NotFound();
            }

            return View(professorPublicationLinker);
        }

        // POST: Professor1PublicationLinkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professorPublicationLinker = await _context.ProfessorPublicationLinker.FindAsync(id);
            _context.ProfessorPublicationLinker.Remove(professorPublicationLinker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorPublicationLinkerExists(int id)
        {
            return _context.ProfessorPublicationLinker.Any(e => e.Id == id);
        }
    }
}
