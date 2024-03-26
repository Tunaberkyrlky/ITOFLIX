using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITOFLIX.Models
{
	public class Category
	{
		public short Id { get; set; }

		[StringLength(100, MinimumLength = 2)]
		[Column(TypeName="nvarchar(100)")]
		public string Name { get; set; } = "";

	}
}

