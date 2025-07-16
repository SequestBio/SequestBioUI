using SequestBioAI.Data;

namespace RiskCalculator.Models.Cards;

/// <summary>
/// Model for the Key Risk Contributors card data
/// </summary>
public class KeyRiskContributorsModel
{
    /// <summary>
    /// Top contributing factors (genes/features)
    /// </summary>
    public List<RiskContributor> TopContributors { get; set; } = new();

    /// <summary>
    /// All contributing factors (genes/features)
    /// </summary>
    public List<RiskContributor> AllContributors { get; set; } = new();

    /// <summary>
    /// Protective factors
    /// </summary>
    public List<RiskContributor> ProtectiveFactors { get; set; } = new();

    /// <summary>
    /// Risk-increasing factors
    /// </summary>
    public List<RiskContributor> RiskFactors { get; set; } = new();

    /// <summary>
    /// Summary of key findings
    /// </summary>
    public string ContributorSummary { get; set; } = string.Empty;

    /// <summary>
    /// Total number of contributing factors analyzed
    /// </summary>
    public int TotalFactorsAnalyzed { get; set; }

    /// <summary>
    /// Calculation timestamp
    /// </summary>
    public DateTime CalculatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Whether the analysis was successful
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
} 