using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service interface for tabbed insights analysis
/// </summary>
public interface ITabbedInsightsService
{
    /// <summary>
    /// Get tabbed insights for a patient
    /// </summary>
    /// <param name="patientId">Patient identifier</param>
    /// <param name="riskScore">Current risk score</param>
    /// <returns>Tabbed insights model</returns>
    Task<TabbedInsightsModel> GetTabbedInsightsAsync(string patientId, double? riskScore);

    /// <summary>
    /// Generate comprehensive tabbed insights from genomic and clinical data
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Tabbed insights model</returns>
    Task<TabbedInsightsModel> GenerateTabbedInsightsAsync(Stream tsvFileStream, ClinicalData clinicalData);
} 