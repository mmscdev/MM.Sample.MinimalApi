using Microsoft.EntityFrameworkCore;

namespace MM.MinimalAPI.Delivery.Module
{
    public class DeliveryModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/delivery/{packageId}/history", async (int packageId, DeliveryContext db) =>
                await db.Locations.Where(x => x.PackageId == packageId).OrderByDescending(x => x.Id)
                        .ToArrayAsync());

            app.MapPost("/delivery", async (DeliveryContext db, PackageLocation dto) =>
            {
                var package = await db.Packages.Where(x => x.Code == dto.Code)
                        .FirstOrDefaultAsync();

                if (package != null)
                {
                    db.Locations.Add(new Location()
                    {
                        Id = dto.Id,
                        PackageId = package.PackageId,
                        Latitude = dto.Latitude,
                        Longitude = dto.Longitude
                    });

                    await db.SaveChangesAsync();
                }

                return Results.Created($"/delivery/{dto.Id}", dto);
            });
        }
    }
}
