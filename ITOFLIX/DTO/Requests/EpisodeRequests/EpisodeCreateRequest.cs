using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.DTO.Requests.EpisodeRequests
{
	public class EpisodeCreateRequest
	{
        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string Title { get; set; } = "";

        [StringLength(500)]
        public string Description { get; set; } = "";

        [Column(TypeName = "time(0)")]
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

        [Required]
        public int MediaId { get; set; }
    }
}

