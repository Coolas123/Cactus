using Cactus.Infrastructure.Interfaces;
using Cactus.Infrastructure.Repositories;
using Cactus.Models;
using Cactus.Models.Database;
using Cactus.Services.Implementations;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Cactus
{
    public class Startup
    {
        public Startup(IConfiguration conf) {
            configuration=conf;
        }

        private IConfiguration configuration { get; set; }


        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(opts => {
                opts.UseNpgsql(configuration["ConnectionStrings:CactusConnection"]);
            });
            services.AddDistributedMemoryCache();
            services.AddRazorPages();
            services.AddMemoryCache();
            services.AddSession();
            services.AddAuthorization();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    opts.SlidingExpiration = true;
                    opts.LoginPath = "/Account/Login";
                    opts.AccessDeniedPath = "/Account/Login";
                    opts.Cookie.HttpOnly = true;
                });
            services.AddHsts(opt => {
                opt.MaxAge = TimeSpan.FromDays(60);
                opt.IncludeSubDomains = true;
            });
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IProfileMaterialRepository, ProfileMaterialRepository>();
            services.AddScoped<IProfileMaterialService, ProfileMaterialService>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPatronRepository, PatronRepository>();
            services.AddScoped<IPatronService, PatronService>();
            services.AddScoped<IAuthorSubscribeRepository, AuthorSubscribeRepository>();
            services.AddScoped<IAuthorSubscribeService, AuthorSubscribeService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostMaterialRepository, PostMaterialRepository>();
            services.AddScoped<IPostMaterialService, PostMaterialService>();
            services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPostCommentRepository, PostCommentRepository>();
            services.AddScoped<IPostCommentService, PostCommentService>();
            services.AddScoped<IUninterestingAuthorRepository, UninterestingAuthorRepository>();
            services.AddScoped<IUninterestingAuthorService, UninterestingAuthorService>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IPostTagRepository, PostTagRepository>();
            services.AddScoped<IPostTagService, PostTagService>();
            services.AddScoped<INewsFeedService, NewsFeedService>();
            services.AddScoped<IComplainRepository, ComplainRepository>();
            services.AddScoped<IComplainService, ComplainService>();
            services.AddScoped<IDonationOptionRepository, DonationOptionRepository>();
            services.AddScoped<IDonationOptionService, DonationOptionService>();
            services.AddScoped<IDonatorService, DonatorService>();
            services.AddScoped<IDonatorRepository, DonatorRepository>();
            services.AddScoped<ISubLevelMaterialService, SubLevelMaterialService>();
            services.AddScoped<ISubLevelMaterialRepository, SubLevelMaterialRepository>();
            services.AddScoped<IPaidAuthorSubscribeService, PaidAuthorSubscribeService>();
            services.AddScoped<IPaidAuthorSubscribeRepository, PaidAuthorSubscribeRepository>();
            services.AddScoped<IPostDonationOptionService, PostDonationOptionService>();
            services.AddScoped<IPostDonationOptionRepository, PostDonationOptionRepository>();
            services.AddScoped<IPayMethodSettingService, PayMethodSettingService>();
            services.AddScoped<IPayMethodSettingRepository, PayMethodSettingRepository>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IPayMethodService, PayMethodService>();
            services.AddScoped<IPayMethodRepository, PayMethodRepository>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IPostOneTimePurschaseDonatorRepository, PostOneTimePurschaseDonatorRepository>();
            services.AddScoped<IPostOneTimePurschaseDonatorService, PostOneTimePurschaseDonatorService>();
        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapGet("/", context => {
                    return Task.Run(() => context.Response.Redirect("/NewsFeed"));
                });
                endpoints.MapFallback(async context => {
                    await context.Response.WriteAsync("Страница не найдена");
                });
            });
            new SeedData().fill(app);
        }
    }
}
