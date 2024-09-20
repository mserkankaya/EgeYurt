using Microsoft.EntityFrameworkCore;
using ProductApi.DataAccess.Concrete;

namespace ProductApi.Extensions
{
    /// <summary>
    /// Bu metod, uygulama başlatıldığında otomatik olarak migrasyonları uygular.
    /// </summary>
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            using EgeYurtContext context = serviceScope.ServiceProvider.GetRequiredService<EgeYurtContext>();

            context.Database.Migrate();
        }
    }
}
