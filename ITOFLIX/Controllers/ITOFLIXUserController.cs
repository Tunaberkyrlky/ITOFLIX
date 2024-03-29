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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace ITOFLIX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ITOFLIXUserController : ControllerBase
    {
        public struct LoginVM
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public struct ValidateTokenVM
        {
            public long Id { get; set; }
            public string ResetPasswordToken { get; set; }
            public string NewPassword { get; set; }
        }
        private readonly SignInManager<ITOFLIXUser> _signInManager;
        private readonly ITOFLIXContext _context;

        public ITOFLIXUserController(SignInManager<ITOFLIXUser> signInManager, ITOFLIXContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        // GET: api/ITOFLIXUser
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult<List<ITOFLIXUser>> GetUsers(bool includePassive = true)
        {
            IQueryable<ITOFLIXUser> users = _signInManager.UserManager.Users;
            if (includePassive == false)
            {
                users = users.Where(u => u.Passive == false);
            }
            return users.AsNoTracking().ToList();
        }

        // GET: api/ITOFLIXUser/5
        [HttpGet("{id}")] 
        [Authorize]
        public ActionResult<ITOFLIXUser> GetITOFLIXUser(long id)
        {
            ITOFLIXUser? iTOFLIXUser;
            if (User.IsInRole("Administrator") == false)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())
                {
                    return Unauthorized();
                }
            }
            iTOFLIXUser = _signInManager.UserManager.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefault();

            if (iTOFLIXUser == null)
            {
              return NotFound();
            }
            return iTOFLIXUser;
        }

        // PUT: api/ITOFLIXUser/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize]
        public ActionResult PutITOFLIXUser(long id, ITOFLIXUser iTOFLIXUser)
        {
            ITOFLIXUser? user = null;
            if (User.IsInRole("CustomerRepresentive") == false)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())
                {
                    return Unauthorized();
                }
            }

            user = _signInManager.UserManager.Users.Where(u => u.Id == iTOFLIXUser.Id).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            user.Name = iTOFLIXUser.Name;
            user.UserName = iTOFLIXUser.UserName;
            user.PhoneNumber = iTOFLIXUser.PhoneNumber;
            user.Email = iTOFLIXUser.Email;

            _signInManager.UserManager.UpdateAsync(user).Wait();

            return Ok();
        }

        // POST: api/ITOFLIXUser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<string> PostITOFLIXUser(ITOFLIXUser iTOFLIXUser)
        {
            if (User.Identity!.IsAuthenticated == true)
            {
                return BadRequest();
            }
            iTOFLIXUser.UserName = iTOFLIXUser.Email;

            IdentityResult identityResult = _signInManager.UserManager.CreateAsync(iTOFLIXUser, iTOFLIXUser.Password).Result;

            if(identityResult != IdentityResult.Success)
            {
                return identityResult.Errors.FirstOrDefault()!.Description;
            }
            return Ok(iTOFLIXUser.Id);
        }

        // DELETE: api/ITOFLIXUser/5
        [HttpDelete("{id}")]
        public IActionResult DeleteITOFLIXUser(long id)
        {
            ITOFLIXUser? user = null;

            if (User.IsInRole("CustomerRepresentive") == false)
            {
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) != id.ToString())
                {
                    return Unauthorized();
                }
            }
            user = _signInManager.UserManager.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            if (user == null)
            {
                return NotFound();
            }

            user.Passive = true;
            _signInManager.UserManager.UpdateAsync(user).Wait();
            return Ok();
        }

        [HttpPost("Login")]
        public ActionResult<bool> Login(LoginVM loginVM)
        {
            // Check user subscription end date and add subscription plan claim
            Microsoft.AspNetCore.Identity.SignInResult signInResult;

            ITOFLIXUser? user = _signInManager.UserManager.FindByNameAsync(loginVM.Username).Result;

            if(user == null)
            {
                return false;
            }
            else if (user.Passive == true)
            {
                return false;
            }
            else if (_context.UserSubscriptions.Where(us=>us.UserId == user!.Id && us.EndDate == DateTime.Today).Any())
            {
                user.Passive = true;
                _signInManager.UserManager.UpdateAsync(user).Wait();
            }
            else if (user.Passive == true)
            {
                return Content("Passive");
            }

            signInResult = _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false).Result;

            if(signInResult != Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                return false;
            }
            return signInResult.Succeeded;
        }

        [HttpPost("Logout")]
        public void Logout()
        {
            _signInManager.SignOutAsync().Wait();
        }

        [HttpPost("ResetPassword")]
        public ActionResult<string> ResetPassword(long Id)
        {
            ITOFLIXUser? user = _signInManager.UserManager.FindByIdAsync(Id.ToString()).Result;
            if (user != null)
            {
                return _signInManager.UserManager.GeneratePasswordResetTokenAsync(user!).Result;
            }
            return NotFound();
        }

        [HttpPost("ValidateToken")]
        public ActionResult<string> ValidateToken(ValidateTokenVM validateTokenVM)
        {
            ITOFLIXUser? user = _signInManager.UserManager.FindByIdAsync(validateTokenVM.Id.ToString()).Result;
            if(user == null)
            {
                return BadRequest();   
            }
            IdentityResult? identityResult = _signInManager.UserManager.ResetPasswordAsync(user!, validateTokenVM.ResetPasswordToken, validateTokenVM.NewPassword).Result;
            return Ok(identityResult.Succeeded);
            
        }
    }
}
