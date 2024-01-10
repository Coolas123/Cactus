using Cactus.Models.Database;
using Cactus.Models.ViewModels;
using Cactus.Services.Implementations;
using Cactus.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cactus.Models
{
    public class SeedData
    {
        public async void fill(IApplicationBuilder app) {
            ApplicationDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            IUserService userService = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<IUserService>();

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();

            }
            if (!context.Users.Any()) {
                var users = new List<RegisterViewModel>{
                    new RegisterViewModel
                    {
                        FirstName = "Антон",
                        LastName = "Глухов",
                        Surname = "Александрович",
                        DateOfBirth = DateTime.Now.AddYears(-1).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "mail@mail.ru",
                        Password="123456"
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Дима",
                        LastName = "Дима2",
                        Surname = "",
                        DateOfBirth = DateTime.Now.AddYears(-2).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "dima@mail.ru",
                        Password="123456"
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Саша",
                        LastName = "Саша2",
                        Surname = "Саша3",
                        DateOfBirth = DateTime.Now.AddYears(-3).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "sasha@mail.ru",
                        Password="123456"
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Анна",
                        LastName = "Анна2",
                        Surname = "Анна3",
                        DateOfBirth = DateTime.Now.AddYears(-5).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "anna@mail.ru",
                        Password="123456"
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Алена",
                        LastName = "Алена2",
                        Surname = "Алена3",
                        DateOfBirth = DateTime.Now.AddYears(-6).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "alena@mail.ru",
                        Password="123456"
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Женя",
                        LastName = "Женя2",
                        Surname = "Женя3",
                        DateOfBirth = DateTime.Now.AddYears(-7).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "sesa@mail.ru",
                        Password="123456"
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Дина",
                        LastName = "Дина2",
                        Surname = "Дина3",
                        DateOfBirth = DateTime.Now.AddYears(-8).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "dina@mail.ru",
                        Password="123456"
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Ольга",
                        LastName = "Ольга2",
                        Surname = "Ольга3",
                        DateOfBirth = DateTime.Now.AddYears(-9).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "olga@mail.ru",
                        Password="123456"
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Данил",
                        LastName = "Данил2",
                        Surname = "Данил3",
                        DateOfBirth = DateTime.Now.AddYears(-10).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "danil@mail.ru",
                        Password="123456"
                    }
                };
                foreach (var user in users) {
                    var result = await userService.Register(user);
                    if (result.StatucCode==StatusCodes.Status200OK) {
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
