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
            builder.Entity<ITOFLIX.Models.CompositeModels.MediaCategory>().HasKey(mc => new { mc.MediaId, mc.CategoryId });
            builder.Entity<ITOFLIX.Models.CompositeModels.MediaDirector>().HasKey(md => new { md.MediaId, md.DirectorId });
            builder.Entity<ITOFLIX.Models.CompositeModels.MediaRestriction>().HasKey(mr => new { mr.MediaId, mr.RestrictionId });
            builder.Entity<ITOFLIX.Models.CompositeModels.MediaActor>().HasKey(ma => new { ma.MediaId, ma.ActorId });
            builder.Entity<ITOFLIX.Models.CompositeModels.UserFavorite>().HasKey(uf => new { uf.MediaId, uf.UserId });
            builder.Entity<ITOFLIX.Models.CompositeModels.UserWatched>().HasKey(uw => new { uw.EpisodeId, uw.UserId });
        }
        public DbSet<ITOFLIX.Models.Actor> Actors { get; set; } = default!;
        public DbSet<ITOFLIX.Models.Category> Categories { get; set; } = default!;
        public DbSet<ITOFLIX.Models.Director> Directors { get; set; } = default!;
        public DbSet<ITOFLIX.Models.Episode> Episodes { get; set; } = default!;
        public DbSet<ITOFLIX.Models.Media> Media { get; set; } = default!;
        public DbSet<ITOFLIX.Models.Plan> Plans { get; set; } = default!;
        public DbSet<ITOFLIX.Models.Restriction> Restrictions { get; set; } = default!;
        public DbSet<ITOFLIX.Models.UserSubscription> UserSubscriptions { get; set; } = default!;

        public DbSet<ITOFLIX.Models.CompositeModels.MediaCategory> MediaCategories { get; set; } = default!;
        public DbSet<ITOFLIX.Models.CompositeModels.MediaDirector> MediaDirectors { get; set; } = default!;
        public DbSet<ITOFLIX.Models.CompositeModels.MediaRestriction> MediaRestrictions { get; set; } = default!;
        public DbSet<ITOFLIX.Models.CompositeModels.MediaActor> MediaActors { get; set; } = default!;
        public DbSet<ITOFLIX.Models.CompositeModels.UserFavorite> UserFavorites { get; set; } = default!;
        public DbSet<ITOFLIX.Models.CompositeModels.UserWatched> UserWatcheds { get; set; } = default!;

    }
}

