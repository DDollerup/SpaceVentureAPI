using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceAdventureAPI;

namespace SpaceAdventureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SafetiesController : ControllerBase
    {
        private readonly SpaceVentureContext _context;

        public SafetiesController(SpaceVentureContext context)
        {
            _context = context;
        }

        // GET: api/Safeties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Safety>>> GetSafeties()
        {
            return await _context.Safeties.ToListAsync();
        }

        // GET: api/Safeties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Safety>> GetSafety(int id)
        {
            var safety = await _context.Safeties.FindAsync(id);

            if (safety == null)
            {
                return NotFound();
            }

            return safety;
        }

        // PUT: api/Safeties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSafety(int id, Safety safety)
        {
            if (id != safety.Id)
            {
                return BadRequest();
            }

            _context.Entry(safety).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SafetyExists(id))
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

        // POST: api/Safeties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Safety>> PostSafety(Safety safety)
        {
            _context.Safeties.Add(safety);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSafety", new { id = safety.Id }, safety);
        }

        // DELETE: api/Safeties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSafety(int id)
        {
            var safety = await _context.Safeties.FindAsync(id);
            if (safety == null)
            {
                return NotFound();
            }

            _context.Safeties.Remove(safety);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SafetyExists(int id)
        {
            return _context.Safeties.Any(e => e.Id == id);
        }
    }
}
