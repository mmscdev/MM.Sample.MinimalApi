using MassTransit;
using MassTransit.Azure.ServiceBus.Core;

namespace MM.MinimalAPI.Delivery.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddMessages(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<PackageCreatedConsumer>();

                x.AddBus(provider => AzureBusFactory.CreateUsingServiceBus(cfg =>
                {
                    cfg.Host(builder.Configuration.GetConnectionString("Azure"), h =>
                    {
                        h.RetryLimit = 3;
                        h.OperationTimeout = TimeSpan.FromSeconds(30);
                    });

                    cfg.ReceiveEndpoint("subs-packagecreated", c =>
                    {
                        c.EnablePartitioning = true;
                        c.Consumer<PackageCreatedConsumer>(provider);
                    });
                }));
            });

            builder.Services.AddMassTransitHostedService();

            return builder;
        }
    }
}
