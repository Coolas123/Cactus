using Cactus.Models;
using Cactus.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

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
            services.AddSession();
            services.AddIdentity<User, SystemRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.ConfigureApplicationCookie(opts => {
                opts.Cookie.HttpOnly = true;
                opts.ExpireTimeSpan = TimeSpan.FromMinutes(50);
                opts.LoginPath = "/Account/Login";
                opts.AccessDeniedPath = "/Account/Login";
                opts.SlidingExpiration = true;
            });
            services.AddAuthorization(opts => {
                opts.AddPolicy("User", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role,"User");
                });
                opts.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });
            services.AddAuthentication().AddGoogle(options =>
            {
                //options.ClientId = configuration["Authentication:Google:ClientId"];
                //options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                options.ClientId = "261341758695-641fob1ip289ng5e94jrace3vdm7h0nr.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });
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
