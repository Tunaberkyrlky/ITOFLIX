using System;
using ITOFLIX.Data;
using ITOFLIX.DTO.Requests.MediaRequests;
using ITOFLIX.DTO.Responses.MediaResponses;
using ITOFLIX.Models;
using ITOFLIX.Models.CompositeModels;

namespace ITOFLIX.DTO.Converters
{
	public class MediaConverter
	{
        MediaCategoryConverter _mediaCategoryConverter = new();
        MediaActorConverter _mediaActorConverter = new();
        MediaDirectorConverter _mediaDirectorConverter = new();
        MediaRestrictionConverter _mediaRestrictionConverter = new();

		public Media Convert(MediaCreateRequest mediaCreateRequest)
		{
            List<MediaActor> mediaActors = new();
            if (mediaCreateRequest.ActorIds != null)
            {
                foreach (var Id in mediaCreateRequest.ActorIds)
                {
                    MediaActor newMediaActor = new();
                    newMediaActor.ActorId = Id;
                    mediaActors.Add(newMediaActor);
                }
            }

			List<MediaCategory> mediaCategories = new();
            if(mediaCreateRequest.CategoryIds != null)
            {
                foreach (var Id in mediaCreateRequest.CategoryIds)
                {
                    MediaCategory newMediaCategory = new();
                    newMediaCategory.CategoryId = Id;
                    mediaCategories.Add(newMediaCategory);
                }
            }

            List<MediaDirector> mediaDirectors = new();
            if(mediaCreateRequest.DirectorIds != null)
            {
                foreach (var Id in mediaCreateRequest.DirectorIds)
                {
                    MediaDirector newMediaDirector = new();
                    newMediaDirector.DirectorId = Id;
                    mediaDirectors.Add(newMediaDirector);
                }
            }

            List<MediaRestriction> mediaRestrictions = new();
            if(mediaCreateRequest.RestrictionIds != null)
            {
                foreach (var Id in mediaCreateRequest.RestrictionIds)
                {
                    MediaRestriction newMediaRestriction = new();
                    newMediaRestriction.RestrictionId = Id;
                    mediaRestrictions.Add(newMediaRestriction);
                }
            }

            Media newMedia = new()
			{
				Name = mediaCreateRequest.Name,
				Description = mediaCreateRequest.Description,
				MediaActors = mediaActors,
                MediaCategories = mediaCategories,
                MediaDirectors = mediaDirectors,
                MediaRestrictions = mediaRestrictions
			};
            return newMedia;
		}

        public MediaGetResponse Convert(Media media)
        {
            MediaGetResponse newMediaResponse = new()
            {
                Id = media.Id,
                Name = media.Name,
                Description = media.Description,
                Passive = media.Passive,
                MediaActorIds = _mediaActorConverter.ConvertToActorId(media.MediaActors),
                MediaCategoryIds = _mediaCategoryConverter.ConvertToCategoryId(media.MediaCategories),
                MediaDirectorIds = _mediaDirectorConverter.ConvertToDirectorId(media.MediaDirectors),
                MediaRestrictionsIds = _mediaRestrictionConverter.ConvertToRestrictionId(media.MediaRestrictions)
            };
            return newMediaResponse;
        }

        public List<MediaGetResponse> Convert(List<Media> medias)
        {
            List<MediaGetResponse> newMediaResponses = new();

            foreach (var media in medias)
            {
                newMediaResponses.Add(Convert(media));
            }
            return newMediaResponses;
        }
	}
}

