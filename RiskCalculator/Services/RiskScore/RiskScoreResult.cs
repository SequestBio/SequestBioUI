using RiskCalculator.Data;
using RiskCalculator.Services.Tumor;

namespace RiskCalculator.Services.RiskScore;

/// <summary>
/// Represents the result of a Sequestone risk score calculation.
/// Includes the numerical score, associated risk category, and clinical recommendation.
/// </summary>
public class RiskScoreResult
{
    /// <summary>
    /// The numerical risk score (0–100) for the patient.
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// The categorized risk level (e.g., Low, Intermediate, High).
    /// </summary>
    public string RiskCategory { get; set; } = string.Empty;

    /// <summary>
    /// The clinical recommendation based on the score and risk category.
    /// </summary>
    public string Recommendation { get; set; } = string.Empty;
}

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
    
    public RiskScoreResult GetScore(PatientData patient)
    {
        // Enrich patient with known tumor features
        patient.TumorFeatures = _tumorFeatureService.GetAllFeatures();

        int score = 0;

        var highRiskMarkers = new[] { "MMP9", "MYC", "CD44", "TP53", "BCL2" };
        foreach (var marker in highRiskMarkers)
        {
            if (patient.TumorFeatures.Any(f => f.Name == marker && f.IsPositiveMarker && f.ExpressionLevel == "Positive"))
            {
                score += 2;
            }
        }

        if (patient.SII > 0.8) score++;
        if (patient.Ki67 > 60) score += 2;
        if (patient.TP53Status.ToLower() == "mut") score += 3;

        string riskCategory = score switch
        {
            >= 10 => "High Risk",
            >= 6 => "Moderate Risk",
            _ => "Low Risk"
        };

        string recommendation = riskCategory switch
        {
            "High Risk" => "Initiate aggressive therapy and monitor closely.",
            "Moderate Risk" => "Consider standard of care with added monitoring.",
            _ => "Low risk. Continue with standard protocols."
        };

        return new RiskScoreResult
        {
            Score = score,
            RiskCategory = riskCategory,
            Recommendation = recommendation
        };
    }
}