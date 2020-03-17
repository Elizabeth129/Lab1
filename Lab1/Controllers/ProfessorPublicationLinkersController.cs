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
    public class ProfessorPublicationLinkersController : Controller
    {
        private readonly Professor_PublicationContext _context;

        public ProfessorPublicationLinkersController(Professor_PublicationContext context)
        {
            _context = context;
        }

        // GET: ProfessorPublicationLinkers
        public async Task<IActionResult> Index(int? id, string? name, string? surname)
        {
            if (id == null) return RedirectToAction("Index","Professors");
            ViewBag.ProfessorId = id;
            ViewBag.ProfessorName = name;
            ViewBag.ProfessorSurname = surname;

            var publicationsByProfessor = _context.ProfessorPublicationLinker.Where(c => c.ProfessorId == id).Include(p => p.Professor).Include(p => p.Publication).Include(p => p.Publication.Publishing);
            return View(await publicationsByProfessor.ToListAsync());
        }

        // GET: ProfessorPublicationLinkers/Details/5
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

        // GET: ProfessorPublicationLinkers/Create
        public IActionResult Create(int professorId)
        {
            // ViewData["ProfessorId"] = new SelectList(_context.Professor, "Id", "Name");
            ViewBag.ProfessorId = professorId;
            ViewBag.ProfessorName = _context.Professor.Where(c => c.Id == professorId).FirstOrDefault().Name;
            ViewBag.ProfessorSurname = _context.Professor.Where(c => c.Id == professorId).FirstOrDefault().Surname;
            ViewData["PublicationId"] = new SelectList(_context.Publication, "Id", "NamePublication");
            return View();
        }

        // POST: ProfessorPublicationLinkers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int professorId, [Bind("Id,ProfessorId,PublicationId")] ProfessorPublicationLinker professorPublicationLinker)
        {
            professorPublicationLinker.ProfessorId = professorId;
            if (ModelState.IsValid)
            {
                _context.Add(professorPublicationLinker);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "ProfessorPublicationLinkers", new { id = professorId,
                                                                      name = _context.Professor.Where(c => c.Id == professorId).FirstOrDefault().Name,
                                                                      surname = _context.Professor.Where(c => c.Id == professorId).FirstOrDefault().Surname
                });
            }
            //ViewData["ProfessorId"] = new SelectList(_context.Professor, "Id", "Name", professorPublicationLinker.ProfessorId);
            ViewData["PublicationId"] = new SelectList(_context.Publication, "Id", "NamePublication", professorPublicationLinker.PublicationId);
            //return View(professorPublicationLinker);
            return RedirectToAction("Index", "ProfessorPublicationLinkers", new
            {
                id = professorId,
                name = _context.Professor.Where(c => c.Id == professorId).FirstOrDefault().Name,
                surname = _context.Professor.Where(c => c.Id == professorId).FirstOrDefault().Surname
            });
        }

        // GET: ProfessorPublicationLinkers/Edit/5
        public async Task<IActionResult> Edit(int? id, int? professorId)
        {
            ViewBag.ProfessorId = professorId;
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

        // POST: ProfessorPublicationLinkers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int professorId, [Bind("Id,PublicationId")] ProfessorPublicationLinker professorPublicationLinker)
        {
            professorPublicationLinker.ProfessorId = professorId;
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
                return RedirectToAction("Index", "ProfessorPublicationLinkers", new
                {
                    id = professorId,
                    name = _context.Professor.Where(c => c.Id == professorId).FirstOrDefault().Name,
                    surname = _context.Professor.Where(c => c.Id == professorId).FirstOrDefault().Surname
                });
            }
            ViewData["ProfessorId"] = new SelectList(_context.Professor, "Id", "Name", professorPublicationLinker.ProfessorId);
            ViewData["PublicationId"] = new SelectList(_context.Publication, "Id", "NamePublication", professorPublicationLinker.PublicationId);
            return RedirectToAction("Index", "ProfessorPublicationLinkers", new
            {
                id = professorId,
                name = _context.Professor.Where(c => c.Id == professorId).FirstOrDefault().Name,
                surname = _context.Professor.Where(c => c.Id == professorId).FirstOrDefault().Surname
            });
        }

        // GET: ProfessorPublicationLinkers/Delete/5
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

        // POST: ProfessorPublicationLinkers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
