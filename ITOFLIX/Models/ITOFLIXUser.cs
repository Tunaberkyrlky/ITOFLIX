using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ITOFLIX.Models
{
	public class ITOFLIXUser : IdentityUser<long>
	{
        [StringLength(100, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = "";

		[Column(TypeName= "date")]
		public DateTime BirthDate { get; set; }

		public bool Passive { get; set; }

		//[NotMapped]
		//[StringLength(100, MinimumLength =8)]
		//public string Password { get; set; } = "";

	}
}

