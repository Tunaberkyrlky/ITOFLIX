using System;
using ITOFLIX.DTO.Responses.MediaDirectorGetResponses;
using ITOFLIX.DTO.Responses.MediaRestrictionResponses;
using ITOFLIX.Models.CompositeModels;

namespace ITOFLIX.DTO.Converters
{
	public class MediaRestrictionConverter
	{
		public MediaRestrictionGetResponse Convert(MediaRestriction mediaRestriction)
		{
			MediaRestrictionGetResponse mediaRestrictionGetResponse = new()
			{
				MediaId = mediaRestriction.MediaId,
				RestrictionId = mediaRestriction.RestrictionId
			};
			return mediaRestrictionGetResponse;
		}

		public List<MediaRestrictionGetResponse> Convert(List<MediaRestriction> mediaRestrictions)
		{
			List<MediaRestrictionGetResponse> mediaRestrictionGetResponses = new();
			if(mediaRestrictions != null)
			{
                foreach (var mediaRestriction in mediaRestrictions)
                {
                    mediaRestrictionGetResponses.Add(Convert(mediaRestriction));
                }
            }
			return mediaRestrictionGetResponses;
        }

        public List<int> ConvertToRestrictionId(List<MediaRestriction> mediaRestrictions)
        {
            List<MediaRestrictionGetResponse> mediaRestrictionList = Convert(mediaRestrictions);
            List<int> restrictionIds = new();
            foreach (var mediaRestriction in mediaRestrictionList)
            {
                restrictionIds.Add(mediaRestriction.RestrictionId);
            }
            return restrictionIds;
        }
    }
}

