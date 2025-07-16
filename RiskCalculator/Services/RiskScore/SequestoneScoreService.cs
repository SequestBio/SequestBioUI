using RiskCalculator.Models.Cards;
using RiskCalculator.Services.Tumor;
using SequestBioAI.Data;
using SequestBioAI.RiskScore;

namespace RiskCalculator.Services.RiskScore;

/// <summary>
/// Service that calculates a Sequestone Score for a given patient.
/// This can be extended later to incorporate real AI or quantum model results.
/// </summary>
public class SequestoneScoreService
{
    private readonly TumorFeatureService _tumorFeatureService;
    private readonly Random _random = new();

    public SequestoneScoreService(TumorFeatureService tumorFeatureService)
    {
        _tumorFeatureService = tumorFeatureService;
    }
    
    /// <summary>
    /// Computes a Sequestone risk score for the specified patient.
    /// Returns the core risk score model for the main risk calculation.
    /// </summary>
    public async Task<ProprietaryRiskScoreModel> GetScoreAsync(Stream tsvFileStream, ClinicalData clinicalData)
    {
        try
        {
            Console.WriteLine("🔍 SequestoneScoreService: Starting score calculation...");
            Console.WriteLine($"📊 Stream length: {tsvFileStream.Length}, Position: {tsvFileStream.Position}");
            
            // Calculate risk score and get contributors
            var (score, topContributors, allContributors) = await RiskScoreCalculator.CalculateRiskWithContributors(tsvFileStream);
            
            Console.WriteLine($"📈 Calculated score: {score}");
            Console.WriteLine($"🧬 Top contributors: {topContributors.Count}");
            Console.WriteLine($"🧬 All contributors: {allContributors.Count}");

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

            // Calculate confidence based on contributing factors
            var confidence = CalculateConfidence(score, allContributors.Count);

            var result = new ProprietaryRiskScoreModel
            {
                Score = score,
                RiskCategory = category,
                Recommendation = recommendation,
                Confidence = confidence,
                IsProcessed = true,
                CalculatedAt = DateTime.Now
            };
            
            Console.WriteLine($"✅ SequestoneScoreService: Score calculation completed successfully");
            Console.WriteLine($"📊 Final result: Score={result.Score}, Category={result.RiskCategory}, Confidence={result.Confidence}");
            
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ SequestoneScoreService: Error calculating score: {ex.Message}");
            Console.WriteLine($"❌ Stack trace: {ex.StackTrace}");
            
            // Return a default model instead of throwing
            return new ProprietaryRiskScoreModel
            {
                Score = 0,
                RiskCategory = "Error",
                Recommendation = "Error calculating risk score. Please try again.",
                Confidence = 0,
                IsProcessed = false,
                CalculatedAt = DateTime.Now
            };
        }
    }

    /// <summary>
    /// Backward compatibility method - delegates to main method
    /// </summary>
    public async Task<ProprietaryRiskScoreModel> GetScoreAsync(Stream tsvFileStream)
    {
        return await GetScoreAsync(tsvFileStream, new ClinicalData());
    }

    /// <summary>
    /// Calculate confidence based on score and number of contributing factors
    /// </summary>
    private int CalculateConfidence(int score, int contributorCount)
    {
        // Base confidence on score stability and contributor count
        var baseConfidence = 70;
        var scoreConfidence = Math.Min(20, score * 2); // Higher scores = more confidence
        var contributorConfidence = Math.Min(10, contributorCount / 5); // More contributors = more confidence
        
        return Math.Min(95, baseConfidence + scoreConfidence + contributorConfidence);
    }
}