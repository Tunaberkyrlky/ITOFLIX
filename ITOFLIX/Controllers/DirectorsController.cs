using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITOFLIX.Data;
using ITOFLIX.Models;

namespace ITOFLIX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly ITOFLIXContext _context;

        public DirectorsController(ITOFLIXContext context)
        {
            _context = context;
        }

        // GET: api/Directors
        [HttpGet]
        public ActionResult<List<Director>> GetDirectors()
        {
          if (_context.Directors == null)
          {
              return NotFound();
          }
            return _context.Directors.ToList();
        }

        // GET: api/Directors/5
        [HttpGet("{id}")]
        public ActionResult<Director> GetDirector(int id)
        {
          if (_context.Directors == null)
          {
              return NotFound();
          }
            var director =  _context.Directors.Find(id);

            if (director == null)
            {
                return NotFound();
            }

            return director;
        }

        // PUT: api/Directors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutDirector(int id, Director director)
        {
            if (id != director.Id)
            {
                return BadRequest();
            }

            Director? currentDirector = _context.Directors.Find(id);
            currentDirector.Name = director.Name;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
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

        // POST: api/Directors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Director> PostDirector(Director director)
        {
          if (_context.Directors == null)
          {
              return Problem("Entity set 'ITOFLIXContext.Directors'  is null.");
          }
             _context.Directors.Add(director);
             _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/Directors/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDirector(int id)
        {
            if (_context.Directors == null)
            {
                return NotFound();
            }
            var director =  _context.Directors.Find(id);
            if (director == null)
            {
                return NotFound();
            }

            director.Passive = true;
            _context.Directors.Update(director);
            _context.SaveChanges();
            return Ok();
        }

        private bool DirectorExists(int id)
        {
            return (_context.Directors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
