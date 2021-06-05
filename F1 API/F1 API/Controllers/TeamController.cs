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

namespace F1_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly LibraryContext _context;

        public TeamController(LibraryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
        {
            var teams = await _context.Teams
                .Include(x => x.Drivers)
                .Include(x => x.Engine)
                .ToListAsync();
                                            
                                            
            return Ok(teams);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id) //WORKS
        {
            var team = await _context.Teams
                .Include(x => x.Drivers)
                .ThenInclude(x => x.TrackWins)
                .ThenInclude(x => x.Track)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> DeleteTeam(int id) //WORKS (when no drivers are in the team)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Team>> InsertTeam(Team team) 
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeam", new { id = team.TeamId }, team);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, Team team) 
        {
            if (id != team.TeamId)
            {
                return BadRequest();
            }
            _context.Entry(team).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(team);
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.TeamId == id);
        }
    }
}
