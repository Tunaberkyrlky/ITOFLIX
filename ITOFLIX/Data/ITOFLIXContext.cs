using System;
using Microsoft.EntityFrameworkCore;
using ITOFLIX.Models;

namespace ITOFLIX.Data
{
	public class ITOFLIXContext : DbContext
	{
		public ITOFLIXContext(DbContextOptions<ITOFLIXContext> options): base(options)
		{
		}
		public DbSet<ITOFLIX.Models.Category> Categories { get; set; } = default!;
	}
}

