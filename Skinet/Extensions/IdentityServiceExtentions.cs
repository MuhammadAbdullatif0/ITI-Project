using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API;

public static class IdentityServiceExtentions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
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

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                ValidIssuer = config["Token:Issuer"],
                ValidateIssuer = true,
                ValidateAudience = false
            };
        });
        services.AddAuthorization();
        return services;
    }
}
