using Microsoft.EntityFrameworkCore;

namespace MM.MinimalAPI.Delivery.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DeliveryContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("sql")));

            return services;
        }

        public static async Task<IApplicationBuilder> MigrateData(this IApplicationBuilder app)
        {
            using (var db = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<DeliveryContext>())
            {
                await db.Database.MigrateAsync();
            }

            return app;
        }
    }
}
