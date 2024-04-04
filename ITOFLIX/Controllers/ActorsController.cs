using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITOFLIX.Data;
using ITOFLIX.Models;
using Microsoft.AspNetCore.Authorization;

namespace ITOFLIX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ITOFLIXContext _context;

        public ActorsController(ITOFLIXContext context)
        {
            _context = context;
        }

        // GET: api/Actors
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
          if (_context.Actors == null)
          {
              return NotFound();
          }
            return await _context.Actors.ToListAsync();
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Actor>> GetActor(int id)
        {
          if (_context.Actors == null)
          {
              return NotFound();
          }
            var actor = await _context.Actors.FindAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public async Task<IActionResult> PutActor(int id, Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
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

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ContentAdmin")]
        public void PostActor(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges();

        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            if (_context.Actors == null)
            {
                return NotFound();
            }
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActorExists(int id)
        {
            return (_context.Actors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
