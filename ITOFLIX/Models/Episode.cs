using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.Models
{
	public class Episode
	{
        public long Id { get; set; }

        [StringLength(200, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(200)")]
        public string Title { get; set; } = "";

        [StringLength(500)]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; } = "";

        public TimeSpan Duration { get; set; }

        [Range(0,byte.MaxValue)]
        public byte SeasonNumber { get; set; }

        [Range(0,366)]
		public short EpisodeNumber { get; set; }

		public DateTime ReleaseDate { get; set; }

        public long ViewCount { get; set; }

        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }
    }
}

