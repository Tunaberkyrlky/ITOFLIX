
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITOFLIX.Data;
using ITOFLIX.Models;
using Microsoft.AspNetCore.Authorization;
using ITOFLIX.Models.CompositeModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;



namespace ITOFLIX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly ITOFLIXContext _context;
        private readonly UserManager<ITOFLIXUser> _userManager;

        public EpisodesController(ITOFLIXContext context, UserManager<ITOFLIXUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public ActionResult Watch(long episodeId)
        {
            UserWatched userWatched = new UserWatched();
            Episode? episode = _context.Episodes.Find(episodeId);
            if (episode == null)
            {
                return NoContent();
            }
            
            try
            {
                userWatched.UserId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                userWatched.EpisodeId = episodeId;
                _context.UserWatcheds.Add(userWatched);
                episode!.ViewCount++;
                _context.Episodes.Update(episode);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return NoContent();
            }

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
        [Authorize(Roles = "ContentAdmin")]
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
        [Authorize(Roles = "ContentAdmin")]
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
