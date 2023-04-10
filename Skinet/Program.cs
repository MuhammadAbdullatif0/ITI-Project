using API;
using API.Middleware;
using Core;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Skinet;

public class Program
{
   public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddAppServices(builder.Configuration);

        var app = builder.Build();
        #region Middleware
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseStatusCodePagesWithRedirects("/error/{0}");
       
            app.UseSwagger();
            app.UseSwaggerUI();
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthorization();

        app.MapControllers();

        // apply any change to DB
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider;
        var context = service.GetRequiredService<StoreContext>();
        var logger = service.GetRequiredService<ILogger<Program>>();
        try
        {
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "an error occured during migration");
        }
        #endregion
        // Configure the HTTP request pipeline.

        app.Run();
    }
}