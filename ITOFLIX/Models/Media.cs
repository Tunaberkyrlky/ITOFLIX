using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.Models
{
	public class Media
	{
		public int Id { get; set; }

		[StringLength(200, MinimumLength =2)]
		[Column(TypeName= "nvarchar(200)")]
		public string Name { get; set; } = "";

		[StringLength(500)]
		[Column(TypeName = "nvarchar(500)")]
		public string? Description { get; set; }

		public List<MediaCatagory>? MediaCatagories { get; set; }

        public List<MediaActor>? MediaActors { get; set; }

        public List<MediaDirector>? MediaDirectors { get; set; }

		public List<Restrictions>? Restrictions { get; set; }
	}
}

