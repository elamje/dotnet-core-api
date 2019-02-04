using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pdfController : ControllerBase
    {
        private readonly pdfContext _context;

        public pdfController(pdfContext context)
        {
            _context = context;

            if (_context.pdfs.Count() == 0)
            {
                // Create a new pdf if collection is empty,
                // which means you can't delete all pdfs.
                _context.pdfs.Add(new pdf { Name = "pdf1" });
                _context.SaveChanges();
            }
        }

        // GET: api/pdf
        [HttpGet]
        public async Task<ActionResult<IEnumerable<pdf>>> Getpdfs()
        {
            return await _context.pdfs.ToListAsync();
        }

        // GET: api/pdf/5
        [HttpGet("{id}")]
        public async Task<ActionResult<pdf>> Getpdf(long id)
        {
            var pdf = await _context.pdfs.FindAsync(id);

            if (pdf == null)
            {
                return NotFound();
            }

            return pdf;
        }

        // POST: api/pdf
        [HttpPost]
        public async Task<ActionResult<pdf>> Postpdf(pdf pdf)
        {
            _context.pdfs.Add(pdf);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Getpdf), new pdf{ Id = pdf.Id}, pdf);
        }

        // PUT: api/pdf/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpdf(long id, pdf pdf)
        {
            if (id != pdf.Id)
            {
                return BadRequest();
            }

            _context.Entry(pdf).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/pdf/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepdf(long id)
        {
            var pdf = await _context.pdfs.FindAsync(id);
            if (pdf == null)
            {
                return NotFound();
            }
            _context.pdfs.Remove(pdf);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}