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
    public class PlansController : ControllerBase
    {
        private readonly ITOFLIXContext _context;

        public PlansController(ITOFLIXContext context)
        {
            _context = context;
        }

        // GET: api/Plans
        [HttpGet]
        [Authorize]
        public ActionResult<List<Plan>> GetPlans()
        {
          if (_context.Plans == null)
          {
              return NotFound();
          }
            return  _context.Plans.ToList();
        }

        // GET: api/Plans/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Plan> GetPlan(short id)
        {
          if (_context.Plans == null)
          {
              return NotFound();
          }
            var plan = _context.Plans.Find(id);

            if (plan == null)
            {
                return NotFound();
            }

            return plan;
        }

        // PUT: api/Plans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult PutPlan(short id, Plan plan)
        {
            if (id != plan.Id)
            {
                return BadRequest();
            }

            Plan? currentPlan = _context.Plans.Find(id);
            currentPlan.Name = plan.Name;
            currentPlan.Price = plan.Price;
            currentPlan.Resolution = plan.Resolution;

            _context.Plans.Update(currentPlan);
            try
            {
                 _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(id))
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

        // POST: api/Plans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public ActionResult<Plan> PostPlan(Plan plan)
        {
          if (_context.Plans == null)
          {
              return Problem("Entity set 'ITOFLIXContext.Plans'  is null.");
          }
            _context.Plans.Add(plan);
            _context.SaveChanges();

            return CreatedAtAction("GetPlan", new { id = plan.Id }, plan);
        }

        // DELETE: api/Plans/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePlan(short id)
        {
            if (_context.Plans == null)
            {
                return NotFound();
            }
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlanExists(short id)
        {
            return (_context.Plans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
