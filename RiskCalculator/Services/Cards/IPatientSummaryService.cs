using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service interface for processing patient summary data
/// </summary>
public interface IPatientSummaryService
{
    /// <summary>
    /// Process patient clinical data and generate summary
    /// </summary>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Patient summary model</returns>
    Task<PatientSummaryModel> ProcessPatientDataAsync(ClinicalData clinicalData);

    /// <summary>
    /// Identify risk factors from clinical data
    /// </summary>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>List of identified risk factors</returns>
    Task<List<string>> IdentifyRiskFactorsAsync(ClinicalData clinicalData);

    /// <summary>
    /// Generate clinical summary text
    /// </summary>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Clinical summary text</returns>
    Task<string> GenerateClinicalSummaryAsync(ClinicalData clinicalData);
} 