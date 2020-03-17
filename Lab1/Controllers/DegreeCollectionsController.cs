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
    public class DegreeCollectionsController : Controller
    {
        private readonly Professor_PublicationContext _context;

        public DegreeCollectionsController(Professor_PublicationContext context)
        {
            _context = context;
        }
       
        public IActionResult CheckDegree(string degreeName)
        {
            var collection = _context.DegreeCollection.ToArray();
            var reg = (from u in collection
                              where u.DegreeName.ToUpper() == degreeName.ToUpper()
                              select new { degreeName }).FirstOrDefault();

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
        // GET: DegreeCollections
        public async Task<IActionResult> Index()
        {
            return View(await _context.DegreeCollection.ToListAsync());
        }

        // GET: DegreeCollections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeCollection = await _context.DegreeCollection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (degreeCollection == null)
            {
                return NotFound();
            }

            //return View(degreeCollection);
            return RedirectToAction("Index", "Professors", new { id = degreeCollection.Id, name = degreeCollection.DegreeName });
        }

        // GET: DegreeCollections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DegreeCollections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DegreeName")] DegreeCollection degreeCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(degreeCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(degreeCollection);
        }

        // GET: DegreeCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeCollection = await _context.DegreeCollection.FindAsync(id);
            if (degreeCollection == null)
            {
                return NotFound();
            }
            return View(degreeCollection);
        }

        // POST: DegreeCollections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DegreeName")] DegreeCollection degreeCollection)
        {
            if (id != degreeCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(degreeCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DegreeCollectionExists(degreeCollection.Id))
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
            return View(degreeCollection);
        }

        // GET: DegreeCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var degreeCollection = await _context.DegreeCollection
                .FirstOrDefaultAsync(m => m.Id == id);
            if (degreeCollection == null)
            {
                return NotFound();
            }

            return View(degreeCollection);
        }

        // POST: DegreeCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var degreeCollection = await _context.DegreeCollection.FindAsync(id);
            _context.DegreeCollection.Remove(degreeCollection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DegreeCollectionExists(int id)
        {
            return _context.DegreeCollection.Any(e => e.Id == id);
        }
    }
}
