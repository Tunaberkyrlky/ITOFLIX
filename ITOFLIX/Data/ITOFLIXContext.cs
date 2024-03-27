using System;
using Microsoft.EntityFrameworkCore;
using ITOFLIX.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ITOFLIX.Data
{
	public class ITOFLIXContext : IdentityDbContext
	{
		public ITOFLIXContext(DbContextOptions<ITOFLIXContext> options): base(options)
		{
		}
		public DbSet<ITOFLIX.Models.Category> Categories { get; set; } = default!;
	}
}

