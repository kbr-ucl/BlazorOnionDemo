using BlazorOnionDemo.Application;
using BlazorOnionDemo.Infrastructure;
using BlazorOnionDemo.WebUi.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Blazor frontend til Domain Centric Backend - IoC setup
builder.Services.AddApplicationServices(); // from BlazorOnionDemo.Application
builder.Services.AddInfrastructureServices(builder.Configuration); // from BlazorOnionDemo.Infrastructure


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
