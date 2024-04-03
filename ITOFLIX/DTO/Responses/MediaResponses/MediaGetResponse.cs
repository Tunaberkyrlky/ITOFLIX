using System;
namespace ITOFLIX.DTO.Responses.MediaResponses
{
	public class MediaGetResponse
	{
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public List<ITOFLIX.Models.CompositeModels.MediaCategory>? MediaCategories { get; set; }

        public List<ITOFLIX.Models.CompositeModels.MediaActor>? MediaActors { get; set; }

        public List<ITOFLIX.Models.CompositeModels.MediaDirector>? MediaDirectors { get; set; }

        public List<ITOFLIX.Models.CompositeModels.MediaRestriction>? MediaRestrictions { get; set; }
    }
}

