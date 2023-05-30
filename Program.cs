
using LearningMaps;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorBootstrap;
using LearningMaps.Data;
using VisNetwork.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazorBootstrap();

builder.Services.AddSingleton<MapsData, MapsData>();
builder.Services.AddSingleton<NetData, NetData>();
builder.Services.AddVisNetwork();

await builder.Build().RunAsync();
