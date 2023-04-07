using Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Skinet;

public class Program
{
   public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connection = builder.Configuration.GetConnectionString("DefultConnection");
        builder.Services.AddDbContext<StoreContext>(opt =>
        {
            opt.UseSqlite(connection);
        });

        builder.Services.AddScoped<IProductRepo, ProductRepo>();
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider;
        var context = service.GetRequiredService<StoreContext>();
        var logger = service.GetRequiredService<ILogger<Program>>();
        try
        {
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "an error occured during migration");
        }
        app.Run();
    }
}