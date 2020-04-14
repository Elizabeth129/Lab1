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
    public class PlaceOfWorksController : Controller
    {
        private readonly Professor_PublicationContext _context;

        public PlaceOfWorksController(Professor_PublicationContext context)
        {
            _context = context;
        }

        // GET: PlaceOfWorks
        public async Task<IActionResult> Index(int? id, string? name, string? facultyname)
        {
            if (id == null) return RedirectToAction("Index","Cathedras");
            ViewBag.CathedraId = id;
            ViewBag.CathedraName = name;
            ViewBag.FacultyName = facultyname;
            var placeOfWorksByCathedra = _context.PlaceOfWork.Where(b => b.CathedraId == id).Include(p => p.Cathedra);
            return View(await placeOfWorksByCathedra.ToListAsync());
        }

        // GET: PlaceOfWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeOfWork = await _context.PlaceOfWork
                .Include(p => p.Cathedra)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeOfWork == null)
            {
                return NotFound();
            }

            return View(placeOfWork);
        }

        // GET: PlaceOfWorks/Create
        public IActionResult Create( int cathedraId)
        {
            ViewBag.CathedraId = cathedraId;
            ViewBag.CathedraName = _context.Cathedra.Where(c => c.Id == cathedraId).FirstOrDefault().CathedraName;

           // ViewData["CathedraId"] = new SelectList(_context.Cathedra, "Id", "CathedraName");
            return View();
        }
   


        // POST: PlaceOfWorks/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int cathedraId, [Bind("Id,CathedraId,DateOfStartWork,DateOfEndWork")] PlaceOfWork placeOfWork)
        {
            placeOfWork.CathedraId = cathedraId;
           // ViewBag.CathedraId = cathedraId;

            if (ModelState.IsValid)
            {

                DateTime time = DateTime.Now;
                DateTime time1 = placeOfWork.DateOfStartWork;
                DateTime time2 = placeOfWork.DateOfEndWork.Value;

                if (DateTime.Compare(time, time1) <= 0)
                {
                    ViewData["CathedraId"] = cathedraId;
                    ModelState.AddModelError("DateOfStartWork", "DateOfStartWork error");
                    return View(placeOfWork);
                }
                if (DateTime.Compare(time, time2) <= 0)
                {
                     ViewData["CathedraId"] = cathedraId;
                    ModelState.AddModelError("DateOfEndWork", "DateOfEndWork error");
                    return View(placeOfWork);
                }
                if (DateTime.Compare(time2, time1) <= 0)
                {
                    ViewData["CathedraId"] = cathedraId;
                    ModelState.AddModelError("DateOfEndWork", "DateOfEndWork error");
                    return View(placeOfWork);
                }
                _context.Add(placeOfWork);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "PlaceOfWorks", new {id = cathedraId, name = _context.Cathedra.Where(c => c.Id == cathedraId).FirstOrDefault().CathedraName });
            }
            
            return RedirectToAction("Index", "PlaceOfWorks", new { id = cathedraId, name = _context.Cathedra.Where(c => c.Id == cathedraId).FirstOrDefault().CathedraName });
        }

        // GET: PlaceOfWorks/Edit/5
        public async Task<IActionResult> Edit(int? id, int? cathedraId)
        {
            ViewBag.CathedraId = cathedraId;
            if (id == null)
            {
                return NotFound();
            }

            var placeOfWork = await _context.PlaceOfWork.FindAsync(id);
            if (placeOfWork == null)
            {
                return NotFound();
            }
            ViewData["CathedraId"] = new SelectList(_context.Cathedra, "Id", "CathedraName", placeOfWork.CathedraId);
            return View(placeOfWork);
        }

        // POST: PlaceOfWorks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int cathedraId, [Bind("Id,DateOfStartWork,DateOfEndWork")] PlaceOfWork placeOfWork)
        {
            placeOfWork.CathedraId = cathedraId;
            if (id != placeOfWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                DateTime time = DateTime.Now;
                DateTime time1 = placeOfWork.DateOfStartWork;
                DateTime time2 = placeOfWork.DateOfEndWork.Value;

                if (DateTime.Compare(time, time1) <= 0)
                {
                    ViewData["CathedraId"] = cathedraId;
                    ModelState.AddModelError("DateOfStartWork", "DateOfStartWork error");
                    return View(placeOfWork);
                }
                if (DateTime.Compare(time, time2) <= 0)
                {
                    ViewData["CathedraId"] = cathedraId;
                    ModelState.AddModelError("DateOfEndWork", "DateOfEndWork error");
                    return View(placeOfWork);
                }
                if (DateTime.Compare(time2, time1) <= 0)
                {
                    ViewData["CathedraId"] = cathedraId;
                    ModelState.AddModelError("DateOfEndWork", "DateOfEndWork error");
                    return View(placeOfWork);
                }
                try
                {
                    _context.Update(placeOfWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceOfWorkExists(placeOfWork.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "PlaceOfWorks", new { id = cathedraId, name = _context.Cathedra.Where(c => c.Id == cathedraId).FirstOrDefault().CathedraName });

            }
            ViewData["CathedraId"] = new SelectList(_context.Cathedra, "Id", "CathedraName", placeOfWork.CathedraId);
            return RedirectToAction("Index", "PlaceOfWorks", new { id = cathedraId, name = _context.Cathedra.Where(c => c.Id == cathedraId).FirstOrDefault().CathedraName });

        }

        // GET: PlaceOfWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placeOfWork = await _context.PlaceOfWork
                .Include(p => p.Cathedra)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeOfWork == null)
            {
                return NotFound();
            }

            return View(placeOfWork);
        }

        // POST: PlaceOfWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placeOfWork = await _context.PlaceOfWork.FindAsync(id);
            _context.PlaceOfWork.Remove(placeOfWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceOfWorkExists(int id)
        {
            return _context.PlaceOfWork.Any(e => e.Id == id);
        }
    }
}
