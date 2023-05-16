using API;
using API.Middleware;
using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Skinet;

public class Program
{
   public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddAppServices(builder.Configuration);
        builder.Services.AddIdentityServices(builder.Configuration);

        var app = builder.Build();
        #region Middleware
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseStatusCodePagesWithRedirects("/error/{0}");
       
            app.UseSwagger();
            app.UseSwaggerUI();
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // apply any change to DB
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<StoreContext>();
        var identityContext = services.GetRequiredService<AppIdentityDBContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var logger = services.GetRequiredService<ILogger<Program>>();
        try
        {
            await context.Database.MigrateAsync();
            await identityContext.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context);
            await AppIdentityDBContextSeed.SeedUserAsync(userManager);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during migration");
        }

        #endregion
        // Configure the HTTP request pipeline.

        app.Run();
    }
}