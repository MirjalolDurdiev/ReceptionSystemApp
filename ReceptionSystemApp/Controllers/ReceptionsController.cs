using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptionSystemApp.Data;
using ReceptionSystemApp.Models;

namespace ReceptionSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionsController : ControllerBase
    {
        private readonly ReceptionDbContext _context;

        public ReceptionsController(ReceptionDbContext context)
        {
            _context = context;
        }

        // GET: api/Receptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reception>>> GetReceptions()
        {
            return await _context.Receptions.ToListAsync();
        }

        // GET: api/Receptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reception>> GetReception(int id)
        {
            var reception = await _context.Receptions.FindAsync(id);

            if (reception == null)
            {
                return NotFound();
            }

            return reception;
        }

        // PUT: api/Receptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReception(int id, Reception reception)
        {
            if (id != reception.Id)
            {
                return BadRequest();
            }

            _context.Entry(reception).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Receptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reception>> PostReception(Reception reception)
        {
            _context.Receptions.Add(reception);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReception", new { id = reception.Id }, reception);
        }

        // DELETE: api/Receptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReception(int id)
        {
            var reception = await _context.Receptions.FindAsync(id);
            if (reception == null)
            {
                return NotFound();
            }

            _context.Receptions.Remove(reception);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceptionExists(int id)
        {
            return _context.Receptions.Any(e => e.Id == id);
        }
    }
}
