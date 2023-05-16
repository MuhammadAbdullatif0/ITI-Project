using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API;

public static class IdentityServiceExtentions
{
    public static IServiceCollection AddIdentityServices(this  IServiceCollection services , IConfiguration config)
    {
        services.AddDbContext<AppIdentityDBContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("IdentityConnection"));
        });
        services.AddIdentityCore<AppUser>(opt =>
        {
            // identity options
        })
        .AddEntityFrameworkStores<AppIdentityDBContext>()
        .AddSignInManager<SignInManager<AppUser>>();

        services.AddAuthentication();
        services.AddAuthorization();
        return services;
    }
}
