using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceAdventureAPI;

namespace SpaceAdventureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShuttlesController : ControllerBase
    {
        private readonly SpaceVentureContext _context;
        private readonly IWebHostEnvironment _env;


        public ShuttlesController(SpaceVentureContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Shuttles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shuttle>>> GetShuttles()
        {
            return await _context.Shuttles.ToListAsync();
        }

        // GET: api/Shuttles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shuttle>> GetShuttle(int id)
        {
            var shuttle = await _context.Shuttles.FindAsync(id);

            if (shuttle == null)
            {
                return NotFound();
            }

            return shuttle;
        }

        // PUT: api/Shuttles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShuttle(int id, Shuttle shuttle)
        {
            if (id != shuttle.Id)
            {
                return BadRequest();
            }

            if (Tools.IsBase64String(shuttle.Image))
            {
                Tools.DeleteFile(_env.WebRootPath, _context.Shuttles.AsNoTracking().FirstOrDefault(e => e.Id == id).Image);
                shuttle.Image = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value + "/" + Tools.ConvertBase64ToFile(shuttle.Image, _env.WebRootPath);
            }

            _context.Entry(shuttle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShuttleExists(id))
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

        // POST: api/Shuttles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shuttle>> PostShuttle(Shuttle shuttle)
        {
            if (Tools.IsBase64String(shuttle.Image))
            {
                shuttle.Image = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value + "/" + Tools.ConvertBase64ToFile(shuttle.Image, _env.WebRootPath);
            }

            _context.Shuttles.Add(shuttle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShuttle", new { id = shuttle.Id }, shuttle);
        }

        // DELETE: api/Shuttles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShuttle(int id)
        {
            var shuttle = await _context.Shuttles.FindAsync(id);
            if (shuttle == null)
            {
                return NotFound();
            }

            _context.Shuttles.Remove(shuttle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShuttleExists(int id)
        {
            return _context.Shuttles.Any(e => e.Id == id);
        }
    }
}
