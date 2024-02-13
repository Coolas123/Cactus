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
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostMaterial> PostMaterials { get; set; }
        public DbSet<PostComment> PostComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<SystemRole>().ToTable("SystemRoles");
            builder.Entity<Patron>().ToTable("Patrons");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<Country>().ToTable("Countries");
            builder.Entity<Author>().ToTable("Authors");
            builder.Entity<ProfileMaterial>().ToTable("ProfileMaterials");
            builder.Entity<AuthorSubscribe>().ToTable("AuthorSubscribes");
            builder.Entity<MaterialType>().ToTable("MaterialTypes");
            builder.Entity<Post>().ToTable("Posts");
            builder.Entity<Tag>().ToTable("Tags");
            builder.Entity<PostTag>().ToTable("PostTags");
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<PostCategory>().ToTable("PostCategories");
            builder.Entity<PostMaterial>().ToTable("PostMaterials");
            builder.Entity<PostComment>().ToTable("PostComments");

            builder.Entity<User>(x =>
            {
                x.Property(p=>p.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<SystemRole>(x =>
            {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
                x.HasData(
                [
                    new {Id=1,Name="User" },
                    new {Id=2,Name="Admin" }
                ]);
            });
            builder.Entity<UserRole>(x =>
            {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
                x.HasData(
                [
                    new {Id=1,Name="Patron" },
                    new {Id=2,Name="Individual" }
                ]);
            });
            builder.Entity<Country>(x =>
            {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
                x.HasData(
                [
                    new {Id=1,Name="Russia" }
                ]);
            });
            builder.Entity<ProfileMaterial>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<MaterialType>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
                x.HasData(
                [
                    new {Id=1,Name="Avatar" },
                    new {Id=2,Name="Banner" },
                    new {Id=3,Name="PostPhoto" }
                ]);
            });
            builder.Entity<AuthorSubscribe>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<Post>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<Tag>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<PostTag>(x => {
                x.HasKey(p => new { p.PostId, p.TagId });
            });
            builder.Entity<Category>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
                x.HasData([
                    new { Id = 1, Name = "Games" },
                    new { Id = 2, Name = "Art" },
                    new { Id = 3, Name = "Music" }
                ]);
            });
            builder.Entity<PostCategory>(x => {
                x.HasKey(p => new { p.PostId, p.CategoryId });
            });
            builder.Entity<PostMaterial>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<PostComment>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
