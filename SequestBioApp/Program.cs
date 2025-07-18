using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Charts;
using Microsoft.EntityFrameworkCore;
using RiskCalculator.Services.RiskScore;
using RiskCalculator.Services.Tumor;
using SequestBioApp.Components;
using Radzen;
using RiskCalculator.Services.AiPipeline;
using RiskCalculator.Services.BiasMitigation;
using RiskCalculator.Services.FeatureSelection;
using SequestBioAI.DataProcessing;
using SequestBioAI.ModelTraining;
using SequestBioRepo.DbContext;
using SequestBioRepo.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Register DbContext with your connection string
builder.Services.AddDbContext<SequestBioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository
builder.Services.AddScoped<IPatientSampleRepository, PatientSampleRepository>();

// AI pipeline services
builder.Services.AddScoped<FeatureSelectionService>();
builder.Services.AddScoped<AiPipelineService>();
builder.Services.AddScoped<BiasMitigationService>(); 
builder.Services.AddScoped<SequestoneScoreService>();
builder.Services.AddScoped<DataPreprocessor>();
builder.Services.AddScoped<ModelTrainer>();


// Existing services
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
