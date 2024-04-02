using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITOFLIX.Data;
using ITOFLIX.Models;
using Microsoft.AspNetCore.Identity;

namespace ITOFLIX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionsController : ControllerBase
    {
        private readonly ITOFLIXContext _context;
        private readonly UserManager<ITOFLIXUser> _userManager;

        public UserSubscriptionsController(ITOFLIXContext context, UserManager<ITOFLIXUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/UserSubscriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSubscription>>> GetUserSubscriptions()
        {
          if (_context.UserSubscriptions == null)
          {
              return NotFound();
          }
            return await _context.UserSubscriptions.ToListAsync();
        }

        // GET: api/UserSubscriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSubscription>> GetUserSubscription(long id)
        {
          if (_context.UserSubscriptions == null)
          {
              return NotFound();
          }
            var userSubscription = await _context.UserSubscriptions.FindAsync(id);

            if (userSubscription == null)
            {
                return NotFound();
            }

            return userSubscription;
        }

        // PUT: api/UserSubscriptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSubscription(long id, UserSubscription userSubscription)
        {
            if (id != userSubscription.Id)
            {
                return BadRequest();
            }
            //claim sorgula bu user abone olan user mı
            UserSubscription? currentUserSubscription = _context.UserSubscriptions.Find(id);

            currentUserSubscription.StartDate = userSubscription.StartDate;
            currentUserSubscription.EndDate = userSubscription.EndDate;
            currentUserSubscription.PlanId = userSubscription.PlanId;

            _context.UserSubscriptions.Update(currentUserSubscription);

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSubscriptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/UserSubscriptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public void PostUserSubscription(string email, short planId)
        {
            Plan plan = _context.Plans.Find(planId)!;
            //Get payment
            //If payment succesfull
            {
                UserSubscription userSubscription = new UserSubscription();
                ITOFLIXUser? currentUser = _userManager.FindByEmailAsync(email).Result;

                userSubscription.UserId = currentUser.Id;
                userSubscription.PlanId = planId;
                userSubscription.StartDate = DateTime.Today;
                userSubscription.EndDate = DateTime.Today.AddDays(30);

                _context.UserSubscriptions.Add(userSubscription);
                _context.SaveChanges();
            }
        }

        // DELETE: api/UserSubscriptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSubscription(long id)
        {
            if (_context.UserSubscriptions == null)
            {
                return NotFound();
            }
            var userSubscription = await _context.UserSubscriptions.FindAsync(id);
            if (userSubscription == null)
            {
                return NotFound();
            }

            _context.UserSubscriptions.Remove(userSubscription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserSubscriptionExists(long id)
        {
            return (_context.UserSubscriptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
