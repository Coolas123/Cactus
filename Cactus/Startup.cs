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
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IMaterialService, MaterialService>();
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
