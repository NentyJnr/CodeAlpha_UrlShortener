using Microsoft.EntityFrameworkCore;
using UrlShortener.Context;

namespace UrlShortener.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();
        }
    }
}
