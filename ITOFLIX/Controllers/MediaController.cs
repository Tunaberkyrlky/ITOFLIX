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
using ITOFLIX.Models.CompositeModels;
using System.Security.Claims;

namespace ITOFLIX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly ITOFLIXContext _context;

        public MediaController(ITOFLIXContext context)
        {
            _context = context;
        }

        // GET: api/Media
        [HttpGet]
        public List<Media> GetMedia(bool includePassive = false)
        {
            IQueryable<Media> media = _context.Media;
            if(includePassive == true)
            {
                media = media.Where(m => m.Passive == true);
            }

            return media.ToList();
        }

        // GET: api/Media/5
        [HttpGet("{id}")]
        public ActionResult<Media> GetMedia(int id)
        {
            Media? media = _context.Media.Find(id);

            if (media == null)
            {
                return NotFound();
            }

            return media;
        }

        // PUT: api/Media/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutMedia(int id, Media media)
        {
            if (id != media.Id)
            {
                return BadRequest();
            }
            Media? currentMedia = _context.Media.Find(id);


            if (media != null)
            {
                currentMedia.Name = media.Name;
                currentMedia.Description = media.Description;
                currentMedia.Passive = media.Passive;
                //mediaactor, mediacatagory, mediadirector, restrictions are missing
                try
                {
                    _context.SaveChanges();
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return NotFound();
            }
            
        }

        // POST: api/Media
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult PostMedia(Media media)
        {
          if (_context.Media == null)
          {
              return Problem("Entity set 'ITOFLIXContext.Media'  is null.");
          }
            _context.Media.Add(media);
            _context.SaveChanges();

            return Ok("Media posted succesfully");
        }

        [HttpGet("Favorite")]
        [Authorize]
        public ActionResult AddFavorite(long mediaId)
        {
            UserFavorite userFavorite = new UserFavorite();
            Media? media = _context.Media.Find(mediaId);


            if (media == null)
            {
                return NoContent();
            }
            try
            {
                userFavorite.MediaId = media.Id;
                userFavorite.UserId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _context.Media.Update(media);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }

        // DELETE: api/Media/5
        [HttpDelete("{id}")]
        public ActionResult DeleteMedia(int id)
        {
            if (_context.Media == null)
            {
                return NotFound();
            }
             Media? media =  _context.Media.Find(id);
            if (media == null)
            {
                return NotFound();
            }

            media.Passive = true;
            _context.Media.Update(media);
            return Ok();
        }

        private bool MediaExists(int id)
        {
            return (_context.Media?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
