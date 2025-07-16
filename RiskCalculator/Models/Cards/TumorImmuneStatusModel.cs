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
    /// Description of the immune status
    /// </summary>
    public string StatusDescription { get; set; } = string.Empty;

    /// <summary>
    /// T-cell infiltration level
    /// </summary>
    public double TCellInfiltration { get; set; }

    /// <summary>
    /// B-cell infiltration level
    /// </summary>
    public double BCellInfiltration { get; set; }

    /// <summary>
    /// NK cell infiltration level
    /// </summary>
    public double NKCellInfiltration { get; set; }

    /// <summary>
    /// Macrophage infiltration level
    /// </summary>
    public double MacrophageInfiltration { get; set; }

    /// <summary>
    /// PD-L1 expression level
    /// </summary>
    public double PDL1Expression { get; set; }

    /// <summary>
    /// Interferon gamma signature score
    /// </summary>
    public double InterferonGammaSignature { get; set; }

    /// <summary>
    /// Calculation timestamp
    /// </summary>
    public DateTime CalculatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Whether the immune status analysis was successful
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
} 