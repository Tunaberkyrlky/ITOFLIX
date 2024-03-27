using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.Models
{
	public class Plan
	{
		public short Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = "";

		[Range(0, float.MaxValue)]
		public float Price { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(20)")]
        public string Resolution { get; set; } = "";
	}
}

