using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.Models.CompositeModels
{
	public class UserWatched
	{

        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public ITOFLIXUser? ITOFLIXUser { get; set; }

        public long EpisodeId { get; set; }
        [ForeignKey("EpisodeId")]
        public Episode? Episode { get; set; }
    }
}

