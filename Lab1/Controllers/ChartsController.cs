using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly Professor_PublicationContext _context;

        public ChartsController(Professor_PublicationContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var degrees = _context.DegreeCollection.Include(b => b.Professor).ToList();
            List<object> degProfessor = new List<object>();
            degProfessor.Add(new[] { "Науковий ступінь", "Кількість науковців" });

            foreach(var c in degrees)
            {
                degProfessor.Add(new object[] { c.DegreeName, c.Professor.Count() });
            }
            return new JsonResult(degProfessor);
        }

       
    }
}