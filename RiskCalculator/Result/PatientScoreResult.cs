using SequestBioAI.Data;

namespace RiskCalculator.Result;

/// <summary>
/// Represents the result of a Sequestone risk score calculation.
/// Includes the numerical score, associated risk category, clinical recommendation,
/// AI confidence level, genomic instability index, and top contributing factors.
/// </summary>
public class PatientScoreResult
{
    /// <summary>
    /// Clinical information for the patient (used for Patient Summary card display)
    /// </summary>
    public ClinicalData ClinicalInfo { get; set; } = new();

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

    /// <summary>
    /// The patient's tumor infiltrating lymphocytes level (0–100).
    /// </summary>
    public int TILLevel { get; set; }

    /// <summary>
    /// The patient's tumor hot/cold score (0–100, where 0=Cold, 100=Hot).
    /// </summary>
    public int TumorHotColdScore { get; set; }

    /// <summary>
    /// The top contributing factors (genes) that influenced the risk score.
    /// </summary>
    public List<RiskContributor> TopContributors { get; set; } = new();

    /// <summary>
    /// All contributing factors (genes) that influenced the risk score.
    /// </summary>
    public List<RiskContributor> AllContributors { get; set; } = new();
}