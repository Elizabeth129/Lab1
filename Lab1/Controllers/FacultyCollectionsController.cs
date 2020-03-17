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
    public class FacultyCollectionsController : Controller
    {
        private readonly Professor_PublicationContext _context;

        public FacultyCollectionsController(Professor_PublicationContext context)
        {
            _context = context;
        }
        public IActionResult CheckFaculty(string facultyName)
        {
            var collection = _context.DegreeCollection.ToArray();
            var reg = (from u in collection
                       where u.DegreeName.ToUpper() == facultyName.ToUpper()
                       select new { facultyName }).FirstOrDefault();

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
        // GET: FacultyCollections
        public async Task<IActionResult> Index()
        {
            return View(await _context.FacultyCollection.ToListAsync());
        }

        // GET: FacultyCollections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyCollection = await _context.FacultyCollection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyCollection == null)
            {
                return NotFound();
            }

            //return View(facultyCollection);
            return RedirectToAction("Index", "Cathedras", new { id = facultyCollection.Id, name =facultyCollection.FacultyName});
        }

        // GET: FacultyCollections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FacultyCollections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FacultyName")] FacultyCollection facultyCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facultyCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facultyCollection);
        }

        // GET: FacultyCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyCollection = await _context.FacultyCollection.FindAsync(id);
            if (facultyCollection == null)
            {
                return NotFound();
            }
            return View(facultyCollection);
        }

        // POST: FacultyCollections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FacultyName")] FacultyCollection facultyCollection)
        {
            if (id != facultyCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultyCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyCollectionExists(facultyCollection.Id))
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
            return View(facultyCollection);
        }

        // GET: FacultyCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyCollection = await _context.FacultyCollection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyCollection == null)
            {
                return NotFound();
            }

            return View(facultyCollection);
        }

        // POST: FacultyCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facultyCollection = await _context.FacultyCollection.FindAsync(id);
            _context.FacultyCollection.Remove(facultyCollection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyCollectionExists(int id)
        {
            return _context.FacultyCollection.Any(e => e.Id == id);
        }
    }
}
