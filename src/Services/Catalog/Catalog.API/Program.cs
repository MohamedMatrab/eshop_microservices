var builder = WebApplication.CreateBuilder(args);

//adding services to the container

var app = builder.Build();

//Configure Http Request Pipeline

app.Run();