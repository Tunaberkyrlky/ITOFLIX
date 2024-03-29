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
    public class EpisodesController : ControllerBase
    {
        private readonly ITOFLIXContext _context;

        public EpisodesController(ITOFLIXContext context)
        {
            _context = context;
        }

        // GET: api/Episodes
        [HttpGet]
        [Authorize]
        public ActionResult<List<Episode>> GetEpisodes(int mediaId, byte seasonNumber ,bool includePassive = true)
        {
            IQueryable<Episode> episodes = _context.Episodes;
            if(includePassive == false)
            {
                episodes = episodes.Where(e => e.Passive == false);
            }
            return episodes
                .Where(e => e.MediaId == mediaId && e.SeasonNumber == seasonNumber)
                .OrderBy(e => e.SeasonNumber).AsNoTracking().ToList();
        }

        // GET: api/Episodes/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Episode> GetEpisode(long id)
        {
            Episode? episode = _context.Episodes.Find(id);

            if (_context.Episodes == null || episode == null)
            {
              return NotFound();
            }

            return episode;
        }

        [HttpGet("Watch")]
        [Authorize]
        public void Watch(long id)
        {
            UserWatched userWatched = new UserWatched();
            Episode episode = new Episode();

            try
            {
                userWatched.UserId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                userWatched.EpisodeId = id;
                _context.UserWatcheds.Add(userWatched);
                episode.ViewCount++;
                _context.Episodes.Update(episode);
                _context.SaveChanges(); 
            }
            catch (Exception ex)
            {

            }
        }

        // PUT: api/Episodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public IActionResult PutEpisode(long id, Episode episode)
        {
            Episode? currentEpisode = _context.Episodes.Find(id);
            if (currentEpisode == null)
            {
                return NotFound();
            }
            currentEpisode.Title = episode.Title;
            currentEpisode.Description = episode.Description;
            currentEpisode.Duration = episode.Duration;
            currentEpisode.SeasonNumber = episode.SeasonNumber;
            currentEpisode.EpisodeNumber = episode.EpisodeNumber;
            currentEpisode.ReleaseDate = episode.ReleaseDate;
            currentEpisode.Passive = episode.Passive;

            _context.Update(currentEpisode);

            return Ok();
        }

        // POST: api/Episodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<long> PostEpisode(Episode episode)
        {
          if (_context.Episodes == null)
          {
              return Problem("Entity set 'ITOFLIXContext.Episodes'  is null.");
          }
            _context.Episodes.Add(episode);
            _context.SaveChanges();

            return Ok("Episode created and an Id assigned: " + episode.Id);
        }

        // DELETE: api/Episodes/5
        [HttpDelete("{id}")]
        public ActionResult<string> DeleteEpisode(long id)
        {
            if (_context.Episodes == null)
            {
                return NotFound();
            }
            Episode? episode =  _context.Episodes.Find(id);
            if (episode == null)
            {
                return NotFound();
            }
            episode.Passive = true;

            return Ok("Deleted");
        }

        private bool EpisodeExists(long id)
        {
            return (_context.Episodes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
