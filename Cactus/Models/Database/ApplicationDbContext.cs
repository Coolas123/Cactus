using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DbSet<Legal> Legals { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ProfileMaterial> ProfileMaterials { get; set; }
        public DbSet<AuthorSubscribe> AuthorSubscribes { get; set; }
        public DbSet<ProjectSubscribe> ProjectSubscribes { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostMaterial> PostMaterials { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<SystemRole>().ToTable("SystemRoles");
            builder.Entity<Legal>().ToTable("Legals");
            builder.Entity<Patron>().ToTable("Patrons");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<Country>().ToTable("Countries");
            builder.Entity<Individual>().ToTable("Individuals");
            builder.Entity<ProfileMaterial>().ToTable("ProfileMaterials");
            builder.Entity<AuthorSubscribe>().ToTable("AuthorSubscribes");
            builder.Entity<ProjectSubscribe>().ToTable("ProjectSubscribes");
            builder.Entity<Project>().ToTable("Projects");
            builder.Entity<MaterialType>().ToTable("MaterialTypes");
            builder.Entity<Post>().ToTable("Posts");
            builder.Entity<Tag>().ToTable("Tags");
            builder.Entity<PostTag>().ToTable("PostTags");
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<PostCategory>().ToTable("PostCategories");
            builder.Entity<PostMaterial>().ToTable("PostMaterials");

            builder.Entity<User>(x =>
            {
                x.Property(p=>p.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<SystemRole>(x =>
            {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
                x.HasData(new SystemRole[]
                {
                    new SystemRole {Id=1,Name="User" },
                    new SystemRole {Id=2,Name="Admin" }
                });
            });
            builder.Entity<UserRole>(x =>
            {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
                x.HasData(new UserRole[]
                {
                    new UserRole {Id=1,Name="Legal" },
                    new UserRole {Id=2,Name="Patron" },
                    new UserRole {Id=3,Name="Individual" }
                });
            });
            builder.Entity<Country>(x =>
            {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
                x.HasData(new Country[]
                {
                    new Country {Id=1,Name="Russia" }
                });
            });
            builder.Entity<ProfileMaterial>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<ProjectSubscribe>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<Project>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<MaterialType>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
                x.HasData(new MaterialType[]
                {
                    new MaterialType {Id=1,Name="Avatar" },
                    new MaterialType {Id=2,Name="Banner" },
                    new MaterialType {Id=3,Name="PostPhoto" }
                });
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
            });
            builder.Entity<PostCategory>(x => {
                x.HasKey(p => new { p.PostId, p.CategoryId });
            });
            builder.Entity<PostMaterial>(x => {
                x.Property(p => p.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
