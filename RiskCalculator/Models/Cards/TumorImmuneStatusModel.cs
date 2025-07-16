namespace RiskCalculator.Models.Cards;

/// <summary>
/// Model for the Tumor Immune Status (Hot vs Cold) card data
/// </summary>
public class TumorImmuneStatusModel
{
    /// <summary>
    /// Hot/Cold tumor score (0-100, where 0=Cold, 100=Hot)
    /// </summary>
    public int TumorHotColdScore { get; set; }

    /// <summary>
    /// Immune status category (Cold, Moderate, Hot)
    /// </summary>
    public string ImmuneStatus { get; set; } = string.Empty;

    /// <summary>
    /// Calculation timestamp
    /// </summary>
    public DateTime CalculatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Whether the immune status analysis was successful
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
} 