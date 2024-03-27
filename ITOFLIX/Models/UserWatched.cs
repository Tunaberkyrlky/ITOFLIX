using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.Models
{
	public class UserWatched
	{

        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public ITOFLIXUser? ITOFLIXUser { get; set; }

        public int EpisodeId { get; set; }
        [ForeignKey("EpisodeId")]
        public Episode? Episode { get; set; }
    }
}

