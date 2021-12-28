using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorAppServerLab.Data;
using BlazorAppServerLab.Models;
using BlazorAppServerLab.Services;
using Blazored.Modal;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// var a = builder.Environment;
// var b = builder.
//     var server = builder.Services.GetService<IServer>();
//
// var addresses = server?.Features.Get<IServerAddressesFeature>();
//
// return addresses?.Addresses ?? Array.Empty<string>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
// builder.Services.AddScoped<IMyNoteService, MyNoteService>();
builder.Services.AddScoped<IMyNoteService, MyNoteWebApiService>();
builder.Services.AddDbContext<MyNoteDbContext>(options => { options.UseSqlite("Data Source=MyNote.db"); });
builder.Services.AddBlazoredModal();
builder.Services.AddControllers();
builder.Services.AddHttpClient<IMyNoteService, MyNoteWebApiService>((sp, client) =>
{
    // 還沒找到更好的方式
    // https://stackoverflow.com/questions/43526630/how-can-i-get-the-baseurl-of-my-site-in-asp-net-core
    var baseAddress = sp.GetService<IServer>()?.Features.Get<IServerAddressesFeature>()?.Addresses.ElementAt(0) ?? "";
    client.BaseAddress = new Uri(baseAddress);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();