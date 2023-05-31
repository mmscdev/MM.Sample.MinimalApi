var builder = WebApplication.CreateBuilder(args);
builder.AddServices();
builder.Services.AddEFCore(builder.Configuration);
builder.AddOpenAPI();
builder.AddMessages();

var app = builder.Build();
app.UseServices();
app.MapCarter();
app.UseOpenAPI(string.Empty);

await app.MigrateData();

app.Run();
