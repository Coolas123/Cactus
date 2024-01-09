using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Cactus.Models.Database
{
    public class ApplicationDbContext : IdentityDbContext<User, SystemRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        public new DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Legal> Legals { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserLogin<Guid>>()
                .ToTable("AspNetUserLogins", t => t.ExcludeFromMigrations());
            builder.Entity<IdentityUserToken<Guid>>()
                .ToTable("AspNetUserTokens", t => t.ExcludeFromMigrations());
            builder.Entity<User>().Ignore(c => c.AccessFailedCount)
                                               .Ignore(c => c.EmailConfirmed)
                                               .Ignore(c => c.SecurityStamp)
                                               .Ignore(c => c.ConcurrencyStamp)
                                               .Ignore(c => c.PhoneNumber)
                                               .Ignore(c => c.PhoneNumberConfirmed)
                                               .Ignore(c => c.TwoFactorEnabled)
                                               .Ignore(c => c.LockoutEnd)
                                               .Ignore(c => c.LockoutEnabled)
                                               .Ignore(c => c.Email)
                                               .Ignore(c => c.NormalizedEmail)
                                               .ToTable("Users");
            builder.Entity<SystemRole>().Ignore(c => c.NormalizedName)
                                               .Ignore(c => c.ConcurrencyStamp);
            builder.Entity<SystemRole>().ToTable("SystemRoles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserHasRoles");
            builder.Entity<Legal>().ToTable("Legals");
            builder.Entity<Patron>().ToTable("Patrons");
            builder.Entity<UserRole>().ToTable("UserRoles");
        }
    }
}
