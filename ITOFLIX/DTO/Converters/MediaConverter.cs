using System;
using ITOFLIX.Data;
using ITOFLIX.DTO.Requests.MediaRequests;
using ITOFLIX.Models;
using ITOFLIX.Models.CompositeModels;

namespace ITOFLIX.DTO.Converters
{
	public class MediaConverter
	{
		public Media Convert(MediaCreateRequest mediaCreateRequest, int mediaId, ITOFLIXContext context)
		{
            List<MediaActor> mediaActors = new();
            foreach (var Id in mediaCreateRequest.ActorIds)
            {
                MediaActor newMediaActor = new();
                newMediaActor.ActorId = Id;
                //newMediaActor.MediaId = mediaId;
                mediaActors.Add(newMediaActor);
                //context.MediaActors.Add(newMediaActor);
            }

			List<MediaCategory> mediaCategories = new();
			foreach (var Id in mediaCreateRequest.CategoryIds)
			{
                MediaCategory newMediaCategory = new();
                newMediaCategory.CategoryId = Id;
                //newMediaCategory.MediaId = mediaId;
                mediaCategories.Add(newMediaCategory);
                //context.MediaCategories.Add(newMediaCategory);
			}

            List<MediaDirector> mediaDirectors = new();
            foreach (var Id in mediaCreateRequest.DirectorIds)
            {
                MediaDirector newMediaDirector = new();                
                newMediaDirector.DirectorId = Id;
                //newMediaDirector.MediaId = mediaId;
                mediaDirectors.Add(newMediaDirector);
                //context.MediaDirectors.Add(newMediaDirector);
            }

            List<MediaRestriction> mediaRestrictions = new();
            foreach (var Id in mediaCreateRequest.RestrictionIds)
            {
                MediaRestriction newMediaRestriction = new();
                newMediaRestriction.RestrictionId = Id;
                //newMediaRestriction.MediaId = mediaId;
                mediaRestrictions.Add(newMediaRestriction);
                //context.MediaRestrictions.Add(newMediaRestriction);
            }

            context.SaveChanges();
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
	}
}

