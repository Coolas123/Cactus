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
        public DbSet<PaidAuthorSubscribe> PaidAuthorSubscribes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PayMethod> PayMethods { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<TransactionStatus> TransactionStatues { get; set; }
        public DbSet<PayMethodSetting> PayMethodSettings { get; set; }
        public DbSet<PostOneTimePurschaseDonator> PostOneTimePurschaseDonators { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);


            builder.Entity<SystemRole>(x => {
                x.HasData(
                [
                    new { Id = 1, Name = "User" },
                    new { Id = 2, Name = "Admin" },
                    new { Id = 3, Name = "Moderator" }
                ]);
            });
            builder.Entity<UserRole>(x => {
                x.HasData(
                [
                    new { Id = 1, Name = "Patron" },
                    new { Id = 2, Name = "Author" }
                ]);
            });
            builder.Entity<Country>(x => {
                x.HasData(
                [
                    new { Id = 1, Name = "Russia" }
                ]);
            });
            builder.Entity<MaterialType>(x => {
                x.HasData(
                [
                    new { Id = 1, Name = "Avatar" },
                    new { Id = 2, Name = "Banner" },
                    new { Id = 3, Name = "PostPhoto" },
                    new { Id = 4, Name = "SubLevelCover" }
                ]);
            });
            builder.Entity<PostTag>(x => {
                x.HasKey(p => new { p.PostId, p.TagId });
            });
            builder.Entity<Category>(x => {
                x.HasData([
                    new { Id = 1, Name = "IT" },
                    new { Id = 2, Name = "Gaming" },
                    new { Id = 3, Name = "Art" },
                    new { Id = 4, Name = "News" }
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
                    new { Id = 2, Name = "SubLevel" },
                    new { Id = 3, Name = "Goal" },
                    new { Id = 4, Name = "Remittance" }
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
                    new { Id = 2, Name = "Author" }
                ]);
            });
            builder.Entity<PostDonationOption>(x => {
                x.HasKey(p => new { p.PostId, p.DonationOptionId });
            });
            builder.Entity<PostOneTimePurschaseDonator>(x => {
                x.HasKey(p => new { p.PostId, p.DonatorId });
            });
            builder.Entity<Currency>(x => {
                x.HasData([
                    new { Id = 1, CountryId = (int)Models.Enums.Country.Russia, Symbol = "₽" }
                ]);
            });
            builder.Entity<PayMethodSetting>(x => {
                x.HasData([
                    new { Id = 1, Comission = (decimal)10, DailyWithdrawLimit = (decimal)0, MonthlyWithdrawLimit = (decimal)0, TransactionTypeId = (int)Models.Enums.TransactionType.IntrasystemOperations },
                    new { Id = 2, Comission = (decimal)1, DailyWithdrawLimit = (decimal)1000, MonthlyWithdrawLimit = (decimal)15000, TransactionTypeId = (int)Models.Enums.TransactionType.Replenish },
                    new { Id = 3, Comission = (decimal)2, DailyWithdrawLimit = (decimal)1000, MonthlyWithdrawLimit = (decimal)15000, TransactionTypeId = (int)Models.Enums.TransactionType.Withdraw },
                    new { Id = 4, Comission = (decimal)3, DailyWithdrawLimit = (decimal)1000, MonthlyWithdrawLimit = (decimal)15000, TransactionTypeId = (int)Models.Enums.TransactionType.Replenish },
                    new { Id = 5, Comission = (decimal)4, DailyWithdrawLimit = (decimal)1000, MonthlyWithdrawLimit = (decimal)15000, TransactionTypeId = (int)Models.Enums.TransactionType.Withdraw }
                ]);
            });
            builder.Entity<PayMethod>(x => {
                x.HasData([
                    new { Id = 1, Name = "Balance", PayMethodSettingId = 1 },
                    new { Id = 2, Name = "Visa", PayMethodSettingId = 2 },
                    new { Id = 3, Name = "MasterCard", PayMethodSettingId = 4 },
                    new { Id = 4, Name = "Mir", PayMethodSettingId = 2 },
                    new { Id = 5, Name = "MasterCard", PayMethodSettingId = 3 },
                    new { Id = 6, Name = "Mir", PayMethodSettingId = 5 }
                ]);
            });
            builder.Entity<TransactionType>(x => {
                x.HasData([
                    new { Id = 1, Name = "Replenish" },
                    new { Id = 2, Name = "Withdraw" },
                    new { Id = 3, Name = "IntrasystemOperations" }
                ]);
            });
            builder.Entity<TransactionStatus>(x => {
                x.HasData([
                    new { Id = 1, Name = "Sended" },
                    new { Id = 2, Name = "InProcess" },
                    new { Id = 3, Name = "Received" }
                ]);
            });
        }
    }
}
