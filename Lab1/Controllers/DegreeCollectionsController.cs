using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab1;
using Microsoft.AspNetCore.Http;
using System.IO;
using ClosedXML.Excel;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                DegreeCollection newdegree;
                                var c = (from deg in _context.DegreeCollection
                                         where deg.DegreeName.Contains(worksheet.Name)
                                         select deg).ToList();
                                if (c.Count > 0)
                                {
                                    newdegree = c[0];
                                }
                                else

                                {
                                    newdegree = new DegreeCollection();
                                    newdegree.DegreeName = worksheet.Name;
                                    _context.DegreeCollection.Add(newdegree);
                                }
                                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                {
                                    try
                                    {
                                        Professor professor = new Professor();
                                        professor.Name = row.Cell(1).Value.ToString();
                                        professor.Surname = row.Cell(2).Value.ToString();
                                        professor.DateOfBirth = Convert.ToDateTime(row.Cell(3).Value.ToString());
                                        professor.PersonalNumber = Convert.ToInt32(row.Cell(4).Value.ToString());
                                        professor.PlaceOfWorkingId = Convert.ToInt32(row.Cell(5).Value.ToString());
                                        professor.Degree = newdegree;

                                        var pr = (from prof in _context.Professor
                                                  where prof.PersonalNumber == professor.PersonalNumber
                                                  select prof).ToList();

                                        if (pr.Count > 0)
                                        {
                                            professor = pr[0];
                                        }
                                        else

                                        {
                                            _context.Professor.Add(professor);
                                        }

                                        int i = 6;
                                        while (row.Cell(i).Value.ToString().Length > 0)
                                        {
                                            Publication publication;
                                            var p = (from publ in _context.Publication
                                                     where
                                                        (publ.NamePublication.Contains(row.Cell(i).Value.ToString()) &&
                                                        publ.Version == Convert.ToInt32(row.Cell(i + 1).Value.ToString()) &&
                                                         publ.PublishingId == Convert.ToInt32(row.Cell(i + 2).Value.ToString()))
                                                     select publ).ToList();
                                            if (p.Count > 0)
                                            {
                                                publication = p[0];
                                            }
                                            else
                                            {
                                                publication = new Publication();
                                                publication.NamePublication = row.Cell(i).Value.ToString();
                                                publication.Version = Convert.ToInt32(row.Cell(i + 1).Value.ToString());
                                                publication.PublishingId = Convert.ToInt32(row.Cell(i + 2).Value.ToString());
                                                publication.PageAmount = Convert.ToInt32(row.Cell(i + 3).Value.ToString());
                                                _context.Publication.Add(publication);
                                            }
                                            ProfessorPublicationLinker pp = new ProfessorPublicationLinker();
                                            pp.Professor = professor;
                                            pp.Publication = publication;

                                            var ppp = (from prof in _context.ProfessorPublicationLinker
                                                      where (prof.Professor == professor && prof.Publication == publication)
                                                      select prof).ToList();

                                            if (ppp.Count > 0)
                                            {
                                                pp = ppp[0];
                                            }
                                            else

                                            {
                                                _context.ProfessorPublicationLinker.Add(pp);
                                            }

                                            
                                            i += 4;
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var degrees = _context.DegreeCollection.Include("Professor").ToList();
                foreach (var d in degrees)
                {
                    var worksheet = workbook.Worksheets.Add(d.DegreeName);

                    worksheet.Cell("A1").Value = "Ім'я";
                    worksheet.Cell("B1").Value = "Прізвище";
                    worksheet.Cell("C1").Value = "Дата народження";
                    worksheet.Cell("D1").Value = "Код";
                    worksheet.Cell("E1").Value = "Місце роботи";

                    worksheet.Row(1).Style.Font.Bold = true;

                    var profesors = d.Professor.ToList();

                    for (int i = 0; i < profesors.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = profesors[i].Name;
                        worksheet.Cell(i + 2, 2).Value = profesors[i].Surname;
                        worksheet.Cell(i + 2, 3).DataType = XLDataType.Text;
                        worksheet.Column(3).Width = 10;
                        worksheet.Cell(i + 2, 3).Value = profesors[i].DateOfBirth;
                        worksheet.Cell(i + 2, 4).Value = profesors[i].PersonalNumber;
                        worksheet.Cell(i + 2, 5).Value = profesors[i].PlaceOfWorkingId;

                        var pp = _context.ProfessorPublicationLinker.Where(a => a.ProfessorId == profesors[i].Id).Include("Publication").ToList();
                        int j = 6;
                        foreach (var p in pp)
                        {
                            worksheet.Cell(1, j).Value = "Назва публікації";
                            worksheet.Cell(1, j + 1).Value = "Версія";
                            worksheet.Cell(1, j + 2).Value = "Видавництво";
                            worksheet.Cell(1, j + 3).Value = "Кількість сторінок";

                            worksheet.Cell(i + 2, j).Value = p.Publication.NamePublication;
                            worksheet.Cell(i + 2, j + 1).Value = p.Publication.Version;
                            worksheet.Cell(i + 2, j + 2).Value = p.Publication.PublishingId;
                            worksheet.Cell(i + 2, j + 3).Value = p.Publication.PageAmount;

                            j += 4;
                        }
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"ProfessorsPublication_{DateTime.UtcNow.ToShortDateString()}.xlsx"

                    };
                }
            }
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
