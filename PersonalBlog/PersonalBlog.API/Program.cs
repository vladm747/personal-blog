using PersonalBlog.API.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterApplicationServices(builder.Configuration);

var app = builder.Build();
app.ConfigureMiddleware();

app.Run();