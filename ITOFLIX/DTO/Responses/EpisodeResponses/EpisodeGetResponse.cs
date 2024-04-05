using System;
using System.ComponentModel.DataAnnotations;

namespace ITOFLIX.DTO.Responses.EpisodeResponses
{
	public class EpisodeGetResponse
	{
        public long Id { get; set; }

        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string Title { get; set; } = "";

        [StringLength(500)]
        public string Description { get; set; } = "";

        [Required]
        public TimeSpan Duration { get; set; }

        [Range(0, byte.MaxValue)]
        [Required]
        public byte SeasonNumber { get; set; }

        [Range(0, 366)]
        [Required]
        public short EpisodeNumber { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public long ViewCount { get; set; }

        public bool Passive { get; set; }

        [Required]
        public int MediaId { get; set; }
    }
}

