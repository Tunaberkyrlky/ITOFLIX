
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITOFLIX.Data;
using ITOFLIX.Models;
using Microsoft.AspNetCore.Authorization;
using ITOFLIX.Models.CompositeModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ITOFLIX.DTO.Converters;
using ITOFLIX.DTO.Responses.EpisodeResponses;
using ITOFLIX.DTO.Requests.EpisodeRequests;

namespace ITOFLIX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly ITOFLIXContext _context;
        private readonly UserManager<ITOFLIXUser> _userManager;

        EpisodeConverter _episodeConverter = new EpisodeConverter();

        public EpisodesController(ITOFLIXContext context, UserManager<ITOFLIXUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Episodes
        [HttpGet]
        [Authorize]
        public ActionResult<List<EpisodeGetResponse>> GetEpisodes(int mediaId, byte seasonNumber ,bool includePassive = false)
        {
            IQueryable<Episode> episodes = _context.Episodes;
            if(includePassive == false)
            {
                episodes = episodes.Where(e => e.Passive == false);
            }
                episodes = episodes.Where(e => e.MediaId == mediaId && e.SeasonNumber == seasonNumber)
                        .OrderBy(e => e.SeasonNumber);
            return _episodeConverter.Convert(episodes.AsNoTracking().ToList());
        }

        // GET: api/Episodes/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<EpisodeGetResponse> GetEpisode(long id)
        {
            Episode? episode = _context.Episodes.Find(id);

            if (_context.Episodes == null || episode == null)
            {
              return NotFound();
            }

            return _episodeConverter.Convert(episode);
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
        public ActionResult AddFavorite(int mediaId)
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
        public ActionResult<long> PostEpisode(EpisodeCreateRequest episodeCreateRequest)
        {
          if (_context.Episodes == null)
          {
              return Problem("Entity set 'ITOFLIXContext.Episodes'  is null.");
          }
            Episode newEpisode = _episodeConverter.Convert(episodeCreateRequest);
            _context.Episodes.Add(newEpisode);
            _context.SaveChanges();

            return Ok("Episode created and an Id assigned: " + newEpisode.Id);
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
