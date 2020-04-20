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
    public class ProfessorsController : Controller
    {
        private readonly Professor_PublicationContext _context;

        public ProfessorsController(Professor_PublicationContext context)
        {
            _context = context;
        }
        public IActionResult CheckNumber(int personalNumber)
        {
            var collection = _context.Professor.ToArray();
            var reg = (from u in collection
                       where u.PersonalNumber == personalNumber
                       select new { personalNumber }).FirstOrDefault();

            if (reg != null) return Json(false);
            /* foreach (var elem in collection)
             {
                 if (elem.DegreeName.Equals(degreeName))
                 {
                     return Json(false);
                 }
             }*/
            return Json(true);
        }

        // GET: Professors
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index","DegreeCollections");
            ViewBag.DegreeCollectionId = id;
            ViewBag.DegreeCollectionName = name;
            var professorsByDegree = _context.Professor.Where(b => b.DegreeId == id).Include(b => b.Degree).Include(b => b.PlaceOfWorking).Include(b => b.PlaceOfWorking.Cathedra).Include(b => b.PlaceOfWorking.Cathedra.Faculty);
            return View(await professorsByDegree.ToListAsync());
        }

        // GET: Professors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .Include(p => p.Degree)
                .Include(p => p.PlaceOfWorking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professor == null)
            {
                return NotFound();
            }

            //return View(professor);
            return RedirectToAction("Index", "ProfessorPublicationLinkers", new {id = professor.Id, name = professor.Name, surname = professor.Surname });
        }

        // GET: Professors/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create(int degreeCollectionId)
        {
            // ViewData["DegreeId"] = new SelectList(_context.DegreeCollection, "Id", "DegreeName");
            ViewBag.DegreeCollectionId = degreeCollectionId;
            ViewBag.DegreeCollectionName = _context.DegreeCollection.Where(c => c.Id == degreeCollectionId).FirstOrDefault().DegreeName;
            ViewData["PlaceOfWorkingId"] = new SelectList(_context.PlaceOfWork, "Id", "Id");
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(int degreeId, [Bind("Id,Name,Surname,DateOfBirth,PersonalNumber,DegreeId,PlaceOfWorkingId")] Professor professor)
        {
            professor.DegreeId = degreeId;
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Professors", new { id = degreeId, name = _context.DegreeCollection.Where(c => c.Id == degreeId).FirstOrDefault().DegreeName });
            }
            //ViewData["DegreeId"] = new SelectList(_context.DegreeCollection, "Id", "DegreeName", professor.DegreeId);
            //ViewData["PlaceOfWorkingId"] = new SelectList(_context.PlaceOfWork, "Id", "Id", professor.PlaceOfWorkingId);
            //return View(professor);
            return RedirectToAction("Index", "Professors", new { id = degreeId, name = _context.DegreeCollection.Where(c => c.Id == degreeId).FirstOrDefault().DegreeName });
        }

        // GET: Professors/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id, int? degreeId)
        {
            ViewBag.DegreeId = degreeId;
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            ViewData["DegreeId"] = new SelectList(_context.DegreeCollection, "Id", "DegreeName", professor.DegreeId);
            ViewData["PlaceOfWorkingId"] = new SelectList(_context.PlaceOfWork, "Id", "Id", professor.PlaceOfWorkingId);
            return View(professor);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, int degreeId, [Bind("Id,Name,Surname,DateOfBirth,PersonalNumber,PlaceOfWorkingId")] Professor professor)
        {
            professor.DegreeId = degreeId;
            if (id != professor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Professors", new { id = degreeId, name = _context.DegreeCollection.Where(c => c.Id == degreeId).FirstOrDefault().DegreeName });

            }
           // ViewData["DegreeId"] = new SelectList(_context.DegreeCollection, "Id", "DegreeName", professor.DegreeId);
            ViewData["PlaceOfWorkingId"] = new SelectList(_context.PlaceOfWork, "Id", "Id", professor.PlaceOfWorkingId);
            // return View(professor);
            return RedirectToAction("Index", "Professors", new { id = degreeId, name = _context.DegreeCollection.Where(c => c.Id == degreeId).FirstOrDefault().DegreeName });

        }

        // GET: Professors/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .Include(p => p.Degree)
                .Include(p => p.PlaceOfWorking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professor.FindAsync(id);
            _context.Professor.Remove(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.Id == id);
        }
    }
}
