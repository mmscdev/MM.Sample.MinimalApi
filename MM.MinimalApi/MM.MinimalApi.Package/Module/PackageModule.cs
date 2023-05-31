
using Dapper;
using MassTransit;
using MassTransit.Transports;
using System.Data.SqlClient;

namespace MM.MinimalApi.Package.Module
{
    public class PackageModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/package", async (SqlConnection db) =>
                await db.QueryAsync("select * from package"));

            app.MapPost("/package", async (SqlConnection db, IPublishEndpoint publishEndpoint, RegisterPackageDto dto) =>
            {
                var newPackage = await db.QueryFirstOrDefaultAsync<RegisterPackageDto>(
                       @"insert into package(code, country, description)
                          output inserted.*
                          values(@code, @country, @description)", dto);

                await publishEndpoint.Publish(new PackageCreated
                {
                    PackageId = newPackage.PackageId,
                    Code = newPackage.Code,
                    Timestamp = DateTimeOffset.Now
                });

                return Results.Created($"/package/{newPackage.PackageId}", newPackage);
            });
        }
    }
}
