using System;
using Microsoft.EntityFrameworkCore;
using ITOFLIX.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ITOFLIX.Data
{
	public class ITOFLIXContext : IdentityDbContext<ITOFLIXUser,ITOFLIXRole, long>
	{
		public ITOFLIXContext(DbContextOptions<ITOFLIXContext> options): base(options)
		{
		}
	

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ITOFLIX.Models.Category> Categories { get; set; } = default!;
    }
}

