using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Charts;
using RiskCalculator.Services.RiskScore;
using RiskCalculator.Services.Tumor;
using RiskCalculator.Services.Cards;
using SequestBioApp.Components;
using Radzen;


var builder = WebApplication.CreateBuilder(args);

// Core services
builder.Services.AddScoped<SequestoneScoreService>();
builder.Services.AddScoped<TumorFeatureService>();

// Card services - register all interfaces with their implementations
builder.Services.AddScoped<IProprietaryRiskScoreService, ProprietaryRiskScoreService>();
builder.Services.AddScoped<IPatientSummaryService, PatientSummaryService>();
builder.Services.AddScoped<ITumorMicroenvironmentService, TumorMicroenvironmentService>();
builder.Services.AddScoped<ITumorImmuneStatusService, TumorImmuneStatusService>();
builder.Services.AddScoped<IKeyRiskContributorsService, KeyRiskContributorsService>();
builder.Services.AddScoped<IPredictedOutcomeProbabilitiesService, PredictedOutcomeProbabilitiesService>();
builder.Services.AddScoped<IBenchmarkComparisonService, BenchmarkComparisonService>();
builder.Services.AddScoped<ITabbedInsightsService, TabbedInsightsService>();
builder.Services.AddScoped<IShapWaterfallService, ShapWaterfallService>();

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
    app.UseHttpsRedirection();
}

app.UseAntiforgery();
app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
