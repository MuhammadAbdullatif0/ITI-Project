using Core;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API;

public static class AppServicesExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services , IConfiguration config)
    {
        #region Connection

        var connection = config.GetConnectionString("DefultConnection");
        services.AddDbContext<StoreContext>(opt =>
        {
            opt.UseSqlite(connection);
        });
        services.AddSingleton<IConnectionMultiplexer>(c =>
        {
            var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
            return ConnectionMultiplexer.Connect(options);
        });
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IBasketRepo, BasketRepo>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductRepo, ProductRepo>();
        services.AddScoped<IPaymentService,PaymentService>();
        services.AddScoped<ITokenService, TokenSevice>();
        services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var error = actionContext.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = error
                };
                return new BadRequestObjectResult(errorResponse);
            };
        });

        #endregion

        // Add services to the container.
        #region Services
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        #endregion
        services.AddCors(p =>
        {
            p.AddPolicy("CorsPolicy", policy =>
            {
                policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            });
        });
        return services;
    }
}
