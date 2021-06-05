using F1_API.database;
using F1_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f1api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase 
    {
        private readonly LibraryContext _context;
        public TrackController(LibraryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Track>>> GetAllTracks() 
        {
            return await _context.Tracks.Include(x => x.WinningDrivers)
                                            .ThenInclude(x => x.Driver)
                                            .ThenInclude(x => x.Team)
                                            .ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Track>> InsertTrack(Track Track)
        {
            _context.Tracks.Add(Track);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTrack", new { id = Track.TrackId }, Track);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Track>> GetTrack(int id) 
        {
            var Track = await _context.Tracks
                .Include(x => x.WinningDrivers)
                .ThenInclude(x => x.Driver)
                .ThenInclude(x => x.Team)
                .FirstOrDefaultAsync(x => x.TrackId == id);
            if (Track == null)
            {
                return NotFound();
            }
            return Track;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Track>> DeleteTrack(int id)
        {
            var Track = await _context.Tracks.FindAsync(id);
            if (Track == null)
            {
                return NotFound();
            }
            _context.Tracks.Remove(Track);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrack(int id, Track Track) 
        {
            if (id != Track.TrackId)
            {
                return BadRequest();
            }
            _context.Entry(Track).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(Track);
        }
        
    
        


        private bool TrackExists(int id)
        {
            return _context.Tracks.Any(x => x.TrackId == id);
        }
    }
}
