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

    /// <summary>
    /// Computes a mock Sequestone risk score for the specified patient.
    /// This is currently randomized and should be replaced with real predictive logic.
    /// </summary>
    public SequestoneScoreService(TumorFeatureService tumorFeatureService)
    {
        _tumorFeatureService = tumorFeatureService;
    }
    
    public async Task<PatientScoreResult> GetScoreAsync(Stream tsvFileStream)
    {
        int score = await RiskScoreCalculator.CalculateRiskCategory(tsvFileStream);

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

        return new PatientScoreResult
        {
            Score = score,
            RiskCategory = category,
            Recommendation = recommendation
        };
    }
}