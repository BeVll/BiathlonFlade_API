using MB_API.Data.Entities;
using MB_API.Data.Entities.Identity;
using MB_API.Data.Entities;
using MB_API.Data.Entities.Identity;
using MB_API.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FladeUp_Api.Data
{
    public class AppEFContext : IdentityDbContext<UserEntity, RoleEntity, int,
        IdentityUserClaim<int>, UserRoleEntity, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public AppEFContext(DbContextOptions<AppEFContext> options)
            : base(options)
        {
            
        }

        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<CheckPointEntity> CheckPoints { get; set; }
        public DbSet<CheckPointType> CheckPointTypes { get; set; }
        public DbSet<RaceCheckPointEntity> RaceCheckPoints { get; set; }
        public DbSet<RaceEntity> Races { get; set; }
        public DbSet<RaceTypeEnitity> RaceTypes { get; set; }
        public DbSet<ResultEntity> Results { get; set; }
        public DbSet<TrackEntity> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserRoleEntity>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });

                ur.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(r => r.RoleId)
                    .IsRequired();

                ur.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();
            });

        }
    }
}
