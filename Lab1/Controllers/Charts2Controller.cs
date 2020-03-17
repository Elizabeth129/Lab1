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
    public class Charts2Controller : ControllerBase
    {
        private readonly Professor_PublicationContext _context;

        public Charts2Controller(Professor_PublicationContext context)
        {
            _context = context;
        }


        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var publishings = _context.PublishingCollection.Include(b => b.Publication).ToList();
            List<object> publPublication = new List<object>();
            publPublication.Add(new[] { "Видавництво", "Кількість публікацій" });

            foreach (var c in publishings)
            {
                publPublication.Add(new object[] { c.PublishingName, c.Publication.Count() });
            }
            return new JsonResult(publPublication);
        }
    }
}