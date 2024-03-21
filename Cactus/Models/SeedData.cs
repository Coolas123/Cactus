using Cactus.Models.Database;
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

            IWalletService walletService = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<IWalletService>();

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();

            }
            if (!context.Users.Any()) {
                var users = new List<RegisterViewModel>{
                    new RegisterViewModel
                    {
                        UserName="Anton",
                        DateOfBirth = DateTime.Now.AddYears(-1).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "mail@mail.ru",
                        Password="123456",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-2).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "dima@mail.ru",
                        Password="123456",
                        UserName="Dima",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-3).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "sasha@mail.ru",
                        Password="123456",
                        UserName="Sasha",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-5).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "anna@mail.ru",
                        Password="123456",
                        UserName="Anna",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-6).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "alena@mail.ru",
                        Password="123456",
                        UserName="Alena",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-7).ToUniversalTime(),
                        Gender = "Мужской",
                        Email = "sesa@mail.ru",
                        Password="123456",
                        UserName="Jenya",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-8).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "dina@mail.ru",
                        Password="123456",
                        UserName="Dina",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-9).ToUniversalTime(),
                        Gender = "Женский",
                        Email = "olga@mail.ru",
                        Password="123456",
                        UserName="Olga",
                        Country = Models.Enums.Country.Russia
                    },
                    new RegisterViewModel
                    {
                        DateOfBirth = DateTime.Now.AddYears(-10).ToUniversalTime(),
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
                var wallets = new List<WalletViewModel>
                {
                    new WalletViewModel
                    {
                        UserId=1,
                        CurrencyId=1,
                        Balance=10000,
                        IsActive=true
                    },
                    new WalletViewModel
                    {
                        UserId=2,
                        CurrencyId=1,
                        Balance=0,
                        IsActive=true
                    },
                    new WalletViewModel
                    {
                        UserId=3,
                        CurrencyId=1,
                        Balance=0,
                        IsActive=true
                    },
                    new WalletViewModel
                    {
                        UserId=4,
                        CurrencyId=1,
                        Balance=0,
                        IsActive=true
                    },
                    new WalletViewModel
                    {
                        UserId=5,
                        CurrencyId=1,
                        Balance=0,
                        IsActive=true
                    },
                    new WalletViewModel
                    {
                        UserId=6,
                        CurrencyId=1,
                        Balance=0,
                        IsActive=true
                    },
                    new WalletViewModel
                    {
                        UserId=7,
                        CurrencyId=1,
                        Balance=0,
                        IsActive=true
                    },
                    new WalletViewModel
                    {
                        UserId=8,
                        CurrencyId=1,
                        Balance=0,
                        IsActive=true
                    },
                    new WalletViewModel
                    {
                        UserId=9,
                        CurrencyId=1,
                        Balance=0,
                        IsActive=true
                    },
                };
                foreach (var wallet in wallets) {
                    await walletService.AddWallet(wallet);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
