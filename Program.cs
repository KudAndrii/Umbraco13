WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddDeliveryApi()
    .AddComposers()
    .Build();

if (builder.Environment.IsProduction())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    var url = $"http://0.0.0.0:{port}";
    builder.WebHost.UseUrls(url);
}

WebApplication app = builder.Build();

//await app.BootUmbracoAsync();

app.MapGet("/", () => "Hello World!");
// app.UseUmbraco()
//     .WithMiddleware(u =>
//     {
//         u.UseBackOffice();
//         u.UseWebsite();
//     })
//     .WithEndpoints(u =>
//     {
//         u.UseInstallerEndpoints();
//         u.UseBackOfficeEndpoints();
//         u.UseWebsiteEndpoints();
//     });

await app.RunAsync();
