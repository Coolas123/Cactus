using Cactus.Infrastructure.Interfaces;
using Cactus.Infrastructure.Repositories;
using Cactus.Models;
using Cactus.Models.Database;
using Cactus.Services.Implementations;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
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
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IProfileMaterialRepository, ProfileMaterialRepository>();
            services.AddScoped<IProfileMaterialService, ProfileMaterialService>();
            services.AddScoped<IIndividualRepository, IndividualRepository>();
            services.AddScoped<IIndividualService, IndividualService>();
            services.AddScoped<IPatronRepository, PatronRepository>();
            services.AddScoped<IPatronService, PatronService>();
            services.AddScoped<IAuthorSubscribeRepository, AuthorSubscribeRepository>();
            services.AddScoped<IAuthorSubscribeService, AuthorSubscribeService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostMaterialRepository, PostMaterialRepository>();
            services.AddScoped<IPostMaterialService, PostMaterialService>();
            services.AddScoped<ILegalRepository, LegalRepository>();
            services.AddScoped<ILegalService, LegalService>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectSubscribeRepository, ProjectSubscribeRepository>();
            services.AddScoped<IProjectSubscribeService, ProjectSubscribeService>();
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
            });
            new SeedData().fill(app);
        }
    }
}
