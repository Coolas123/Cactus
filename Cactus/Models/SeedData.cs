using Cactus.Models.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Claims;

namespace Cactus.Models
{
    public class SeedData
    {
        public async void fill(IApplicationBuilder app) {
            ApplicationDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var userManager = context.GetService<UserManager<User>>();
            //var roleManager = context.GetService<RoleManager<IdentityRole>>();

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();

            }
            if (!context.Users.Any()) {
                if (!context.Roles.Any()) {

                    //await roleManager.CreateAsync(new IdentityRole { Name="Admin"});
                    //await roleManager.CreateAsync(new IdentityRole { Name = "User" });
                }

                //var roleAdmin = new IdentityRole { Name = "Admin" };
                //var roleUser = new IdentityRole { Name = "User" };

                List<User> users = new List<User>{
                    new User
                    {
                        UserName = "mail@mail.ru",
                        //UserName = "anton",
                        FirstName = "Антон",
                        LastName = "Глухов",
                        Surname = "Александрович",
                        DateOfBirth = DateTime.Now.AddYears(-1).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "mail@mail.ru"
                    },
                    //new User
                    //{
                    //    UserName = "dima@mail.ru",
                    //    FirstName = "Дима",
                    //    LastName = "Дима2",
                    //    Surname = "",
                    //    DateOfBirth = DateTime.Now.AddYears(-2).ToUniversalTime(),
                    //    Gender = "Мужской",
                    //    Email = "dima@mail.ru"
                    //},
                    //new User
                    //{
                    //    UserName = "sasha@mail.ru",
                    //    FirstName = "Саша",
                    //    LastName = "Саша2",
                    //    Surname = "Саша3",
                    //    DateOfBirth = DateTime.Now.AddYears(-3).ToUniversalTime(),
                    //    Gender = "Мужской",
                    //    Email = "sasha@mail.ru"
                    //},
                    //new User
                    //{
                    //    UserName = "anna@mail.ru",
                    //    FirstName = "Анна",
                    //    LastName = "Анна2",
                    //    Surname = "Анна3",
                    //    DateOfBirth = DateTime.Now.AddYears(-5).ToUniversalTime(),
                    //    Gender = "Женский",
                    //    Email = "anna@mail.ru"
                    //},
                    //new User
                    //{
                    //    UserName = "alena@mail.ru",
                    //    FirstName = "Алена",
                    //    LastName = "Алена2",
                    //    Surname = "Алена3",
                    //    DateOfBirth = DateTime.Now.AddYears(-6).ToUniversalTime(),
                    //    Gender = "Женский",
                    //    Email = "alena@mail.ru"
                    //},
                    //new User
                    //{
                    //    UserName = "sesa@mail.ru",
                    //    FirstName = "Женя",
                    //    LastName = "Женя2",
                    //    Surname = "Женя3",
                    //    DateOfBirth = DateTime.Now.AddYears(-7).ToUniversalTime(),
                    //    Gender = "Мужской",
                    //    Email = "sesa@mail.ru"
                    //},
                    //new User
                    //{
                    //    UserName = "dina@mail.ru",
                    //    FirstName = "Дина",
                    //    LastName = "Дина2",
                    //    Surname = "Дина3",
                    //    DateOfBirth = DateTime.Now.AddYears(-8).ToUniversalTime(),
                    //    Gender = "Женский",
                    //    Email = "dina@mail.ru"
                    //},
                    //new User
                    //{
                    //    UserName = "olga@mail.ru",
                    //    FirstName = "Ольга",
                    //    LastName = "Ольга2",
                    //    Surname = "Ольга3",
                    //    DateOfBirth = DateTime.Now.AddYears(-9).ToUniversalTime(),
                    //    Gender = "Женский",
                    //    Email = "olga@mail.ru"
                    //},
                    //new User
                    //{
                    //    UserName = "danil@mail.ru",
                    //    FirstName = "Данил",
                    //    LastName = "Данил2",
                    //    Surname = "Данил3",
                    //    DateOfBirth = DateTime.Now.AddYears(-10).ToUniversalTime(),
                    //    Gender = "Мужской",
                    //    Email = "danil@mail.ru"
                    //}
                };
                foreach (var user in users) {
                    var result = await userManager.CreateAsync(user, "123456");
                    if (result.Succeeded) {
                        await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
