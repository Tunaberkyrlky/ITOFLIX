using System;
using System.Collections.Generic;
using ITOFLIX.DTO.Responses.MediaCategoryResponses;
using ITOFLIX.Models.CompositeModels;

namespace ITOFLIX.DTO.Converters
{
	public class MediaCategoryConverter
	{
		public MediaCategoryGetResponse Convert(MediaCategory mediaCategory)
		{
			MediaCategoryGetResponse newMediaCategoryGetResponse = new()
			{
				MediaId = mediaCategory.MediaId,
				CategoryId = mediaCategory.CategoryId
			};

			return newMediaCategoryGetResponse;
		}


		public List<MediaCategoryGetResponse> Convert(List<MediaCategory> mediaCategories)
		{
			List<MediaCategoryGetResponse> mediaCategoryGetResponses = new();
			if(mediaCategories != null)
			{
                foreach (var mediaCategory in mediaCategories)
                {
                    mediaCategoryGetResponses.Add(Convert(mediaCategory));
                }
            }

            return mediaCategoryGetResponses;
		}

		public List<int> ConvertToCategoryId(List<MediaCategory> mediaCategories)
		{
			List<MediaCategoryGetResponse> mediaCategoryList = Convert(mediaCategories);
			List<int> categoryIds = new();
			foreach (var mediaCategory in mediaCategoryList)
			{
				categoryIds.Add(mediaCategory.CategoryId);
			}
			return categoryIds;
		}
	}
}

