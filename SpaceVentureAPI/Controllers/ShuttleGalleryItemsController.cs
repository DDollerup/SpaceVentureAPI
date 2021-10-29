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
    public class ShuttleGalleryItemsController : ControllerBase
    {
        private readonly SpaceVentureContext _context;
        private readonly IWebHostEnvironment _env;

        public ShuttleGalleryItemsController(SpaceVentureContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/ShuttleGalleryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShuttleGalleryItem>>> GetShuttleGalleryItems()
        {
            return await _context.ShuttleGalleryItems.ToListAsync();
        }

        // GET: api/ShuttleGalleryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShuttleGalleryItem>> GetShuttleGalleryItem(int id)
        {
            var shuttleGalleryItem = await _context.ShuttleGalleryItems.FindAsync(id);

            if (shuttleGalleryItem == null)
            {
                return NotFound();
            }

            return shuttleGalleryItem;
        }

        // PUT: api/ShuttleGalleryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShuttleGalleryItem(int id, ShuttleGalleryItem shuttleGalleryItem)
        {
            if (id != shuttleGalleryItem.Id)
            {
                return BadRequest();
            }

            if (Tools.IsBase64String(shuttleGalleryItem.Image))
            {
                Tools.DeleteFile(_env.WebRootPath, _context.ShuttleGalleryItems.AsNoTracking().FirstOrDefault(e => e.Id == id).Image);
                shuttleGalleryItem.Image = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value + "/" + Tools.ConvertBase64ToFile(shuttleGalleryItem.Image, _env.WebRootPath);
            }

            _context.Entry(shuttleGalleryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShuttleGalleryItemExists(id))
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

        // POST: api/ShuttleGalleryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShuttleGalleryItem>> PostShuttleGalleryItem(ShuttleGalleryItem shuttleGalleryItem)
        {
            if (Tools.IsBase64String(shuttleGalleryItem.Image))
            {
                shuttleGalleryItem.Image = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value + "/" + Tools.ConvertBase64ToFile(shuttleGalleryItem.Image, _env.WebRootPath);
            }

            _context.ShuttleGalleryItems.Add(shuttleGalleryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShuttleGalleryItem", new { id = shuttleGalleryItem.Id }, shuttleGalleryItem);
        }

        // DELETE: api/ShuttleGalleryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShuttleGalleryItem(int id)
        {
            var shuttleGalleryItem = await _context.ShuttleGalleryItems.FindAsync(id);
            if (shuttleGalleryItem == null)
            {
                return NotFound();
            }

            _context.ShuttleGalleryItems.Remove(shuttleGalleryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShuttleGalleryItemExists(int id)
        {
            return _context.ShuttleGalleryItems.Any(e => e.Id == id);
        }
    }
}
