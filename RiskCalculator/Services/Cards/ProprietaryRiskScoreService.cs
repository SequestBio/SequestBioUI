using RiskCalculator.Models.Cards;
using SequestBioAI.Data;
using SequestBioAI.RiskScore;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service for calculating proprietary risk scores (Scientists: Modify calculations here)
/// </summary>
public class ProprietaryRiskScoreService : IProprietaryRiskScoreService
{
    public async Task<ProprietaryRiskScoreModel> CalculateScoreAsync(Stream tsvFileStream, ClinicalData clinicalData)
    {
        try
        {
            // Use existing RiskScoreCalculator for now
            var (score, topContributors, allContributors) = await RiskScoreCalculator.CalculateRiskWithContributors(tsvFileStream);

            var category = score switch
            {
                >= 10 => "High Risk",
                >= 6 => "Moderate Risk",
                _ => "Low Risk"
            };

            var recommendation = category switch
            {
                "High Risk" => "Initiate aggressive therapy and monitor closely.",
                "Moderate Risk" => "Consider standard of care with added monitoring.",
                _ => "Low risk. Continue with standard protocols."
            };

            // TODO: Scientists can add more sophisticated confidence calculations here
            var confidence = CalculateConfidence(score, allContributors.Count);

            return new ProprietaryRiskScoreModel
            {
                Score = score,
                RiskCategory = category,
                Recommendation = recommendation,
                Confidence = confidence,
                IsProcessed = true,
                CalculatedAt = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            // Log error appropriately
            Console.WriteLine($"Error calculating proprietary risk score: {ex.Message}");
            
            return new ProprietaryRiskScoreModel
            {
                Score = 0,
                RiskCategory = "Error",
                Recommendation = "Unable to calculate score due to processing error.",
                Confidence = 0,
                IsProcessed = false,
                CalculatedAt = DateTime.Now
            };
        }
    }

    public async Task<ProprietaryRiskScoreModel> CalculateScoreAsync(Stream tsvFileStream)
    {
        return await CalculateScoreAsync(tsvFileStream, new ClinicalData());
    }

    /// <summary>
    /// Calculate confidence score based on various factors
    /// Scientists: Modify this method to improve confidence calculations
    /// </summary>
    /// <param name="score">The calculated risk score</param>
    /// <param name="contributorCount">Number of contributing factors</param>
    /// <returns>Confidence percentage</returns>
    private int CalculateConfidence(int score, int contributorCount)
    {
        // Basic confidence calculation - Scientists can enhance this
        int baseConfidence = 70;
        
        // More contributors = higher confidence
        int contributorBonus = Math.Min(contributorCount * 2, 20);
        
        // Extreme scores might have lower confidence
        int scoreAdjustment = score switch
        {
            < 5 or > 95 => -5,
            < 10 or > 90 => -3,
            _ => 0
        };
        
        int confidence = baseConfidence + contributorBonus + scoreAdjustment;
        return Math.Clamp(confidence, 0, 100);
    }
} 