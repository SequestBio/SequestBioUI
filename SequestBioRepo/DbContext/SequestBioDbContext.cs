using Microsoft.EntityFrameworkCore;
using SequestBioDataModel.Entities;

namespace SequestBioRepo.DbContext;

public class SequestBioDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public SequestBioDbContext(DbContextOptions<SequestBioDbContext> options)
        : base(options)
    {
    }

    public DbSet<PatientSampleEntity> PatientSamples { get; set; } = null!;
    public DbSet<PredictionResultEntity> PredictionResults { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PatientSampleEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Features)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, new System.Text.Json.JsonSerializerOptions()),
                    v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, float>>(v, new System.Text.Json.JsonSerializerOptions()) ?? new());
        });

        modelBuilder.Entity<PredictionResultEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        base.OnModelCreating(modelBuilder);
    }
}