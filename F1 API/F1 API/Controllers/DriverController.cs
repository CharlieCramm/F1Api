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
    public class DriverController : ControllerBase
    {
        private readonly LibraryContext _context;

        public DriverController(LibraryContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetAllDrivers (string searchString, string sort, string Direction,int? page,int length)
        {
            IQueryable<Driver> query = _context.Drivers;

          
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(d => d.FirstName.Contains(searchString)
                || d.LastName.Contains(searchString)
                || d.Nationality.Contains(searchString)
                || d.Drivernumber.ToString().Contains(searchString)
                || d.BirthDate.ToString().Contains(searchString));
            }

            
            if (!string.IsNullOrWhiteSpace(sort) && !string.IsNullOrWhiteSpace(Direction))
            {
                switch (sort)
                {
                    case "firstName":
                        if (Direction == "dalen")
                        {
                            query = query.OrderByDescending(x => x.FirstName);
                        }
                        else if (Direction == "stijgen")
                        {
                            query = query.OrderBy(x => x.FirstName);
                        }
                        break;
                    case "lastName":
                        if (Direction == "dalen")
                        {
                            query = query.OrderByDescending(x => x.LastName);
                        }
                        else if (Direction == "stijgen")
                        {
                            query = query.OrderBy(x => x.LastName);
                        }
                        break;
                    case "nationality":
                        if (Direction == "dalen")
                        {
                            query = query.OrderByDescending(x => x.Nationality);
                        }
                        else if (Direction == "stijgen")
                        {
                            query = query.OrderBy(x => x.Nationality);
                        }
                        break;
                    case "birthDate":
                        if (Direction == "dalen")
                        {
                            query = query.OrderByDescending(x => x.BirthDate);
                        }
                        else if (Direction == "stijgen")
                        {
                            query = query.OrderBy(x => x.BirthDate);
                        }
                        break;
                    case "number":
                        if (Direction == "dalen")
                        {
                            query = query.OrderByDescending(x => x.Drivernumber);
                        }
                        else if (Direction == "stijgen")
                        {
                            query = query.OrderBy(x => x.Drivernumber);
                        }
                        break;
                }
            }

           
            if (page.HasValue)
            {
                query = query.Skip(page.Value * length);
                query = query.Take(length);
            }
           

           
            return await query.Include(d => d.Team)
                              .Include(d => d.TrackWins)
                              .ThenInclude(gp => gp.Track)
                              .ToListAsync();
        }

        // dit is mijn gewone GetAll

        /*[HttpGet]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _context.Drivers.Include(x => x.Team).Select(x => new Driver
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Team = new Team
                {
                    TeamId = x.Team.TeamId,
                    TeamNationality = x.Team.TeamNationality,
                    TeamName = x.Team.TeamName
                },
                Nationality = x.Nationality,
                BirthDate = x.BirthDate,
                Drivernumber = x.Drivernumber

            }).ToListAsync();
            return Ok(drivers);
        }*/
        [HttpPost]
        public async Task<ActionResult<Driver>> PostDriver(Driver driver) //WORKS
        {
            driver.Team = _context.Teams.SingleOrDefault(x => x.TeamId == driver.Team.TeamId);
            if (driver.TrackWins != null)
            {

            
            foreach (var item in driver.TrackWins)
            {
                item.Driver = item.Driver;
                
                item.Track = _context.Tracks.SingleOrDefault(x => x.TrackId == item.Track.TrackId);
                
            }
            }
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

           
            return CreatedAtAction(nameof(GetDriver), new { id = driver.Id }, driver);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Driver>> GetDriver(int id)
        {
            var driver = await _context.Drivers
                .Include(x => x.Team)
                .Include(x => x.TrackWins)
                .ThenInclude(x => x.Track)
                .FirstOrDefaultAsync(x => x.Drivernumber == id);
            if (driver == null)
            {
                return NotFound();
            }

            return driver;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Driver>> DeleteDriver(int id) //WORKS
        {
            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDriver(int id, [FromBody]Driver driver) 
        {
            if (id != driver.Id)
            {
                return BadRequest();
            }

            _context.Teams.Update(driver.Team);
            await _context.SaveChangesAsync();
            _context.Entry(driver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(driver);
        }
        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(x => x.Id == id);
        }
    }
}
