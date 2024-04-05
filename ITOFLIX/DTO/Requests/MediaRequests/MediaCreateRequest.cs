using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.DTO.Requests.MediaRequests
{
	public class MediaCreateRequest
	{
        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string Name { get; set; } = "";

        [StringLength(500)]
        [Required]
        public string? Description { get; set; }

        public List<short> CategoryIds { get; set; } = new List<short>();

        public List<int> ActorIds { get; set; } = new List<int>();

        public List<int> DirectorIds { get; set; } = new List<int>();

        public List<byte> RestrictionIds { get; set; } = new List<byte>();
    }
}

