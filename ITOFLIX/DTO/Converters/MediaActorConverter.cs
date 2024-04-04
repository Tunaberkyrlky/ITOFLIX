using System;
using ITOFLIX.DTO.Responses.MediaActorResponses;
using ITOFLIX.Models.CompositeModels;

namespace ITOFLIX.DTO.Converters
{
	public class MediaActorConverter
	{
		public MediaActorGetResponse Convert(MediaActor mediaActor)
		{
			MediaActorGetResponse mediaActorGetResponse = new()
			{
				ActorId = mediaActor.ActorId,
				MediaId = mediaActor.MediaId
			};
			return mediaActorGetResponse;
		}
		public List<MediaActorGetResponse> Convert(List<MediaActor> mediaActors)
		{
			List<MediaActorGetResponse> mediaActorGetResponses = new();
			if(mediaActors != null)
			{
                foreach (var mediaActor in mediaActors)
                {
                    mediaActorGetResponses.Add(Convert(mediaActor));
                }
            }
			return mediaActorGetResponses;
        }

		public List<int> ConvertToActorId(List<MediaActor> mediaActors)
		{
			List<MediaActorGetResponse> mediaActorList = Convert(mediaActors);
			List<int> mediaActorIds = new();
			foreach (var mediaActor in mediaActorList)
			{
				mediaActorIds.Add(mediaActor.ActorId);
			}
			return mediaActorIds;
        }
	}
}

