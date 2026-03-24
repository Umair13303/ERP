var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Add this to ensure the pipeline processes routes correctly
app.UseStaticFiles();
app.UseRouting();

app.MapReverseProxy();

app.Run();