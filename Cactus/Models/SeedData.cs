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
                        UserName="Anton",
                        FirstName = "Антон",
                        LastName = "Глухов",
                        DateOfBirth = DateTime.Now.AddYears(-1).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "mail@mail.ru",
                        Password="123456",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Дима",
                        LastName = "Дима2",
                        DateOfBirth = DateTime.Now.AddYears(-2).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "dima@mail.ru",
                        Password="123456",
                        UserName="Dima",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Саша",
                        LastName = "Саша2",
                        DateOfBirth = DateTime.Now.AddYears(-3).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "sasha@mail.ru",
                        Password="123456",
                        UserName="Sasha",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Анна",
                        LastName = "Анна2",
                        DateOfBirth = DateTime.Now.AddYears(-5).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "anna@mail.ru",
                        Password="123456",
                        UserName="Anna",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Алена",
                        LastName = "Алена2",
                        DateOfBirth = DateTime.Now.AddYears(-6).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "alena@mail.ru",
                        Password="123456",
                        UserName="Alena",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Женя",
                        LastName = "Женя2",
                        DateOfBirth = DateTime.Now.AddYears(-7).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "sesa@mail.ru",
                        Password="123456",
                        UserName="Jenya",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Дина",
                        LastName = "Дина2",
                        DateOfBirth = DateTime.Now.AddYears(-8).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "dina@mail.ru",
                        Password="123456",
                        UserName="Dina",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Ольга",
                        LastName = "Ольга2",
                        DateOfBirth = DateTime.Now.AddYears(-9).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "olga@mail.ru",
                        Password="123456",
                        UserName="Olga",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        FirstName = "Данил",
                        LastName = "Данил2",
                        DateOfBirth = DateTime.Now.AddYears(-10).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "danil@mail.ru",
                        Password="123456",
                        UserName="Danil",
                        Country = Models.Enums.Country.Russia
                    }
                };
                foreach (var user in users) {
                    var result = await userService.Register(user);
                    if (result.StatusCode==StatusCodes.Status200OK) {
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
