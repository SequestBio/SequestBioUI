using RiskCalculator.Models.Cards;
using SequestBioAI.Data;
using SequestBioAI.RiskScore;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service for SHAP Waterfall analysis
/// </summary>
public class ShapWaterfallService : IShapWaterfallService
{
    private readonly Random _random = new(); // TODO: Remove when real calculations are implemented

    public async Task<ShapWaterfallModel> GenerateShapWaterfallAsync(Stream tsvFileStream, ClinicalData clinicalData)
    {
        try
        {
            // Get the actual risk score as final score
            var finalScore = await RiskScoreCalculator.CalculateRiskCategory(tsvFileStream);
            
            // TODO: Replace this entire method with real SHAP analysis
            // For now, generate mock SHAP contributions
            await Task.Delay(1); // Simulate processing time
            
            var baseScore = 50.0; // Population average baseline
            var contributions = GenerateMockShapContributions(finalScore, baseScore);
            
            return new ShapWaterfallModel
            {
                BaseRiskScore = baseScore,
                FinalRiskScore = finalScore,
                ShapContributions = contributions,
                ContributionSummary = GenerateContributionSummary(contributions),
                TotalFeaturesAnalyzed = contributions.Count + _random.Next(50, 200), // Mock total features
                IsAnalysisComplete = true,
                CalculatedAt = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating SHAP waterfall: {ex.Message}");
            
            return new ShapWaterfallModel
            {
                BaseRiskScore = 0,
                FinalRiskScore = 0,
                ShapContributions = new List<ShapContribution>(),
                ContributionSummary = "Analysis failed",
                TotalFeaturesAnalyzed = 0,
                IsAnalysisComplete = false,
                CalculatedAt = DateTime.Now
            };
        }
    }

    /// <summary>
    /// Generate mock SHAP contributions for demonstration
    /// TODO: Replace with real SHAP value calculations
    /// </summary>
    private List<ShapContribution> GenerateMockShapContributions(double finalScore, double baseScore)
    {
        var totalDelta = finalScore - baseScore;
        var contributions = new List<ShapContribution>();
        
        // Generate mock gene contributions
        var geneNames = new[] { "BRCA1", "TP53", "EGFR", "KRAS", "PIK3CA", "PTEN", "MYC", "RB1", "CDKN2A", "APC" };
        var clinicalFactors = new[] { "Age", "Stage", "Grade", "Tumor Size", "Lymph Node Status" };
        
        var remainingDelta = totalDelta;
        var contributionCount = Math.Min(8, geneNames.Length + clinicalFactors.Length);
        
        // Add gene contributions
        for (int i = 0; i < Math.Min(6, geneNames.Length) && i < contributionCount; i++)
        {
            var shapValue = (remainingDelta / (contributionCount - i)) * (_random.NextDouble() * 0.8 + 0.6);
            remainingDelta -= shapValue;
            
            contributions.Add(new ShapContribution
            {
                FeatureName = geneNames[i],
                ShapValue = shapValue,
                FeatureValue = _random.NextDouble() * 10 + 1, // Mock expression value
                FeatureType = "Gene",
                Explanation = $"{geneNames[i]} expression {(shapValue > 0 ? "increases" : "decreases")} risk by {Math.Abs(shapValue):F2} points"
            });
        }
        
        // Add clinical contributions
        for (int i = 0; i < Math.Min(2, clinicalFactors.Length) && contributions.Count < contributionCount; i++)
        {
            var shapValue = remainingDelta / (contributionCount - contributions.Count);
            remainingDelta -= shapValue;
            
            contributions.Add(new ShapContribution
            {
                FeatureName = clinicalFactors[i],
                ShapValue = shapValue,
                FeatureValue = _random.NextDouble() * 5 + 1, // Mock clinical value
                FeatureType = "Clinical",
                Explanation = $"{clinicalFactors[i]} {(shapValue > 0 ? "increases" : "decreases")} risk by {Math.Abs(shapValue):F2} points"
            });
        }
        
        // Sort by absolute SHAP value (descending)
        return contributions.OrderByDescending(c => Math.Abs(c.ShapValue)).ToList();
    }

    /// <summary>
    /// Generate a summary of the key contributions
    /// </summary>
    private string GenerateContributionSummary(List<ShapContribution> contributions)
    {
        if (!contributions.Any()) return "No significant contributions identified.";
        
        var topPositive = contributions.Where(c => c.ShapValue > 0).Take(2).ToList();
        var topNegative = contributions.Where(c => c.ShapValue < 0).Take(2).ToList();
        
        var summary = "Key findings: ";
        
        if (topPositive.Any())
        {
            summary += $"Risk increased by {string.Join(", ", topPositive.Select(c => c.FeatureName))}";
        }
        
        if (topNegative.Any())
        {
            if (topPositive.Any()) summary += "; ";
            summary += $"Risk decreased by {string.Join(", ", topNegative.Select(c => c.FeatureName))}";
        }
        
        return summary + ".";
    }
} 