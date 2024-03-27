using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.Models.CompositeModels
{
	public class UserFavorite
	{
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public ITOFLIXUser? ITOFLIXUser { get; set; }

        public int MediaId { get; set; }
        [ForeignKey("MediaId")]
        public Media? Media { get; set; }
    }
}

