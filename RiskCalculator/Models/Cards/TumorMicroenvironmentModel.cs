namespace RiskCalculator.Models.Cards;

/// <summary>
/// Model for the Tumor Microenvironment (TME) card data
/// </summary>
public class TumorMicroenvironmentModel
{
    /// <summary>
    /// Genomic instability percentage (0-100)
    /// </summary>
    public int GenomicInstability { get; set; }

    /// <summary>
    /// Tumor Infiltrating Lymphocytes (TIL) level percentage (0-100)
    /// </summary>
    public int TILLevel { get; set; }

    /// <summary>
    /// Mutation burden score
    /// </summary>
    public double MutationBurden { get; set; }

    /// <summary>
    /// Cellular heterogeneity index
    /// </summary>
    public double CellularHeterogeneity { get; set; }

    /// <summary>
    /// Immune infiltration score
    /// </summary>
    public double ImmuneInfiltration { get; set; }

    /// <summary>
    /// Stromal content percentage
    /// </summary>
    public double StromalContent { get; set; }

    /// <summary>
    /// Calculation timestamp
    /// </summary>
    public DateTime CalculatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Whether the TME analysis was successfully completed
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
} 