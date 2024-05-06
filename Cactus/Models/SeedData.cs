using Cactus.Components;
using Cactus.Models.Database;
using Cactus.Models.Responses;
using Cactus.Models.ViewModels;
using Cactus.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cactus.Models
{
    public class SeedData
    {
        public async void fill(IApplicationBuilder app) {
            ApplicationDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            IUserService userService = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<IUserService>();

            IAuthorService authorService = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<IAuthorService>();

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();

            }
            if (!context.Users.Any()) {
                var users = new List<RegisterViewModel>{
                    new RegisterViewModel
                    {
                        UserName="Anton",
                        DateOfBirth = DateTime.Now.AddYears(-21).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "anton@gmail.com",
                        Password="aA1@123456",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-26).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "dima@gmail.com",
                        Password="aA1@123456",
                        UserName="Dima",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-21).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "artyom@gmail.com",
                        Password="aA1@123456",
                        UserName="Artyom",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-23).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "anna@mail.ru",
                        Password="123456",
                        UserName="Anna",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-20).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "alena@mail.ru",
                        Password="123456",
                        UserName="Alena",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-25).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "sesa@mail.ru",
                        Password="123456",
                        UserName="Jenya",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-26).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "dina@mail.ru",
                        Password="123456",
                        UserName="Dina",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-27).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "olga@mail.ru",
                        Password="123456",
                        UserName="Olga",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-28).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "danil@mail.ru",
                        Password="123456",
                        UserName="Danil",
                        Country = Models.Enums.Country.Russia
                    }
                };
                foreach (var user in users) {
                    await userService.Register(user);
                }
                var author = new RegisterAuthorViewModel
                {
                    UrlPage = "anton"
                };
                authorService.RegisterAuthor(new RegisterAuthorViewModel{UrlPage = "anton"},1);
                authorService.RegisterAuthor(new RegisterAuthorViewModel{UrlPage = "dima"},2);
                BaseResponse<User> userModer = await userService.GetAsync(2);
                userModer.Data.SystemRoleId = (int)Enums.SystemRole.Moderator;
                context.Update(userModer.Data);
                await context.SaveChangesAsync();
            }
        }
    }
}
