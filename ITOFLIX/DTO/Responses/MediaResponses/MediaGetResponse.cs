using System;


namespace ITOFLIX.DTO.Responses.MediaResponses
{
	public class MediaGetResponse
	{
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool Passive { get; set; }

        public List<int>? MediaCategoryIds { get; set; }

        public List<int>? MediaActorIds { get; set; }

        public List<int>? MediaDirectorIds { get; set; }

        public List<int>? MediaRestrictionsIds { get; set; }
    }
}

