namespace SequestBioAI.Data;

/// <summary>
/// Represents a factor that contributed to the patient's risk score.
/// </summary>
public class RiskContributor
{
    /// <summary>
    /// The name of the contributing factor (e.g., gene symbol).
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The contribution value (positive = increases risk, negative = protective).
    /// </summary>
    public double Contribution { get; set; }

    /// <summary>
    /// The impact type (e.g., "High Risk", "Protective").
    /// </summary>
    public string Impact { get; set; } = string.Empty;

    /// <summary>
    /// The expression level of this factor in the patient.
    /// </summary>
    public double ExpressionLevel { get; set; }
} 