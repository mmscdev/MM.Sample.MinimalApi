using MassTransit;

namespace MM.MinimalAPI.Delivery.Consumer
{
    public class PackageCreatedConsumer : IConsumer<PackageCreated>
    {
        private DeliveryContext _db { get; set; }
        public PackageCreatedConsumer(DeliveryContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<PackageCreated> context)
        {
            _db.Packages.Add(new Package(context.Message.PackageId, context.Message.Code));
            await _db.SaveChangesAsync();
        }
    }
}
