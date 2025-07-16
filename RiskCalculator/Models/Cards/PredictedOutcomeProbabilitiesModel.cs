namespace RiskCalculator.Models.Cards;

/// <summary>
/// Model for the Predicted Outcome Probabilities card data
/// </summary>
public class PredictedOutcomeProbabilitiesModel
{
    /// <summary>
    /// Recurrence-Free Survival (RFS) data points
    /// </summary>
    public List<SurvivalPoint> RFSData { get; set; } = new();

    /// <summary>
    /// Metastasis-Free Survival (MFS) data points
    /// </summary>
    public List<SurvivalPoint> MFSData { get; set; } = new();

    /// <summary>
    /// Overall Survival (OS) data points
    /// </summary>
    public List<SurvivalPoint> OSData { get; set; } = new();

    /// <summary>
    /// Disease-Free Survival (DFS) data points
    /// </summary>
    public List<SurvivalPoint> DFSData { get; set; } = new();

    /// <summary>
    /// 5-year survival probability
    /// </summary>
    public double FiveYearSurvival { get; set; }

    /// <summary>
    /// 10-year survival probability
    /// </summary>
    public double TenYearSurvival { get; set; }

    /// <summary>
    /// Median survival time in months
    /// </summary>
    public double MedianSurvivalMonths { get; set; }

    /// <summary>
    /// Confidence interval for survival estimates
    /// </summary>
    public (double Lower, double Upper) ConfidenceInterval { get; set; }

    /// <summary>
    /// Calculation timestamp
    /// </summary>
    public DateTime CalculatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Whether the survival analysis was successful
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
}

/// <summary>
/// Represents a survival probability data point
/// </summary>
public class SurvivalPoint
{
    /// <summary>
    /// Time point (in years)
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Survival probability (0.0-1.0)
    /// </summary>
    public double Probability { get; set; }

    /// <summary>
    /// Confidence interval lower bound
    /// </summary>
    public double ConfidenceLower { get; set; }

    /// <summary>
    /// Confidence interval upper bound
    /// </summary>
    public double ConfidenceUpper { get; set; }
} 