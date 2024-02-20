using Microsoft.EntityFrameworkCore;

namespace Cactus.Models.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) {
            Database.EnsureCreated();
        }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<SystemRole> SystemRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ProfileMaterial> ProfileMaterials { get; set; }
        public DbSet<AuthorSubscribe> AuthorSubscribes { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostMaterial> PostMaterials { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<UninterestingAuthor> UninterestingAuthors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);


            builder.Entity<SystemRole>(x =>
            {
                x.HasData(
                [
                    new {Id=1,Name="User" },
                    new {Id=2,Name="Admin" }
                ]);
            });
            builder.Entity<UserRole>(x =>
            {
                x.HasData(
                [
                    new {Id=1,Name="Patron" },
                    new {Id=2,Name="Individual" }
                ]);
            });
            builder.Entity<Country>(x =>
            {
                x.HasData(
                [
                    new {Id=1,Name="Russia" }
                ]);
            });
            builder.Entity<MaterialType>(x => {
                x.HasData(
                [
                    new {Id=1,Name="Avatar" },
                    new {Id=2,Name="Banner" },
                    new {Id=3,Name="PostPhoto" }
                ]);
            });
            builder.Entity<PostTag>(x => {
                x.HasKey(p => new { p.PostId, p.TagId });
            });
            builder.Entity<Category>(x => {
                x.HasData([
                    new { Id = 1, Name = "Games" },
                    new { Id = 2, Name = "Art" },
                    new { Id = 3, Name = "Music" }
                ]);
            });
            builder.Entity<PostCategory>(x => {
                x.HasKey(p => new { p.PostId, p.CategoryId });
            });
        }
    }
}
