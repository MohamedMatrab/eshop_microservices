
using Carter;

var builder = WebApplication.CreateBuilder(args);

//adding services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

//Configure Http Request Pipeline
app.MapCarter();

app.Run();