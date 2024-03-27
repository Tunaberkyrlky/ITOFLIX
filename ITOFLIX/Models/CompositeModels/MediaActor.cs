using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.Models.CompositeModels
{
	public class MediaActor
	{
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }

        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor? Actor { get; set; }
    }
}

