WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

// var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
// var url = $"http://0.0.0.0:{port}";
// builder.WebHost.UseUrls(url);

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__umbracoDbDSN");
app.MapGet("/test-edp", () => $"Hello World with connection string: {connectionString}");

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseInstallerEndpoints();
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
