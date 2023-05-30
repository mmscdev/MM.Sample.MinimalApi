using Carter;
using MM.MinimalApi.Package.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddDapper();
builder.AddServices();
builder.AddOpenApi();

var app = builder.Build();
app.UseServices();
app.MapCarter();
app.UseOpenApi("");

app.Run();
