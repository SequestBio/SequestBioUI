namespace RiskCalculator.Result;

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