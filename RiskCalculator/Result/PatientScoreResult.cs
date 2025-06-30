namespace RiskCalculator.Result;

/// <summary>
/// Represents the result of a Sequestone risk score calculation.
/// Includes the numerical score, associated risk category, clinical recommendation,
/// AI confidence level, and genomic instability index.
/// </summary>
public class PatientScoreResult
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

    /// <summary>
    /// The AI model's confidence in the calculated score (0–100).
    /// </summary>
    public int Confidence { get; set; }

    /// <summary>
    /// The patient's genomic instability index (0–100).
    /// </summary>
    public int GenomicInstability { get; set; }
}