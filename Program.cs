using ImageSus;
using ImageSus.Models;
using ImageSus.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// These Variables are public, so we can just set them here.
// They do not have any permission besides reading the images.
// Supabase explicitly allows this.
var options = new SupabaseConfiguration();
options.Url = "https://iddasjdmhzoxvukzohgu.supabase.co";
options.Key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImlkZGFzamRtaHpveHZ1a3pvaGd1Iiwicm9sZSI6ImFub24iLCJpYXQiOjE2ODMwMzE1MDYsImV4cCI6MTk5ODYwNzUwNn0.mJO7sxyS-_OY1FhDZrQX15Ea4Re-lWeBWmJCDtJ5tp4";

// Add services to the container.
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton(options);
builder.Services.AddSingleton<ImageService>();

await builder.Build().RunAsync();