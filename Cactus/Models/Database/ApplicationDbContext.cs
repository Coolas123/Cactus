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
        public DbSet<Complain> Complains { get; set; }
        public DbSet<ComplainStatus> ComplainStatues { get; set; }
        public DbSet<ComplainType> ComplainTypes { get; set; }
        public DbSet<MonetizationType> MonetizationTypes { get; set; }
        public DbSet<ComplainTargetType> ComplainTargetTypes { get; set; }
        public DbSet<DonationTargetType> DonationTargetTypes { get; set; }
        public DbSet<DonationOption> DonationOptions { get; set; }
        public DbSet<Donator> Donators { get; set; }
        public DbSet<PostDonationOption> PostDonationOptions { get; set; }
        public DbSet<SubLevelMaterial> SubLevelMaterials { get; set; }

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
                    new {Id=3,Name="PostPhoto" },
                    new {Id=4,Name= "SubLevelCover" }
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
            builder.Entity<ComplainStatus>(x => {
                x.HasData([
                    new { Id = 1, Name = "NotReviewed" },
                    new { Id = 2, Name = "Reviewed" }
                ]);
            });
            builder.Entity<ComplainType>(x => {
                x.HasData([
                    new { Id = 1, Name = "Spam" },
                    new { Id = 2, Name = "Deception" }
                ]);
            });
            builder.Entity<MonetizationType>(x => {
                x.HasData([
                    new { Id = 1, Name = "OneTimePurchase" },
                    new { Id = 2, Name = "SubLevel" }
                ]);
            });
            builder.Entity<ComplainTargetType>(x => {
                x.HasData([
                    new { Id = 1, Name = "Post" },
                    new { Id = 2, Name = "User" },
                    new { Id = 3, Name = "Comment" }
                ]);
            });
            builder.Entity<DonationTargetType>(x => {
                x.HasData([
                    new { Id = 1, Name = "Post" },
                    new { Id = 2, Name = "User" }
                ]);
            });
            builder.Entity<PostDonationOption>(x => {
                x.HasKey(p => new { p.PostId, p.DonationOptionId });
            });
        }
    }
}
