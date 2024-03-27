using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.Models.CompositeModels
{
	public class MediaRestriction
	{
        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }

        public byte RestrictionId { get; set; }
        [ForeignKey("RestrictionId")]
        public Restriction? Restriction { get; set; }
    }
}

