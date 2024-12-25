var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5181);
});

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Welcome to Notification Gateway!");

app.MapControllers();

app.Run();
