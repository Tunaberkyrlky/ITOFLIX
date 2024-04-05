using System;
using ITOFLIX.DTO.Requests.EpisodeRequests;
using ITOFLIX.DTO.Responses.EpisodeResponses;
using ITOFLIX.Models;

namespace ITOFLIX.DTO.Converters
{
	public class EpisodeConverter
	{
		public Episode Convert(EpisodeCreateRequest request)
		{
			Episode newEpisode = new()
			{
				Title = request.Title,
				Description = request.Description,
				Duration = request.Duration,
				SeasonNumber = request.SeasonNumber,
				EpisodeNumber = request.EpisodeNumber,
				ReleaseDate = request.ReleaseDate,
				MediaId = request.MediaId,

				Passive = false
			};
			return newEpisode;
		}

		public EpisodeGetResponse Convert(Episode episode)
		{
			EpisodeGetResponse newEpisodeResponse = new()
			{
				Id = episode.Id,
				Title = episode.Title,
				Description = episode.Description,
				Duration = episode.Duration,
				SeasonNumber = episode.SeasonNumber,
				EpisodeNumber = episode.EpisodeNumber,
				ReleaseDate = episode.ReleaseDate,
				ViewCount = episode.ViewCount,
				Passive = episode.Passive,
				MediaId = episode.MediaId
			};
			return newEpisodeResponse;
		}

		public List<EpisodeGetResponse> Convert(List<Episode> episodes)
		{
			List<EpisodeGetResponse> episodeResponses = new();
			foreach (var episode in episodes)
			{
				episodeResponses.Add(Convert(episode));
			}
			return episodeResponses;
		}
	}
}

