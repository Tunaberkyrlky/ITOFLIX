using System;
using ITOFLIX.DTO.Responses.MediaCategoryResponses;
using ITOFLIX.DTO.Responses.MediaDirectorGetResponses;
using ITOFLIX.Models.CompositeModels;

namespace ITOFLIX.DTO.Converters
{
	public class MediaDirectorConverter
	{
		public MediaDirectorGetResponse Convert(MediaDirector mediaDirector)
		{
			MediaDirectorGetResponse mediaDirectorGetResponse = new()
			{
				DirectorId = mediaDirector.DirectorId,
				MediaId = mediaDirector.MediaId
			};
			return mediaDirectorGetResponse;
		}

		public List<MediaDirectorGetResponse> Convert(List<MediaDirector> mediaDirectors)
		{
			List<MediaDirectorGetResponse> mediaDirectorGetResponses = new();
			if(mediaDirectors != null)
			{
                foreach (var mediaDirector in mediaDirectors)
                {
                    mediaDirectorGetResponses.Add(Convert(mediaDirector));
                }
            }
			return mediaDirectorGetResponses;
        }

		public List<int> ConvertToDirectorId(List<MediaDirector> mediaDirectors)
		{
            List<MediaDirectorGetResponse> mediaDirectorList = Convert(mediaDirectors);
            List<int> directorIds = new();
            foreach (var mediaDirector in mediaDirectorList)
            {
                directorIds.Add(mediaDirector.DirectorId);
            }
            return directorIds;
        }
	}
}

