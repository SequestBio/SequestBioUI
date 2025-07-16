using SequestBioAI.Data;

namespace RiskCalculator.Models.Cards;

/// <summary>
/// Model for the Proprietary Risk Score card data
/// </summary>
public class ProprietaryRiskScoreModel
{
    /// <summary>
    /// The calculated risk score (0-100)
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Risk category (Low Risk, Moderate Risk, High Risk)
    /// </summary>
    public string RiskCategory { get; set; } = string.Empty;

    /// <summary>
    /// Clinical recommendation based on the score
    /// </summary>
    public string Recommendation { get; set; } = string.Empty;

    /// <summary>
    /// Confidence level of the prediction (0-100)
    /// </summary>
    public int Confidence { get; set; }

    /// <summary>
    /// Timestamp when the score was calculated
    /// </summary>
    public DateTime CalculatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Whether the file was successfully processed
    /// </summary>
    public bool IsProcessed { get; set; }
} 