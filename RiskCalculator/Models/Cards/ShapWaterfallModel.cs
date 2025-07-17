namespace RiskCalculator.Models.Cards;

/// <summary>
/// Model for the SHAP Waterfall card data
/// </summary>
public class ShapWaterfallModel
{
    /// <summary>
    /// Base risk score (starting point)
    /// </summary>
    public double BaseRiskScore { get; set; }

    /// <summary>
    /// Final risk score (ending point)
    /// </summary>
    public double FinalRiskScore { get; set; }

    /// <summary>
    /// SHAP contributions (ranked by absolute impact)
    /// </summary>
    public List<ShapContribution> ShapContributions { get; set; } = new();

    /// <summary>
    /// Summary of key findings
    /// </summary>
    public string ContributionSummary { get; set; } = string.Empty;

    /// <summary>
    /// Total number of features analyzed
    /// </summary>
    public int TotalFeaturesAnalyzed { get; set; }

    /// <summary>
    /// Calculation timestamp
    /// </summary>
    public DateTime CalculatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Whether the SHAP analysis was successful
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
}

/// <summary>
/// Represents a single SHAP contribution for the waterfall plot
/// </summary>
public class ShapContribution
{
    /// <summary>
    /// Feature name (e.g., gene name, clinical factor)
    /// </summary>
    public string FeatureName { get; set; } = string.Empty;

    /// <summary>
    /// SHAP value (delta-score contribution)
    /// </summary>
    public double ShapValue { get; set; }

    /// <summary>
    /// Feature value for this patient
    /// </summary>
    public double FeatureValue { get; set; }

    /// <summary>
    /// Feature type (Gene, Clinical, Pathway, etc.)
    /// </summary>
    public string FeatureType { get; set; } = string.Empty;

    /// <summary>
    /// Detailed explanation of the contribution
    /// </summary>
    public string Explanation { get; set; } = string.Empty;

    /// <summary>
    /// Whether this contribution increases risk (positive) or decreases risk (negative)
    /// </summary>
    public bool IncreasesRisk => ShapValue > 0;
} 