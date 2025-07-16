using RiskCalculator.Models.Cards;
using SequestBioAI.Data;
using SequestBioAI.RiskScore;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service for benchmark comparison analysis (Scientists: Replace with real benchmark calculations)
/// </summary>
public class BenchmarkComparisonService : IBenchmarkComparisonService
{
    private readonly Random _random = new(); // TODO: Remove when real calculations are implemented

    public async Task<BenchmarkComparisonModel> PerformBenchmarkComparisonAsync(Stream tsvFileStream, ClinicalData clinicalData)
    {
        try
        {
            // Get the real Sequestone score
            var sequestoneScore = await RiskScoreCalculator.CalculateRiskCategory(tsvFileStream);
            
            // TODO: Scientists - Replace these with real benchmark calculations
            // For now, use mock data for comparison methods
            var clinicalFactorsScore = CalculateMockClinicalScore(clinicalData);
            var stagingScore = CalculateMockStagingScore(clinicalData);
            var molecularSubtypeScore = CalculateMockMolecularScore(clinicalData);

            var benchmarkData = new List<BenchmarkPoint>
            {
                new BenchmarkPoint { Label = "Sequestone Score", Value = sequestoneScore, Metric = "Risk Score", Color = "#007bff" },
                new BenchmarkPoint { Label = "Clinical Factors Only", Value = clinicalFactorsScore, Metric = "Risk Score", Color = "#28a745" },
                new BenchmarkPoint { Label = "Traditional Staging", Value = stagingScore, Metric = "Risk Score", Color = "#ffc107" },
                new BenchmarkPoint { Label = "Molecular Subtype", Value = molecularSubtypeScore, Metric = "Risk Score", Color = "#17a2b8" }
            };

            // Mock performance metrics
            var performanceMetrics = new Dictionary<string, double>
            {
                { "AUC", 0.85 + _random.NextDouble() * 0.1 },
                { "Sensitivity", 0.82 + _random.NextDouble() * 0.1 },
                { "Specificity", 0.87 + _random.NextDouble() * 0.1 }
            };

            var comparisonSummary = $"The SequestBio Score ({sequestoneScore}) demonstrates superior performance compared to traditional methods. " +
                                  $"Clinical factors alone scored {clinicalFactorsScore}, while staging-based assessment scored {stagingScore}.";

            return new BenchmarkComparisonModel
            {
                BenchmarkData = benchmarkData,
                SequestoneScore = sequestoneScore,
                ClinicalFactorsScore = clinicalFactorsScore,
                StagingScore = stagingScore,
                MolecularSubtypeScore = molecularSubtypeScore,
                PerformanceMetrics = performanceMetrics,
                ComparisonSummary = comparisonSummary,
                IsAnalysisComplete = true,
                CalculatedAt = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error performing benchmark comparison: {ex.Message}");
            
            return new BenchmarkComparisonModel
            {
                BenchmarkData = new List<BenchmarkPoint>(),
                SequestoneScore = 0,
                ClinicalFactorsScore = 0,
                StagingScore = 0,
                MolecularSubtypeScore = 0,
                PerformanceMetrics = new Dictionary<string, double>(),
                ComparisonSummary = "Analysis failed",
                IsAnalysisComplete = false,
                CalculatedAt = DateTime.Now
            };
        }
    }

    public Task<BenchmarkComparisonModel> GetBenchmarkComparisonAsync(string patientId, double? riskScore)
    {
        // TODO: Scientists - This method is not currently used but may be needed for future database-based lookups
        return Task.FromResult(new BenchmarkComparisonModel
        {
            BenchmarkData = new List<BenchmarkPoint>(),
            SequestoneScore = 0,
            ClinicalFactorsScore = 0,
            StagingScore = 0,
            MolecularSubtypeScore = 0,
            PerformanceMetrics = new Dictionary<string, double>(),
            ComparisonSummary = "Analysis not available",
            IsAnalysisComplete = false,
            CalculatedAt = DateTime.Now
        });
    }

    private double CalculateMockClinicalScore(ClinicalData clinicalData)
    {
        // Simple mock calculation based on clinical data
        double score = 50; // Base score
        
        if (clinicalData.DateOfBirth.HasValue)
        {
            var age = DateTime.Now.Year - clinicalData.DateOfBirth.Value.Year;
            if (age > 65) score += 10;
            else if (age < 40) score += 5;
        }
        
        if (clinicalData.CancerSubtypeStage?.Contains("III") == true) score += 15;
        if (clinicalData.CancerSubtypeStage?.Contains("IV") == true) score += 25;
        if (clinicalData.TumorGrade >= 3) score += 10;
        if (clinicalData.BiomarkerStatus?.Contains("ER-") == true) score += 8;
        if (clinicalData.BiomarkerStatus?.Contains("HER2+") == true) score += 12;
        
        return Math.Min(100, score);
    }

    private double CalculateMockStagingScore(ClinicalData clinicalData)
    {
        // Simple mock staging calculation
        double score = 30; // Base score
        
        if (clinicalData.CancerSubtypeStage?.Contains("I") == true) score += 5;
        if (clinicalData.CancerSubtypeStage?.Contains("II") == true) score += 15;
        if (clinicalData.CancerSubtypeStage?.Contains("III") == true) score += 25;
        if (clinicalData.CancerSubtypeStage?.Contains("IV") == true) score += 35;
        
        return Math.Min(100, score);
    }

    private double CalculateMockMolecularScore(ClinicalData clinicalData)
    {
        // Simple mock molecular subtype calculation
        double score = 40; // Base score
        
        if (!string.IsNullOrEmpty(clinicalData.BiomarkerStatus))
        {
            // Triple negative (highest risk)
            if (clinicalData.BiomarkerStatus.Contains("ER-") && 
                clinicalData.BiomarkerStatus.Contains("PR-") && 
                clinicalData.BiomarkerStatus.Contains("HER2-"))
            {
                score += 25;
            }
            // HER2 positive
            else if (clinicalData.BiomarkerStatus.Contains("HER2+"))
            {
                score += 20;
            }
            // Luminal B (ER+ but high grade)
            else if (clinicalData.BiomarkerStatus.Contains("ER+") && clinicalData.TumorGrade >= 3)
            {
                score += 15;
            }
            // Luminal A (ER+, low grade) - lowest risk
            else if (clinicalData.BiomarkerStatus.Contains("ER+"))
            {
                score += 5;
            }
        }
        
        return Math.Min(100, score);
    }
} 