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
    public class GalleryItemsController : ControllerBase
    {
        private readonly SpaceVentureContext _context;
        private readonly IWebHostEnvironment _env;


        public GalleryItemsController(SpaceVentureContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/GalleryItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GalleryItem>>> GetGalleryItems()
        {
            return await _context.GalleryItems.ToListAsync();
        }

        // GET: api/GalleryItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GalleryItem>> GetGalleryItem(int id)
        {
            var galleryItem = await _context.GalleryItems.FindAsync(id);

            if (galleryItem == null)
            {
                return NotFound();
            }

            return galleryItem;
        }

        // PUT: api/GalleryItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGalleryItem(int id, GalleryItem galleryItem)
        {
            if (id != galleryItem.Id)
            {
                return BadRequest();
            }

            if (Tools.IsBase64String(galleryItem.Image))
            {
                Tools.DeleteFile(_env.WebRootPath, _context.GalleryItems.AsNoTracking().FirstOrDefault(e => e.Id == id).Image);
                galleryItem.Image = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value + "/" + Tools.ConvertBase64ToFile(galleryItem.Image, _env.WebRootPath);
            }

            _context.Entry(galleryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GalleryItemExists(id))
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

        // POST: api/GalleryItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GalleryItem>> PostGalleryItem(GalleryItem galleryItem)
        {
            if (Tools.IsBase64String(galleryItem.Image))
            {
                galleryItem.Image = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value + "/" + Tools.ConvertBase64ToFile(galleryItem.Image, _env.WebRootPath);
            }

            _context.GalleryItems.Add(galleryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGalleryItem", new { id = galleryItem.Id }, galleryItem);
        }

        // DELETE: api/GalleryItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGalleryItem(int id)
        {
            var galleryItem = await _context.GalleryItems.FindAsync(id);
            if (galleryItem == null)
            {
                return NotFound();
            }

            _context.GalleryItems.Remove(galleryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GalleryItemExists(int id)
        {
            return _context.GalleryItems.Any(e => e.Id == id);
        }
    }
}
