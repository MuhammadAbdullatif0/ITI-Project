using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure;

public class AppIdentityDBContextSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
       if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Muhammad",
                Email = "Muhammad@mo.com",
                UserName = "MuhammadAbdullatif",
                Address = new Address
                {
                    FirstName = "Muhammad",
                    LastName = "Abdullatif",
                    Street = "Tefa Street",
                    City = "AwladSaif",
                    State = "King",
                    ZipCode = "1998"
                }
            };
            await userManager.CreateAsync(user , "Pa$$w0rd");
        }
    }
}
