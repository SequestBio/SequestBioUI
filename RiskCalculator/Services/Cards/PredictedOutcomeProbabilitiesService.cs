using RiskCalculator.Models.Cards;
using SequestBioAI.Data;
using SequestBioAI.RiskScore;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service for predicting outcome probabilities (Scientists: Replace with real survival calculations)
/// </summary>
public class PredictedOutcomeProbabilitiesService : IPredictedOutcomeProbabilitiesService
{
    private readonly Random _random = new(); // TODO: Remove when real calculations are implemented

    public async Task<PredictedOutcomeProbabilitiesModel> PredictOutcomeProbabilitiesAsync(Stream tsvFileStream, ClinicalData clinicalData)
    {
        try
        {
            // Get real risk score to inform survival predictions
            var riskScore = await RiskScoreCalculator.CalculateRiskCategory(tsvFileStream);
            
            // TODO: Scientists - Replace this entire method with real survival modeling
            // For now, return mock data based on risk score
            await Task.Delay(1); // Simulate processing time
            
            var riskMultiplier = riskScore / 100.0;
            var baseSurvival = 0.85 - (riskMultiplier * 0.3);
            
            var fiveYearSurvival = Math.Max(0.1, baseSurvival);
            var tenYearSurvival = Math.Max(0.05, baseSurvival * 0.8);
            var medianSurvival = Math.Max(12, baseSurvival * 120);
            
            // Generate mock survival curves
            var rfsData = GenerateMockSurvivalCurve(baseSurvival, 0.15);
            var confidenceInterval = (Math.Max(0, fiveYearSurvival - 0.1), Math.Min(1, fiveYearSurvival + 0.1));

            return new PredictedOutcomeProbabilitiesModel
            {
                RFSData = rfsData,
                MFSData = GenerateMockSurvivalCurve(baseSurvival + 0.03, 0.12),
                OSData = GenerateMockSurvivalCurve(baseSurvival + 0.07, 0.08),
                DFSData = GenerateMockSurvivalCurve(baseSurvival - 0.03, 0.18),
                FiveYearSurvival = fiveYearSurvival,
                TenYearSurvival = tenYearSurvival,
                MedianSurvivalMonths = medianSurvival,
                ConfidenceInterval = confidenceInterval,
                IsAnalysisComplete = true,
                CalculatedAt = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error predicting outcome probabilities: {ex.Message}");
            
            return new PredictedOutcomeProbabilitiesModel
            {
                RFSData = new List<SurvivalPoint>(),
                MFSData = new List<SurvivalPoint>(),
                OSData = new List<SurvivalPoint>(),
                DFSData = new List<SurvivalPoint>(),
                FiveYearSurvival = 0,
                TenYearSurvival = 0,
                MedianSurvivalMonths = 0,
                ConfidenceInterval = (0, 0),
                IsAnalysisComplete = false,
                CalculatedAt = DateTime.Now
            };
        }
    }

    public Task<PredictedOutcomeProbabilitiesModel> GetPredictedOutcomeProbabilitiesAsync(string patientId, double? riskScore)
    {
        // TODO: Scientists - This method is not currently used but may be needed for future database-based lookups
        return Task.FromResult(new PredictedOutcomeProbabilitiesModel
        {
            RFSData = new List<SurvivalPoint>(),
            MFSData = new List<SurvivalPoint>(),
            OSData = new List<SurvivalPoint>(),
            DFSData = new List<SurvivalPoint>(),
            FiveYearSurvival = 0,
            TenYearSurvival = 0,
            MedianSurvivalMonths = 0,
            ConfidenceInterval = (0, 0),
            IsAnalysisComplete = false,
            CalculatedAt = DateTime.Now
        });
    }

    private List<SurvivalPoint> GenerateMockSurvivalCurve(double baselineSurvival, double declineRate)
    {
        var survivalPoints = new List<SurvivalPoint>();
        
        for (int year = 0; year <= 10; year++)
        {
            // Simple exponential decline model
            double probability = baselineSurvival * Math.Exp(-declineRate * year);
            
            // Add some noise for confidence intervals
            double noise = (_random.NextDouble() - 0.5) * 0.1;
            double lowerBound = Math.Max(0, probability - Math.Abs(noise));
            double upperBound = Math.Min(1, probability + Math.Abs(noise));
            
            survivalPoints.Add(new SurvivalPoint
            {
                Year = year,
                Probability = probability,
                ConfidenceLower = lowerBound,
                ConfidenceUpper = upperBound
            });
        }
        
        return survivalPoints;
    }
} 