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
    public class CathedrasController : Controller
    {
        private readonly Professor_PublicationContext _context;

        public CathedrasController(Professor_PublicationContext context)
        {
            _context = context;
        }
        public IActionResult CheckCathedra(string cathedraName)
        {
            var collection = _context.Cathedra.ToArray();

                //int faculty = Convert.ToInt32(HttpContext.Request.Query["facultyId"].First());
            foreach (var elem in collection)
            {
                if (elem.CathedraName.Equals(cathedraName)) return Json(false);
            }

            return Json(true);
        }
        // GET: Cathedras
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "FacultyCollections");
            ViewBag.FacultyId = id;
            ViewBag.FacultyName = name;
            var cathedras_byFaculty = _context.Cathedra.Where(c => c.FacultyId == id).Include(b => b.Faculty);
            return View(await cathedras_byFaculty.ToListAsync());
        }

        // GET: Cathedras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cathedra = await _context.Cathedra
                .Include(c => c.Faculty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cathedra == null)
            {
                return NotFound();
            }
            
            //return View(cathedra);
            return RedirectToAction("Index", "PlaceOfWorks", new { id = cathedra.Id, name = cathedra.CathedraName, facultyname =  _context.FacultyCollection.Where(c => c.Id == cathedra.FacultyId).FirstOrDefault().FacultyName });
        }

        // GET: Cathedras/Create
        public IActionResult Create(int facultyId)
        {
            // ViewData["FacultyId"] = new SelectList(_context.FacultyCollection, "Id", "FacultyName");
            ViewBag.FacultyId  = facultyId;
            ViewBag.FacultyName = _context.FacultyCollection.Where(c => c.Id == facultyId).FirstOrDefault().FacultyName;
            return View();
        }

        // POST: Cathedras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int facultyId, [Bind("Id,FacultyId,CathedraName")] Cathedra cathedra)
        {
            cathedra.FacultyId = facultyId;
            if (ModelState.IsValid)
            {
                _context.Add(cathedra);
                await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Cathedras", new { id = facultyId, name = _context.FacultyCollection.Where(c => c.Id == facultyId).FirstOrDefault().FacultyName });
            }
            // ViewData["FacultyId"] = new SelectList(_context.FacultyCollection, "Id", "FacultyName", cathedra.FacultyId);
            //return View(cathedra);
            return RedirectToAction("Index", "Cathedras", new { id = facultyId, name = _context.FacultyCollection.Where(c => c.Id == facultyId).FirstOrDefault().FacultyName });
        }

        // GET: Cathedras/Edit/5
        public async Task<IActionResult> Edit(int? id, int? facultyId)
        {
            ViewBag.FacultyId = facultyId;
            if (id == null)
            {
                return NotFound();
            }

            var cathedra = await _context.Cathedra.FindAsync(id);
            if (cathedra == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.FacultyCollection, "Id", "FacultyName", cathedra.FacultyId);
            return View(cathedra);
        }

        // POST: Cathedras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int facultyId, [Bind("Id,CathedraName")] Cathedra cathedra)
        {
            cathedra.FacultyId = facultyId;
            if (id != cathedra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cathedra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CathedraExists(cathedra.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Cathedras", new { id = facultyId, name = _context.FacultyCollection.Where(c => c.Id == facultyId).FirstOrDefault().FacultyName });
            }
            ViewData["FacultyId"] = new SelectList(_context.FacultyCollection, "Id", "FacultyName", cathedra.FacultyId);
            return RedirectToAction("Index", "Cathedras", new { id = facultyId, name = _context.FacultyCollection.Where(c => c.Id == facultyId).FirstOrDefault().FacultyName });

        }

        // GET: Cathedras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cathedra = await _context.Cathedra
                .Include(c => c.Faculty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cathedra == null)
            {
                return NotFound();
            }

            return View(cathedra);
        }

        // POST: Cathedras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cathedra = await _context.Cathedra.FindAsync(id);
            _context.Cathedra.Remove(cathedra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CathedraExists(int id)
        {
            return _context.Cathedra.Any(e => e.Id == id);
        }
    }
}
