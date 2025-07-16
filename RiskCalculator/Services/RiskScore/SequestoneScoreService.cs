using RiskCalculator.Result;
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

    /// <summary>
    /// Computes a Sequestone risk score for the specified patient.
    /// This is currently randomized and should be replaced with real predictive logic.
    /// </summary>
    public SequestoneScoreService(TumorFeatureService tumorFeatureService)
    {
        _tumorFeatureService = tumorFeatureService;
    }
    
    public async Task<PatientScoreResult> GetScoreAsync(Stream tsvFileStream, ClinicalData clinicalData)
    {
        // Calculate risk score and get contributors
        var (score, topContributors, allContributors) = await RiskScoreCalculator.CalculateRiskWithContributors(tsvFileStream);

        string category = score switch
        {
            >= 10 => "High Risk",
            >= 6 => "Moderate Risk",
            _ => "Low Risk"
        };

        string recommendation = category switch
        {
            "High Risk" => "Initiate aggressive therapy and monitor closely.",
            "Moderate Risk" => "Consider standard of care with added monitoring.",
            _ => "Low risk. Continue with standard protocols."
        };

        // Generate mock values for other metrics (these can be replaced with real calculations later)
        var confidence = _random.Next(70, 95);
        var genomicInstability = _random.Next(20, 80);
        var tilLevel = _random.Next(25, 75);
        var tumorHotColdScore = _random.Next(30, 85);

        return new PatientScoreResult
        {
            ClinicalInfo = clinicalData,
            Score = score,
            RiskCategory = category,
            Recommendation = recommendation,
            Confidence = confidence,
            GenomicInstability = genomicInstability,
            TILLevel = tilLevel,
            TumorHotColdScore = tumorHotColdScore,
            TopContributors = topContributors,
            AllContributors = allContributors
        };
    }

    // Keep the old method signature for backward compatibility (optional)
    public async Task<PatientScoreResult> GetScoreAsync(Stream tsvFileStream)
    {
        return await GetScoreAsync(tsvFileStream, new ClinicalData());
    }
}