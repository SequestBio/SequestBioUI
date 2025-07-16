using SequestBioAI.Data;

namespace RiskCalculator.Models.Cards;

/// <summary>
/// Model for the Patient Summary card data
/// </summary>
public class PatientSummaryModel
{
    /// <summary>
    /// Clinical information for the patient
    /// </summary>
    public ClinicalData ClinicalInfo { get; set; } = new();

    /// <summary>
    /// Whether clinical data is available
    /// </summary>
    public bool HasClinicalData { get; set; }

    /// <summary>
    /// Summary of key clinical indicators
    /// </summary>
    public string ClinicalSummary { get; set; } = string.Empty;

    /// <summary>
    /// Risk factors identified from clinical data
    /// </summary>
    public List<string> IdentifiedRiskFactors { get; set; } = new();
} 