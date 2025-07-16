using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Charts;
using RiskCalculator.Services.RiskScore;
using RiskCalculator.Services.Tumor;
using SequestBioApp.Components;
using Radzen;


var builder = WebApplication.CreateBuilder(args);

// Services used by the application
builder.Services.AddScoped<SequestoneScoreService>();
builder.Services.AddScoped<TumorFeatureService>();

builder.Services.AddHttpClient();
builder.Services.AddRadzenComponents();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        options.MaximumReceiveMessageSize = 1000 * 1024 * 1024; // 1 GB
    });
builder.Services
    .AddBlazorise(options => options.Immediate = true)
    .AddBootstrap5Providers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}


app.UseAntiforgery();

app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
