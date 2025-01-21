using API.Extensions;
using API.Helpers;
using API.Middleware;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(typeof(MappingProfiles));
        builder.Services.AddDbContext<StoreContext>(x =>
            x.UseSqlite(builder.Configuration.GetConnectionString("Default")));

        builder.Services.AddDbContext<AppIdentityDbContext>(x => 
            x.UseSqlite(builder.Configuration.GetConnectionString("Identity")));

        builder.Services.AddSingleton<IConnectionMultiplexer>(c => {
            var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis")!, true);
            return ConnectionMultiplexer.Connect(configuration);
        });

        builder.Services.AddApplicationServices();
        builder.Services.AddSwaggerDocumentation();
        builder.Services.AddIdentityService(builder.Configuration);



        var app = builder.Build();

        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var context = services.GetRequiredService<StoreContext>();
            await context.Database.MigrateAsync();
            await StoreContextSeed.SeedAsync(context, loggerFactory);

            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var identityContext = services.GetRequiredService<AppIdentityDbContext>();
            await identityContext.Database.MigrateAsync();
            await AppIdentityDbContextSeed.SeedUserAsync(userManager);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred during migration");
        }

        // Configure the HTTP request pipeline.
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseStatusCodePagesWithReExecute("/errors/{0}");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.UseSwaggerDocumentation();

        // Map controllers
        app.MapControllers();

        app.MapFallbackToController("Index", "FallBack");

        app.Run();
    }
}